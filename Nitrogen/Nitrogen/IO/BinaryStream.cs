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
    public abstract class BinaryStream
        : Stream, IDisposable
    {
        private bool leaveOpen;
        private Stream stream;

        protected BinaryStream(Stream stream, StreamState initialState, bool leaveOpen = false)
        {
            Contract.Requires<ArgumentNullException>(stream != null);
            Contract.Requires<ArgumentException>(stream.CanWrite && stream.CanRead);
            Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(StreamState), initialState));

            this.leaveOpen = leaveOpen;
            this.stream = stream;
            State = initialState;
        }

        public StreamState State { get; set; }

        public abstract BinaryReader Reader { get; }

        public abstract BinaryWriter Writer { get; }

        public virtual void Stream(ref bool value)
        {
            if (State == StreamState.Read)
                Reader.Read(out value);
            else if (State == StreamState.Write)
                Writer.Write(value);
        }

        public virtual void Stream(ref sbyte value)
        {
            if (State == StreamState.Read)
                Reader.Read(out value);
            else if (State == StreamState.Write)
                Writer.Write(value);
        }

        public virtual void Stream(ref byte value)
        {
            if (State == StreamState.Read)
                Reader.Read(out value);
            else if (State == StreamState.Write)
                Writer.Write(value);
        }

        public virtual void Stream(ref short value)
        {
            if (State == StreamState.Read)
                Reader.Read(out value);
            else if (State == StreamState.Write)
                Writer.Write(value);
        }

        public virtual void Stream(ref ushort value)
        {
            if (State == StreamState.Read)
                Reader.Read(out value);
            else if (State == StreamState.Write)
                Writer.Write(value);
        }

        public virtual void Stream(ref int value)
        {
            if (State == StreamState.Read)
                Reader.Read(out value);
            else if (State == StreamState.Write)
                Writer.Write(value);
        }

        public virtual void Stream(ref uint value)
        {
            if (State == StreamState.Read)
                Reader.Read(out value);
            else if (State == StreamState.Write)
                Writer.Write(value);
        }

        public virtual void Stream(ref long value)
        {
            if (State == StreamState.Read)
                Reader.Read(out value);
            else if (State == StreamState.Write)
                Writer.Write(value);
        }

        public virtual void Stream(ref ulong value)
        {
            if (State == StreamState.Read)
                Reader.Read(out value);
            else if (State == StreamState.Write)
                Writer.Write(value);
        }

        public virtual void Stream(ref float value)
        {
            if (State == StreamState.Read)
                Reader.Read(out value);
            else if (State == StreamState.Write)
                Writer.Write(value);
        }

        public virtual void Stream(ref double value)
        {
            if (State == StreamState.Read)
                Reader.Read(out value);
            else if (State == StreamState.Write)
                Writer.Write(value);
        }

        public virtual void Stream(ref DateTime value)
        {
            if (State == StreamState.Read)
                Reader.Read(out value);
            else if (State == StreamState.Write)
                Writer.Write(value);
        }

        public virtual void Stream(IList<sbyte> values, int offset, int count)
        {
            for (int i = offset; i < offset + count; i++)
            {
                sbyte value = values[i];
                Stream(ref value);
                values[i] = value;
            }
        }

        public virtual void Stream(IList<byte> values, int offset, int count)
        {
            for (int i = offset; i < offset + count; i++)
            {
                byte value = values[i];
                Stream(ref value);
                values[i] = value;
            }
        }

        public virtual void Stream(IList<short> values, int offset, int count)
        {
            for (int i = offset; i < offset + count; i++)
            {
                short value = values[i];
                Stream(ref value);
                values[i] = value;
            }
        }

        public virtual void Stream(IList<ushort> values, int offset, int count)
        {
            for (int i = offset; i < offset + count; i++)
            {
                ushort value = values[i];
                Stream(ref value);
                values[i] = value;
            }
        }

        public virtual void Stream(IList<int> values, int offset, int count)
        {
            for (int i = offset; i < offset + count; i++)
            {
                int value = values[i];
                Stream(ref value);
                values[i] = value;
            }
        }

        public virtual void Stream(IList<uint> values, int offset, int count)
        {
            for (int i = offset; i < offset + count; i++)
            {
                uint value = values[i];
                Stream(ref value);
                values[i] = value;
            }
        }

        public virtual void Stream(IList<long> values, int offset, int count)
        {
            for (int i = offset; i < offset + count; i++)
            {
                long value = values[i];
                Stream(ref value);
                values[i] = value;
            }
        }

        public virtual void Stream(IList<ulong> values, int offset, int count)
        {
            for (int i = offset; i < offset + count; i++)
            {
                ulong value = values[i];
                Stream(ref value);
                values[i] = value;
            }
        }

        public virtual void Stream(IList<float> values, int offset, int count)
        {
            for (int i = offset; i < offset + count; i++)
            {
                float value = values[i];
                Stream(ref value);
                values[i] = value;
            }
        }

        public virtual void Stream(IList<double> values, int offset, int count)
        {
            for (int i = offset; i < offset + count; i++)
            {
                double value = values[i];
                Stream(ref value);
                values[i] = value;
            }
        }

        public virtual void StreamNullTerminatedString(ref string output, Encoding encoding, long maxLength = 0)
        {
            if (State == StreamState.Read)
                Reader.Read(out output, encoding, maxLength);
            else
                Writer.Write(output, encoding, maxLength);
        }

        public virtual void StreamString(ref string output, Encoding encoding, int length)
        {
            if (State == StreamState.Read)
                Reader.Read(out output, encoding, length);
            else
                Writer.Write(output, encoding, length);
        }

        #region Stream Members

        public override bool CanRead { get { return this.stream.CanRead; } }

        public override bool CanSeek { get { return this.stream.CanSeek; } }

        public override bool CanWrite { get { return this.stream.CanWrite; } }

        public override long Length { get { return this.stream.Length; } }

        public override long Position
        {
            get { return this.stream.Position; }
            set { this.stream.Position = value; }
        }

        public override void Flush() { this.stream.Flush(); }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.stream.Write(buffer, offset, count);
        }

        #endregion

        #region IDisposable Members

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (this.stream != null && !this.leaveOpen)
                {
                    this.stream.Dispose();
                }

                this.stream = null;
            }
        }

        #endregion
    }
}
