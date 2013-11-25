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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace Nitrogen.IO
{
    public class EndianStream
        : BinaryStream
    {
        private ByteOrder endianness;
        private Lazy<EndianReader> reader;
        private Lazy<EndianWriter> writer;

        public EndianStream(Stream stream, StreamState initialState, ByteOrder endianness, bool leaveOpen = false)
            : base(stream, initialState, leaveOpen)
        {
            Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(ByteOrder), endianness));

            this.endianness = endianness;
            this.reader = new Lazy<EndianReader>(() => new EndianReader(this, leaveOpen));
            this.writer = new Lazy<EndianWriter>(() => new EndianWriter(this, leaveOpen));
        }

        /// <summary>
        /// Gets a value indicating whether data will be written to the underlying stream in little
        /// endian format.
        /// </summary>
        public bool IsLittleEndian
        {
            get
            {
                if (this.endianness == ByteOrder.Default)
                    return BitConverter.IsLittleEndian;

                return this.endianness == ByteOrder.LittleEndian;
            }
        }

        public override BinaryReader Reader { get { return this.reader.Value; } }

        public override BinaryWriter Writer { get { return this.writer.Value; } }

        public void SetEndianness(ByteOrder endianness)
        {
            this.endianness = endianness;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (IsLittleEndian)
                Array.Reverse(buffer);

            base.Write(buffer, offset, count);
        }

        public void Pad(int byteCount)
        {
            Stream(new byte[byteCount], 0, byteCount);
        }
    }
}
