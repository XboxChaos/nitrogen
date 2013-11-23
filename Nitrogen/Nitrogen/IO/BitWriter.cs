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
using System.Text;

namespace Nitrogen.Core.IO
{
    public sealed class BitWriter
        : BinaryWriter
    {
        private const int WindowSize = sizeof(ulong) * 8;

        private ulong window;
        private int windowPos;

        public BitWriter(BitStream stream, bool leaveOpen = false)
            : base(stream, leaveOpen) { }

        public void Flush()
        {
            var bytes = BitConverter.GetBytes(this.window);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            int length = (this.windowPos + 7) / 8;
            BaseStream.Write(bytes, 0, length);
            this.window = 0;
            this.windowPos = 0;
        }

        public override void Dispose()
        {
            Flush();
            base.Dispose();
        }

        public void Write(ulong value, int bits)
        {
            Contract.Requires<ArgumentOutOfRangeException>(bits > 0 && bits <= WindowSize);

            while (bits > 0)
            {
                this.window |= ((value >> (bits - 1)) & 1) << (WindowSize - 1 - this.windowPos);
                this.windowPos++;
                bits--;

                if (this.windowPos == WindowSize)
                    Flush();
            }
        }

        public void Write(long value, int bits)
        {
            Contract.Requires<ArgumentOutOfRangeException>(bits > 0 && bits <= WindowSize);
            Write((ulong)value, bits);
        }

        public override void Write(bool value)
        {
            Write(value ? 1 : 0, 1);
        }

        public override void Write(byte value)
        {
            Write(value, 8);
        }

        public override void Write(sbyte value)
        {
            Write(value, 8);
        }

        public override void Write(short value)
        {
            Write(value, 16);
        }

        public override void Write(ushort value)
        {
            Write(value, 16);
        }

        public override void Write(int value)
        {
            Write(value, 32);
        }

        public override void Write(uint value)
        {
            Write(value, 32);
        }

        public override void Write(long value)
        {
            Write(value, 64);
        }

        public override void Write(ulong value)
        {
            Write(value, 64);
        }

        public void WriteEncodedFloat(float value, int n, float min, float max, bool signed, bool isRounded = true, bool flag = true)
        {
            uint maxInt = (uint)(1 << n);
            if (signed)
            {
                maxInt--;
                if (value == 0.5f * (max + min))
                {
                    Write((maxInt - 1) >> 1, n);
                    return;
                }
            }

            if (flag)
            {
                if (value == min)
                {
                    Write(0, n);
                    return;
                }
                    
                if (value == max)
                {
                    Write(maxInt - 1, n);
                    return;
                }

                float y = (max - min) / (float)(max - 2);
                Write((uint)((value - min - y * 0.5f) / y + 1), n);
            }
            else
            {
                float y = (max - min) / (float)max;
                Write((uint)((value - min - y * 0.5f) / y), n);
            }
        }
    }
}
