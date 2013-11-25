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
    public class BinaryWriter
        : IDisposable
    {
        private bool leaveOpen;
        private Stream stream;

        public BinaryWriter(Stream stream, bool leaveOpen)
        {
            this.stream = stream;
            this.leaveOpen = leaveOpen;
        }

        private BinaryWriter() { }

        protected Stream BaseStream { get { return this.stream; } }

        /// <summary>
        /// Writes a boolean value to the underlying stream as a byte and advances the stream position
        /// by one byte.
        /// </summary>
        /// <param name="value">The boolean value to write.</param>
        public virtual void Write(bool value)
        {
            this.stream.WriteByte((byte)(value ? 1 : 0));
        }

        /// <summary>
        /// Writes a signed byte to the underlying stream and advances the stream position by one byte.
        /// </summary>
        /// <param name="value">The signed byte to write.</param>
        public virtual void Write(sbyte value)
        {
            Write((byte)value);
        }

        /// <summary>
        /// Writes an unsigned byte to the underlying stream and advances the stream position by one byte.
        /// </summary>
        /// <param name="value">The unsigned byte to write.</param>
        public virtual void Write(byte value)
        {
            this.stream.WriteByte(value);
        }

        /// <summary>
        /// Writes a two-byte signed integer to the underlying stream and advances the stream position
        /// by two bytes.
        /// </summary>
        /// <param name="value">The two-byte signed integer to write.</param>
        public virtual void Write(short value)
        {
            Write((ushort)value);
        }

        public virtual void Write(ushort value)
        {
            WriteByteParams(
                (byte)(value >> 8),
                (byte)(value & 0xFF)
            );
        }

        /// <summary>
        /// Writes a four-byte signed integer to the underlying stream and advances the stream position
        /// by four bytes.
        /// </summary>
        /// <param name="value">The four-byte signed integer to write.</param>
        public virtual void Write(int value)
        {
            Write((uint)value);
        }

        public virtual void Write(uint value)
        {
            WriteByteParams(
                (byte)(value >> 24),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF)
            );
        }

        /// <summary>
        /// Writes an eight-byte signed integer to the underlying stream and advances the stream position
        /// by eight bytes.
        /// </summary>
        /// <param name="value">The eight-byte signed integer to write.</param>
        public virtual void Write(long value)
        {
            Write((ulong)value);
        }

        public virtual void Write(ulong value)
        {
            WriteByteParams(
                (byte)(value >> 56),
                (byte)((value >> 48) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF)
            );
        }

        public virtual void Write(float value)
        {
            Write((uint)value);
        }

        public virtual void Write(double value)
        {
            Write((ulong)value);
        }

        public virtual void Write(DateTime value)
        {
            double totalSeconds = value.Subtract(Utilities.UnixEpoch).TotalSeconds;
            Write(totalSeconds);
        }

        public virtual void Write(IEnumerable<sbyte> values)
        {
            foreach (var value in values) { Write(value); }
        }

        public virtual void Write(IEnumerable<byte> values)
        {
            foreach (var value in values) { Write(value); }
        }

        public virtual void Write(IEnumerable<short> values)
        {
            foreach (var value in values) { Write(value); }
        }

        public virtual void Write(IEnumerable<ushort> values)
        {
            foreach (var value in values) { Write(value); }
        }

        public virtual void Write(IEnumerable<int> values)
        {
            foreach (var value in values) { Write(value); }
        }

        public virtual void Write(IEnumerable<uint> values)
        {
            foreach (var value in values) { Write(value); }
        }

        public virtual void Write(IEnumerable<long> values)
        {
            foreach (var value in values) { Write(value); }
        }

        public virtual void Write(IEnumerable<ulong> values)
        {
            foreach (var value in values) { Write(value); }
        }

        public virtual void Write(IEnumerable<float> values)
        {
            foreach (var value in values) { Write(value); }
        }

        public virtual void Write(IEnumerable<double> values)
        {
            foreach (var value in values) { Write(value); }
        }

        /// <summary>
        /// Writes a null-terminated string to the underlying stream in the specified <paramref name="encoding"/>.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="encoding">The character encoding of the string value.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> or <paramref name="encoding"/> is null.
        /// </exception>
        public virtual void Write(string value, Encoding encoding)
        {
            Write(encoding.GetBytes(value));
            Write(encoding.GetBytes("\0"));
        }

        /// <summary>
        /// Writes a fixed-length string to the underlying stream in the specified <paramref name="encoding"/>.
        /// The value is to be padded with null bytes to meet the desired <paramref name="length"/>.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="encoding">The character encoding of the string value.</param>
        /// <param name="length">
        /// The length (in bytes) of the string value to be written to the underlying stream.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> or <paramref name="encoding"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// A negative number is passed to <paramref name="length"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Encoded string length exceeds <paramref name="length"/>.
        /// </exception>
        public virtual void Write(string value, Encoding encoding, int length)
        {
            byte[] encodedValue = encoding.GetBytes(value);
            Write(encodedValue);
            Write(new byte[length - encodedValue.Length]);
        }

        #region IDisposable Members

        public virtual void Dispose()
        {
            // TODO: Implement
        }

        #endregion

        private void WriteByteParams(params byte[] values)
        {
            this.Write(values);
        }
    }
}
