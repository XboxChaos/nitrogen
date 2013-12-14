using Nitrogen.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace Nitrogen.IO
{
    [ContractVerification(true)]
    public class BinaryWriter
        : IDisposable
    {
        private Stream _stream;
        private ByteOrder _endianness;

        private readonly bool _leaveOpen;

        public BinaryWriter(Stream output, ByteOrder endianness = ByteOrder.Default, bool leaveOpen = false)
        {
            Contract.Requires(output != null);
            Contract.Requires(endianness.IsDefined());

            _stream = output;
            _endianness = endianness;
            _leaveOpen = leaveOpen;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="BinaryWriter"/> class from being created
        /// except in derived classes.
        /// </summary>
        protected BinaryWriter() { }

        /// <summary>
        /// Gets the underlying stream.
        /// </summary>
        public virtual Stream BaseStream { get { return _stream; } }

        /// <summary>
        /// Gets or sets the endianness of the data to be written to the underlying stream.
        /// </summary>
		public ByteOrder Endianness
		{
			get { return _endianness; }
			set
			{
				Contract.Requires(value.IsDefined());

				if ( value == ByteOrder.Default )
					value = BitConverter.IsLittleEndian ? ByteOrder.LittleEndian : ByteOrder.BigEndian;

				_endianness = value;
			}
		}

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Write(bool value)
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanWrite);
            _stream.WriteByte((byte)(value ? 1 : 0));
        }

        public virtual void Write(sbyte value)
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanWrite);
            _stream.WriteByte((byte)value);
        }

        public virtual void Write(byte value)
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanWrite);
            _stream.WriteByte(value);
        }

        public virtual void Write(short value)
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanWrite);
            Write(value, sizeof(short));
        }

        public virtual void Write(ushort value)
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanWrite);
            Write(value, sizeof(ushort));
        }

        public virtual void Write(int value)
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanWrite);
            Write(value, sizeof(int));
        }

        public virtual void Write(uint value)
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanWrite);
            Write(value, sizeof(uint));
        }

        public virtual void Write(long value)
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanWrite);
            Write(value, sizeof(long));
        }

        public virtual void Write(ulong value)
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanWrite);
            Write(value, sizeof(ulong));
        }

        public virtual void Write(float value)
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanWrite);
            Write((uint)value, sizeof(float));
        }

        public virtual void Write(double value)
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanWrite);
            Write((ulong)value, sizeof(double));
        }

        public virtual void Write(DateTime value)
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanWrite);
            Write(value.ToUnixTime());
        }

        /// <summary>
        /// Writes a null-terminated string to the underlying stream in the specified <paramref name="encoding"/>.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="encoding">The character encoding of the string value.</param>
        public virtual void WriteNullTerminatedString(string value, Encoding encoding, int maxLength = 0)
        {
            Contract.Requires<ArgumentNullException>(value != null && encoding != null);

            value += "\0";
            byte[] encodedValue = new byte[encoding.GetByteCount(value)];
            Array.Copy(encoding.GetBytes(value), encodedValue, encodedValue.Length);
            if (maxLength > 0) { Array.Resize(ref encodedValue, maxLength); }
            _stream.Write(encodedValue, 0, encodedValue.Length);
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
        /// <param name="nullTerminated">
        /// <c>true</c> if the string is null-terminated; otherwise, <c>false</c>.
        /// </param>
        public virtual void WriteString(string value, Encoding encoding, int length)
        {
            Contract.Requires<ArgumentNullException>(value != null && encoding != null);
            Contract.Requires<ArgumentOutOfRangeException>(length >= 0);
            Contract.Requires<ArgumentException>(encoding.GetByteCount(value) <= length);

            byte[] encodedValue = encoding.GetBytes(value);
            _stream.Write(encodedValue, 0, encodedValue.Length);

            if (encodedValue.Length < length)
            {
                byte[] padding = new byte[length - encodedValue.Length];
                _stream.Write(padding, 0, padding.Length);
            }
        }

        public void Write(IList<byte> values, int offset, int length)
        {
            Contract.Requires<ArgumentNullException>(values != null);
            Contract.Requires<ArgumentOutOfRangeException>(offset >= 0 && length >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(offset + length <= values.Count);

            for (int i = offset; i < length; i++)
                Write(values[i]);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_leaveOpen && _stream != null)
                {
                    _stream.Dispose();
                    _stream = null;
                }
            }
        }

        private void Write(long value, int byteLength)
        {
            Contract.Requires<ArgumentOutOfRangeException>(byteLength > 0);
            Write((ulong)value, byteLength);
        }

        private void Write(ulong value, int byteLength)
        {
            Contract.Requires<ArgumentOutOfRangeException>(byteLength > 0);

            byte[] buffer = new byte[byteLength];
            for (int i = 0; i < byteLength; i++)
            {
                int offset = i * 8;
                if (Endianness == ByteOrder.BigEndian)
                    offset = (byteLength - i - 1) * 8;

                buffer[i] = (byte)((value >> offset) & 0xFF);
            }
            _stream.Write(buffer, 0, byteLength);
        }
    }
}
