/*
 *   Nitrogen - Halo Content API
 *   Copyright (c) 2013 Matt Saville and Aaron Dierking
 * 
 *   This file is part of Nitrogen.
 *
 *   Nitrogen is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   Nitrogen is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with Nitrogen.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace Nitrogen.Utilities.IO
{
    /// <summary>
    /// Reads primitive data types as binary values of varying bit sizes from a stream.
    /// </summary>
    public class BitReader
        : IDisposable
    {
        const int WindowSize = sizeof(ulong) * 8;

        private Stream baseStream;
        private bool leaveOpen;

        private ulong window;
        private int bitsAvailable;

        private byte currentByte;
        private int currentBit = 8;
        private long currentBitPos = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="BitReader"/> class based on the specified
        /// stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        public BitReader(Stream input)
            : this(input, false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitReader"/> class based on the specified
        /// stream, and optionally leaves the stream open.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="leaveOpen">
        /// <c>true</c> to leave the stream open after the <see cref="BitReader"/> object is disposed;
        /// otherwise, <c>false</c>.
        /// </param>
        public BitReader(Stream input, bool leaveOpen)
        {
            Contract.Requires<ArgumentNullException>(input != null);

            this.baseStream = input;
            this.leaveOpen = leaveOpen;
        }

        ~BitReader()
        {
            Dispose(false);
        }

        /// <summary>
        /// Exposes access to the underlying stream of the <see cref="BitReader"/>.
        /// </summary>
        public virtual Stream BaseStream { get { return this.baseStream; } }

        /// <summary>
        /// Gets the position of the current bit in the stream.
        /// </summary>
        public long Position
        {
            get { return this.currentBitPos; }
        }

        /// <summary>
        /// Reads a signed n-bit integer from the stream.
        /// </summary>
        /// <param name="n">The number of bits to read.</param>
        /// <returns>The integer that was read.</returns>
        public long ReadIntN(int n)
        {
            Contract.Requires<ArgumentOutOfRangeException>(n > 0 && n <= WindowSize);

            var unsigned = ReadUIntN(n);
            return ExtendSign(unsigned, n);
        }

        /// <summary>
        /// Reads an unsigned n-bit integer from the stream.
        /// </summary>
        /// <param name="n">The number of bits to read.</param>
        /// <returns>The integer that was read.</returns>
        public ulong ReadUIntN(int n)
        {
            Contract.Requires<ArgumentOutOfRangeException>(n > 0 && n <= WindowSize);

            EnsureBitsAvailable(n);

            // Pull bits from the left of the window
            var result = this.window >> (WindowSize - n);
            if (n < WindowSize)
                this.window <<= n;
            else
                this.window = 0;
            this.bitsAvailable -= n;
            this.currentBitPos += n;
            return result;
        }

        /// <summary>
        /// Reads an array of bytes from the stream.
        /// </summary>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The byte array that was read.</returns>
        public byte[] ReadBytes(int count)
        {
            var result = new byte[count];
            for (var i = 0; i < count; i++)
                result[i] = (byte)ReadUIntN(8);
            return result;
        }

        /// <summary>
        /// Reads bytes from the stream into an existing array.
        /// </summary>
        /// <param name="buffer">The array to store the read bytes to.</param>
        /// <param name="offset">The index of the element to start storing bytes to.</param>
        /// <param name="count">The number of bytes to read.</param>
        public void ReadBytes(byte[] buffer, int offset, int count)
        {
            for (var i = 0; i < count; i++)
                buffer[offset + i] = (byte)ReadUIntN(8);
        }

        /// <summary>
        /// Seeks to a bit in the stream.
        /// </summary>
        /// <param name="bitPos">The index of the bit to seek to.</param>
        public void SeekTo(long bitPos)
        {
            var bytePos = bitPos / 8;
            var bitOffset = bitPos % 8;
            SeekTo(bytePos, (int)bitOffset);
        }

        /// <summary>
        /// Seeks to an offset within a byte in the stream.
        /// </summary>
        /// <param name="bytePos">The position of the byte to seek inside.</param>
        /// <param name="bitOffset">The offset of the bit from the start of the byte to seek to. 0 = MSB.</param>
        public void SeekTo(long bytePos, int bitOffset)
        {
            Contract.Requires<ArgumentException>(bytePos >= 0);
            Contract.Requires<ArgumentException>(bitOffset >= 0 && bitOffset < 8);

            this.baseStream.Position = bytePos;
            this.currentBitPos = bytePos * 8 + bitOffset;
            ResetWindow(); // Technically, we might not need to reset the window and cache a new byte if the seek position is close to our current position, but meh
            CacheByte();
            this.currentBit = bitOffset;
        }

        /// <summary>
        /// Seeks relative to the current position of the stream.
        /// </summary>
        /// <param name="offset">The number of bits to seek forward or backward by.</param>
        public void SeekRelative(long offset)
        {
            SeekTo(this.currentBitPos + offset);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!this.leaveOpen)
                    this.baseStream.Dispose();

                this.baseStream = null;
            }
        }

        /// <summary>
        /// Ensures that a specified number of bits is available in the window.
        /// If not, the window is refilled as much as possible.
        /// </summary>
        /// <param name="count">The number of bits needed.</param>
        private void EnsureBitsAvailable(int count)
        {
            Contract.Requires<ArgumentOutOfRangeException>(count > 0 && count <= WindowSize);

            if (this.bitsAvailable < count)
            {
                // Refill as much of the window as possible
                while (this.bitsAvailable < WindowSize)
                {
                    if (this.currentBit == 8 && !CacheByte())
                    {
                        if (this.bitsAvailable < count)
                            throw new EndOfStreamException("Unexpected end of stream encountered");

                        break;
                    }

                    // We can only extract at most 8 bits from the current byte
                    var extractCount = Math.Min(8 - this.currentBit, WindowSize - this.bitsAvailable);

                    // Shift the byte over and mask out any extra bits to the left
                    var bits = (ulong)((this.currentByte >> (8 - this.currentBit - extractCount)) & (0xFF >> (8 - extractCount)));

                    // Add it to the window
                    this.window |= bits << (WindowSize - this.bitsAvailable - extractCount);

                    // Advance by the number of bits read
                    this.bitsAvailable += extractCount;
                    this.currentBit += extractCount;
                }
            }
        }

        /// <summary>
        /// Reads a byte from the stream and caches it.
        /// </summary>
        /// <returns><c>true</c> if the byte was successfully cached.</returns>
        private bool CacheByte()
        {
            var b = this.baseStream.ReadByte();
            if (b == -1)
                return false;
            this.currentByte = (byte)b;
            this.currentBit = 0;
            return true;
        }

        /// <summary>
        /// Resets the cache window.
        /// </summary>
        private void ResetWindow()
        {
            this.window = 0;
            this.bitsAvailable = 0;
        }

        /// <summary>
        /// Extends the sign of an unsigned integer.
        /// </summary>
        /// <param name="val">The value to sign-extend.</param>
        /// <param name="size">The original width of the value in bits.</param>
        /// <returns>The signed integer.</returns>
        private static long ExtendSign(ulong val, int size)
        {
            if (size < WindowSize && (val & (1UL << (size - 1))) != 0)
            {
                // Value is negative - fill the left with ones
                return (long)(val | (ulong.MaxValue << size));
            }
            return (long)val;
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
