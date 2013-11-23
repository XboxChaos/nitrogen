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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace Nitrogen.Core.IO
{
    public sealed class BitReader
        : BinaryReader
    {
        private const int WindowSize = sizeof(ulong) * 8;

        private ulong window;
        private int bitsAvailable;

        private byte currentByte;
        private int currentBit = 8;
        private long currentBitPos = 0;

        public BitReader(BitStream stream, bool leaveOpen = false)
            : base(stream, leaveOpen) { }

        public int Read(out long output, int n)
        {
            Contract.Requires<ArgumentOutOfRangeException>(n > 0 && n <= WindowSize);

            ulong val;
            Read(out val, n);
            output = ExtendSign(val, n);
            return n;
        }

        public int Read(out ulong output, int n)
        {
            Contract.Requires<ArgumentOutOfRangeException>(n > 0 && n <= WindowSize);

            EnsureBitsAvailable(n);

            output = this.window >> (WindowSize - n);
            if (n < WindowSize)
                this.window <<= n;
            else
                this.window = 0;
            this.bitsAvailable -= n;
            this.currentBitPos += n;

            return n;
        }

        public override int Read(out bool output)
        {
            ulong val;
            Read(out val, 1);
            output = val == 1;
            return 1;
        }

        public override int Read(out byte output)
        {
            ulong val;
            Read(out val, 8);
            output = (byte)val;
            return 8;
        }

        public override int Read(out sbyte output)
        {
            ulong val;
            Read(out val, 8);
            output = (sbyte)val;
            return 8;
        }

        public override int Read(out short output)
        {
            ulong val;
            Read(out val, 16);
            output = (short)val;
            return 16;
        }

        public override int Read(out ushort output)
        {
            ulong val;
            Read(out val, 16);
            output = (ushort)val;
            return 16;
        }

        public override int Read(out int output)
        {
            ulong val;
            Read(out val, 32);
            output = (int)val;
            return 32;
        }

        public override int Read(out uint output)
        {
            ulong val;
            Read(out val, 32);
            output = (uint)val;
            return 32;
        }

        public override int Read(out long output)
        {
            return Read(out output, 64);
        }

        public override int Read(out ulong output)
        {
            return Read(out output, 64);
        }

        public override int Read(out string output, Encoding encoding)
        {
            int count = 0;
            int delimiterSize = encoding.GetByteCount("\0");
            byte[] buffer = new byte[delimiterSize];
            var builder = new StringBuilder();
            while (this.currentBitPos < (BaseStream.Length * 8))
            {
                count += Read(buffer, buffer.Length);
                string value = encoding.GetString(buffer);
                if (value == "\0")
                    break;
                builder.Append(value);
            }
            output = builder.ToString();
            return count;
        }

        public override int Read(out string output, Encoding encoding, int length)
        {
            byte[] buffer = new byte[length];
            int count = Read(buffer, buffer.Length);
            output = encoding.GetString(buffer).Trim('\0');
            return count;
        }

        public float ReadEncodedFloat(int n, float min, float max, bool signed, bool isRounded = true, bool flag = true)
        {
            ulong encodedValue;
            Read(out encodedValue, n);

            uint maxInt = (uint)(1 << n);
            float result;
            if (signed)
            {
                maxInt--;
                if ((encodedValue << 1) == maxInt - 1)
                    result = 0.5f * (max + min);
            }

            if (flag)
            {
                if (encodedValue == 0)
                    return min;
                if (encodedValue == max - 1)
                    return max;

                float y = (max - min) / (float)(max - 2);
                result = (float)(encodedValue - 1) * y + y * 0.5f + min;
            }
            else
            {
                float y = (max - min) / (float)max;
                result = (float)encodedValue * y + y * 0.5f + min;
            }

            if (isRounded)
            {
                int rounded = (int)(result + 0.5f);
                if (Math.Abs((float)rounded - result) <= .002f)
                    result = (float)rounded;
            }

            return result;
        }

        /// <summary>
        /// Seeks to an offset within a byte in the stream.
        /// </summary>
        /// <param name="bytePos">The position of the byte to seek inside.</param>
        /// <param name="bitOffset">The offset of the bit from the start of the byte to seek to. 0 = MSB.</param>
        public void Seek(long bytePos, int bitOffset)
        {
            Contract.Requires<ArgumentException>(bytePos >= 0);
            Contract.Requires<ArgumentException>(bitOffset >= 0 && bitOffset < 8);

            BaseStream.Position = bytePos;
            this.currentBitPos = bytePos * 8 + bitOffset;
            ResetWindow(); // Technically, we might not need to reset the window and cache a new byte if the seek position is close to our current position, but meh
            CacheByte();
            this.currentBit = bitOffset;
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
            var b = BaseStream.ReadByte();
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
    }
}
