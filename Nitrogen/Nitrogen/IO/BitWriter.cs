using Nitrogen.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;

namespace Nitrogen.IO
{
    [ContractVerification(true)]
    public class BitWriter
        : IDisposable
    {
        private const int WindowSize = sizeof(ulong) * 8;

        private ulong _window;
        private int _windowPos;
        private Stream _stream;

        private readonly bool _leaveOpen;

        public BitWriter(Stream stream, bool leaveOpen = false)
        {
            Contract.Requires(stream != null);
            _stream = stream;
            _leaveOpen = leaveOpen;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="BitWriter"/> class from being created
        /// except in derived classes.
        /// </summary>
        protected BitWriter() { }

        ~BitWriter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the underlying stream.
        /// </summary>
        public virtual Stream BaseStream { get { return _stream; } }

        public void Flush()
        {
            Contract.Requires<InvalidOperationException>(BaseStream != null);

            var bytes = BitConverter.GetBytes(_window);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            int length = (_windowPos + 7) / 8;
            _stream.Write(bytes, 0, length);
            _window = 0;
            _windowPos = 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void WriteBit(bool value)
        {
            Write(value ? 1 : 0, 1);
        }

        public virtual void Write(DateTime value)
        {
            Contract.Requires(BaseStream != null && BaseStream.CanWrite);
            Write((ulong)value.ToUnixTime(), sizeof(ulong) * 8);
        }

        public void Write(ulong value, int bits)
        {
            Contract.Requires<ArgumentOutOfRangeException>(bits > 0 && bits <= WindowSize);

            while (bits > 0)
            {
                _window |= ((value >> (bits - 1)) & 1) << (WindowSize - 1 - _windowPos);
                _windowPos++;
                bits--;

                if (_windowPos == WindowSize)
                    Flush();
            }
        }

        public void Write(long value, int bits)
        {
            Contract.Requires<ArgumentOutOfRangeException>(bits > 0 && bits <= WindowSize);
            Write((ulong)value, bits);
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
			foreach ( byte b in encodedValue )
			{
				Write(b, 8);
			}
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
            foreach (byte b in encodedValue) { Write(b, 8); }

            if (encodedValue.Length < length)
            {
                byte[] padding = new byte[length - encodedValue.Length];
                foreach (byte b in padding) { Write(b, 8); }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            Flush();

            if (disposing)
            {
                if (!_leaveOpen && _stream != null)
                {
                    _stream.Dispose();
                    _stream = null;
                }
            }
        }
    }
}
