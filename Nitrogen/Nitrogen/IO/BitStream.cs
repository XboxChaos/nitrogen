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
    public class BitStream
        : BinaryStream
    {
        private Lazy<BitReader> reader;
        private Lazy<BitWriter> writer;

        public BitStream(Stream stream, StreamState initialState, bool leaveOpen = false)
            : base(stream, initialState, leaveOpen)
        {
            this.reader = new Lazy<BitReader>(() => new BitReader(this, leaveOpen));
            this.writer = new Lazy<BitWriter>(() => new BitWriter(this, leaveOpen));
        }

        public override BinaryReader Reader { get { return this.reader.Value; } }

        public override BinaryWriter Writer { get { return this.writer.Value; } }

        public void Stream(ref byte value, int bits)
        {
            if (State == StreamState.Read)
            {
                ulong val;
                this.reader.Value.Read(out val, bits);
                value = (byte)val;
            }
            else if (State == StreamState.Write)
            {
                this.writer.Value.Write(value, bits);
            }
        }

        public void Stream(ref sbyte value, int bits)
        {
            if (State == StreamState.Read)
            {
                ulong val;
                this.reader.Value.Read(out val, bits);
                value = (sbyte)val;
            }
            else if (State == StreamState.Write)
            {
                this.writer.Value.Write(value, bits);
            }
        }

        public void Stream(ref ushort value, int bits)
        {
            if (State == StreamState.Read)
            {
                ulong val;
                this.reader.Value.Read(out val, bits);
                value = (ushort)val;
            }
            else if (State == StreamState.Write)
            {
                this.writer.Value.Write(value, bits);
            }
        }

        public void Stream(ref short value, int bits)
        {
            if (State == StreamState.Read)
            {
                ulong val;
                this.reader.Value.Read(out val, bits);
                value = (short)val;
            }
            else if (State == StreamState.Write)
            {
                this.writer.Value.Write(value, bits);
            }
        }

        public void Stream(ref uint value, int bits)
        {
            if (State == StreamState.Read)
            {
                ulong val;
                this.reader.Value.Read(out val, bits);
                value = (uint)val;
            }
            else if (State == StreamState.Write)
            {
                this.writer.Value.Write(value, bits);
            }
        }

        public void Stream(ref int value, int bits)
        {
            if (State == StreamState.Read)
            {
                ulong val;
                this.reader.Value.Read(out val, bits);
                value = (int)val;
            }
            else if (State == StreamState.Write)
            {
                this.writer.Value.Write(value, bits);
            }
        }

        public void Stream(ref ulong value, int bits)
        {
            if (State == StreamState.Read)
            {
                ulong val;
                this.reader.Value.Read(out val, bits);
                value = (ulong)val;
            }
            else if (State == StreamState.Write)
            {
                this.writer.Value.Write(value, bits);
            }
        }

        public void Stream(ref long value, int bits)
        {
            if (State == StreamState.Read)
            {
                ulong val;
                this.reader.Value.Read(out val, bits);
                value = (long)val;
            }
            else if (State == StreamState.Write)
            {
                this.writer.Value.Write(value, bits);
            }
        }

        public void Stream(IList<bool> values, int offset, int count)
        {
            for (int i = offset; i < offset + count; i++)
            {
                bool value = values[i];
                Stream(ref value);
                values[i] = value;
            }
        }

        public void Stream(ref float value, int bits, float min, float max, bool signed, bool rounded = true, bool flag = true)
        {
            if (State == StreamState.Read)
            {
                value = this.reader.Value.ReadEncodedFloat(bits, min, max, signed, rounded, flag);
            }
        }

        public void Pad(int bitCount)
        {
            Stream(new bool[bitCount], 0, bitCount);
        }
    }
}
