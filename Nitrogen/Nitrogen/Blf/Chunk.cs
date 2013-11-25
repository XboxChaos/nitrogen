/*
 *   Nitrogen - Halo Content API
 *   Copyright © 2013 The Nitrogen Authors. All rights reserved.
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

using Nitrogen.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;

namespace Nitrogen.Blf
{
    public abstract class Chunk
        : ISerializable<EndianStream>
    {
        private int signature;
        private int length;
        private short version;
        private MemoryStream buffer;

        protected Chunk(string signature, short version, int? length = null)
        {
            byte[] sigBytes = Encoding.ASCII.GetBytes(signature);
            if (sigBytes.Length != 4)
                throw new ArgumentException("Invalid signature '" + signature + "'", "signature");

            if (BitConverter.IsLittleEndian)
                Array.Reverse(sigBytes);

            this.signature = BitConverter.ToInt32(sigBytes, 0);
            this.version = version;
            this.buffer = new MemoryStream();

            if (length.HasValue)
                this.length = length.Value;
            else
                RecalculateLength = true;
        }

        [Flags]
        protected enum ChunkFlags
            : short
        {
            None            = 0,
            IsInitialized   = 1 << 0,
            IsHeader        = 1 << 1,
        }

        /// <summary>
        /// Gets the flags of this chunk.
        /// </summary>
        protected virtual ChunkFlags Flags { get { return ChunkFlags.IsInitialized; } }

        /// <summary>
        /// Gets or sets a boolean value specifying whether the chunk length should be recalculated.
        /// Set this to true for large, variable-length data such as screenshot images and film data.
        /// </summary>
        protected bool RecalculateLength { get; set; }

        protected abstract void SerializeEndianStreamData(EndianStream s);

        public void Serialize(EndianStream s)
        {
            long offset = s.Position;

            // Read chunk data into the buffer when deserializing.
            if (s.State == StreamState.Read)
            {
                s.Seek(offset + 4, SeekOrigin.Begin);
                s.Reader.Read(out this.length);
                s.Seek(offset + 12, SeekOrigin.Begin);

                byte[] temp = new byte[this.length - 12];
                s.Reader.Read(temp, temp.Length);
                this.buffer.Write(temp, 0, temp.Length);

                s.Position = offset;
            }
            else if (s.State == StreamState.Write) // Clear the buffer in case
            {
                this.buffer.SetLength(0);
            }

            // Serialize the buffer.
            this.buffer.Position = 0;
            ByteOrder endianness = (s.IsLittleEndian ? ByteOrder.LittleEndian : ByteOrder.BigEndian);
            using (var stream = new EndianStream(this.buffer, s.State, endianness, true))
                SerializeEndianStreamData(stream);

            if (RecalculateLength)
                this.length = (int)this.buffer.Length + 12;

            s.Stream(ref this.signature);
            s.Stream(ref this.length);
            s.Stream(ref this.version);
            short flags = (short)Flags;
            s.Stream(ref flags);
            s.Stream(this.buffer.ToArray(), 0, (int)this.buffer.Length);

            // Move the stream to the end of this chunk.
            s.Position = offset + this.length;
        }
    }
}
