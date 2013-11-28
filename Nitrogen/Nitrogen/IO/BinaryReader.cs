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
using System.IO;
using System.Text;

namespace Nitrogen.IO
{
    public class BinaryReader
        : IDisposable
    {
        private const int BufferSize = sizeof(ulong);

        private Stream stream;
        private bool leaveOpen;
        private byte[] buffer;

        public BinaryReader(Stream stream, bool leaveOpen)
        {
            this.stream = stream;
            this.leaveOpen = leaveOpen;
            this.buffer = new byte[BufferSize];
        }

        private BinaryReader() { }

        protected Stream BaseStream { get { return this.stream; } }

        public virtual int Read(out bool output)
        {
            int count = this.stream.Read(this.buffer, 0, sizeof(bool));
            output = this.buffer[0] == 1;
            return count;
        }

        public virtual int Read(out sbyte output)
        {
            int count = this.stream.Read(this.buffer, 0, sizeof(sbyte));
            output = (sbyte)this.buffer[0];
            return count;
        }

        public virtual int Read(out byte output)
        {
            int count = this.stream.Read(this.buffer, 0, sizeof(byte));
            output = this.buffer[0];
            return count;
        }

        public virtual int Read(out short output)
        {
            int count = this.stream.Read(this.buffer, 0, sizeof(short));
            output = (short)((this.buffer[0] << 8) | this.buffer[1]);
            return count;
        }

        public virtual int Read(out ushort output)
        {
            int count = this.stream.Read(this.buffer, 0, sizeof(ushort));
            output = (ushort)((this.buffer[0] << 8) | this.buffer[1]);
            return count;
        }

        public virtual int Read(out int output)
        {
            int count = this.stream.Read(this.buffer, 0, sizeof(int));
            output = (int)((this.buffer[0] << 24) | (this.buffer[1] << 16) | (this.buffer[2] << 8) | this.buffer[3]);
            return count;
        }

        public virtual int Read(out uint output)
        {
            int count = this.stream.Read(this.buffer, 0, sizeof(uint));
            output = (uint)((this.buffer[0] << 24) | (this.buffer[1] << 16) | (this.buffer[2] << 8) | this.buffer[3]);
            return count;
        }

        public virtual int Read(out long output)
        {
            uint one, two;
            int count1 = Read(out one);
            int count2 = Read(out two);
            output = (one << 32) | two;
            return count1 + count2;
        }

        public virtual int Read(out ulong output)
        {
            uint one, two;
            int count1 = Read(out one);
            int count2 = Read(out two);
            output = (one << 32) | two;
            return count1 + count2;
        }

        public virtual int Read(out float output)
        {
            throw new NotImplementedException();
        }

        public virtual int Read(out double output)
        {
            throw new NotImplementedException();
        }

        public virtual int Read(out DateTime output)
        {
            ulong totalSeconds;
            int count = Read(out totalSeconds);
            output = Utilities.UnixEpoch.AddSeconds(totalSeconds);
            return count;
        }

        public virtual int Read(IList<sbyte> output, int length)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                sbyte value;
                count += Read(out value);
                output[i] = value;
            }
            return count;
        }

        public virtual int Read(IList<byte> output, int length)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                byte value;
                count += Read(out value);
                output[i] = value;
            }
            return count;
        }

        public virtual int Read(IList<short> output, int length)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                short value;
                count += Read(out value);
                output[i] = value;
            }
            return count;
        }

        public virtual int Read(IList<ushort> output, int length)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                ushort value;
                count += Read(out value);
                output[i] = value;
            }
            return count;
        }

        public virtual int Read(IList<int> output, int length)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                int value;
                count += Read(out value);
                output[i] = value;
            }
            return count;
        }

        public virtual int Read(IList<uint> output, int length)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                uint value;
                count += Read(out value);
                output[i] = value;
            }
            return count;
        }

        public virtual int Read(IList<long> output, int length)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                long value;
                count += Read(out value);
                output[i] = value;
            }
            return count;
        }

        public virtual int Read(IList<ulong> output, int length)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                ulong value;
                count += Read(out value);
                output[i] = value;
            }
            return count;
        }

        public virtual int Read(IList<float> output, int length)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                float value;
                count += Read(out value);
                output[i] = value;
            }
            return count;
        }

        public virtual int Read(IList<double> output, int length)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                double value;
                count += Read(out value);
                output[i] = value;
            }
            return count;
        }

        public virtual int Read(out string output, Encoding encoding, long maxLength = 0)
        {
            int count = 0;
            int delimiterSize = encoding.GetByteCount("\0");
            byte[] buffer = new byte[delimiterSize];
            var builder = new StringBuilder();
            long max = maxLength > 0 ? this.stream.Position + maxLength : this.stream.Length;
            while (this.stream.Position < max)
            {
                count += this.stream.Read(buffer, 0, buffer.Length);
                string value = encoding.GetString(buffer);
                if (value == "\0")
                    break;
                builder.Append(value);
            }
            output = builder.ToString();
            return count;
        }

        public virtual int Read(out string output, Encoding encoding, int length)
        {
            byte[] buffer = new byte[length];
            int count = this.stream.Read(buffer, 0, buffer.Length);
            output = encoding.GetString(buffer).Trim('\0');
            return count;
        }

        public virtual void Dispose()
        {
            if (this.stream != null && !this.leaveOpen)
                this.stream.Dispose();

            this.stream = null;
            this.buffer = null;
        }
    }
}
