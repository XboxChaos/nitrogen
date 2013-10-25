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

namespace Nitrogen.Utilities.IO
{
    /// <summary>
    /// Writes big-endian integers of varying bit widths to a stream.
    /// </summary>
    public class BitWriter
        : IDisposable
    {
        private const int WindowSize = sizeof(ulong) * 8;

        private Stream baseStream;
        private bool leaveOpen;

        private ulong window;
        private int windowPos;

        /// <summary>
        /// Initializes a new instance of the <see cref="BitWriter"/> class based on the specified
        /// stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        public BitWriter(Stream input)
            : this(input, false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitWriter"/> class based on the specified
        /// stream, and optionally leaves the stream open.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="leaveOpen">
        /// <c>true</c> to leave the stream open after the <see cref="BitWriter"/> object is disposed;
        /// otherwise, <c>false</c>.
        /// </param>
        public BitWriter(Stream input, bool leaveOpen)
        {
            Contract.Requires<ArgumentNullException>(input != null);

            this.baseStream = input;
            this.leaveOpen = leaveOpen;
        }

        ~BitWriter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Exposes access to the underlying stream of the <see cref="BitWriter"/>.
        /// </summary>
        public virtual Stream BaseStream { get { return this.baseStream; } }

        public void WriteUIntN(int n, ulong value)
        {
            Contract.Requires<ArgumentOutOfRangeException>(n > 0 && n <= WindowSize);

            while (n > 0)
            {
                this.window |= ((value >> (n - 1)) & 1) << (WindowSize - 1 - this.windowPos);
                this.windowPos++;
                n--;

                if (this.windowPos == WindowSize)
                    Flush();
            }
        }

        public void WriteIntN(int n, long value)
        {
            Contract.Requires<ArgumentOutOfRangeException>(n > 0 && n <= WindowSize);

            WriteUIntN(n, (ulong)value);
        }

        public void Flush()
        {
            var bytes = BitConverter.GetBytes(this.window);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            int length = (this.windowPos + 7) / 8;
            this.baseStream.Write(bytes, 0, length);
            this.window = 0;
            this.windowPos = 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Flush();
                if (!this.leaveOpen)
                {
                    this.baseStream.Dispose();
                }
                this.baseStream = null;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
