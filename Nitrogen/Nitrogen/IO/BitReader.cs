using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace Nitrogen.IO
{
    [ContractVerification(true)]
    public class BitReader
        : IDisposable
    {
        private const int WindowSize = sizeof(ulong) * 8;

        private Stream _stream;
        private ulong _window;
        private int _bitsAvailable;
        private byte _currentByte;
        private long _currentBitPos;
        private int _currentBit = 8;

        private readonly bool _leaveOpen;

        public BitReader(Stream stream, bool leaveOpen = false)
        {
            Contract.Requires(stream != null);
            _stream = stream;
            _leaveOpen = leaveOpen;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="BitReader"/> class from being created
        /// except in derived classes.
        /// </summary>
        protected BitReader() { }

        /// <summary>
        /// Gets the underlying stream.
        /// </summary>
        public virtual Stream BaseStream { get { return _stream; } }

        /// <summary>
        /// Extends the sign of an unsigned integer.
        /// </summary>
        /// <param name="val">A value to sign-extend.</param>
        /// <param name="size">The original width of the value in bits.</param>
        /// <returns>The signed integer.</returns>
        private static long ExtendSign(ulong val, int size)
        {
            if (size < WindowSize && (val & (1UL << (size - 1))) != 0)
            {
                // Value is negative - fill the left with ones
                return (long)(val | (ulong.MaxValue << size));
            }
            return (long)val;
        }

        /// <summary>
        /// Reads a signed bits-bit integer from the stream.
        /// </summary>
        /// <param name="bits">The number of bits to read.</param>
        /// <returns>The integer that was read.</returns>
        public virtual long ReadIntN(int n)
        {
            Contract.Requires<ArgumentOutOfRangeException>(n > 0 && n <= WindowSize);
            var unsigned = ReadUIntN(n);
            return ExtendSign(unsigned, n);
        }

        /// <summary>
        /// Reads an unsigned bits-bit integer from the stream.
        /// </summary>
        /// <param name="bits">The number of bits to read.</param>
        /// <returns>The integer that was read.</returns>
        public virtual ulong ReadUIntN(int n)
        {
            Contract.Requires<ArgumentOutOfRangeException>(n > 0 && n <= WindowSize);

            EnsureBitsAvailable(n);

            // Pull bits from the left of the window
            var result = _window >> (WindowSize - n);
            if (n < WindowSize)
                _window <<= n;
            else
                _window = 0;
            _bitsAvailable -= n;
            _currentBitPos += n;

            return result;
        }

        public bool ReadBit()
        {
            return ReadUIntN(1) == 1;
        }

        public DateTime ReadDateTime()
        {
            Contract.Requires(BaseStream != null && BaseStream.CanRead);
			return ReadIntN(sizeof(long) * 8).ToDateTime();
        }

        /// <summary>
        /// Reads an array of bytes from the stream.
        /// </summary>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The byte array that was read.</returns>
        public byte[] ReadBytes(int count)
        {
            Contract.Requires<ArgumentOutOfRangeException>(count > 0);

            var result = new byte[count];
            for (var i = 0; i < count; i++)
                result[i] = (byte)ReadUIntN(8);
            return result;
        }

        /// <summary>
        /// Reads bytes from the stream into an existing array.
        /// </summary>
        /// <param name="buffer">The array to store the read bytes to.</param>
        /// <param name="offset">The index of the element to start storing bytes to.</param>
        /// <param name="count">The number of bytes to read.</param>
        public void ReadBytes(byte[] buffer, int offset, int count)
        {
            for (var i = 0; i < count; i++)
                buffer[offset + i] = (byte)ReadUIntN(8);
        }

		public byte ReadByte ()
		{
			return (byte) ReadUIntN(sizeof(byte));
		}

		public sbyte ReadSByte ()
		{
			return (sbyte) ReadIntN(sizeof(sbyte));
		}

        public virtual string ReadNullTerminatedString(Encoding encoding, int maxLength)
        {
            Contract.Requires<ArgumentNullException>(encoding != null);
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanRead);

            int delimiterSize = encoding.GetByteCount("\0");
            byte[] buffer = new byte[delimiterSize];
            var builder = new StringBuilder();
            long max = maxLength > 0 ? (_currentBitPos + (maxLength * 8)) : _stream.Length * 8;
            while (_currentBitPos < max)
            {
                ReadBytes(buffer, 0, buffer.Length);
                string value = encoding.GetString(buffer, 0, buffer.Length);
                if (value == "\0")
                    break;
                builder.Append(value);
            }
            return builder.ToString();
        }

        public virtual string ReadString(Encoding encoding, int length, bool trimmed = true)
        {
            Contract.Requires<ArgumentNullException>(encoding != null);
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanRead);
            Contract.Requires<ArgumentNullException>(length >= 0);

            byte[] buffer = new byte[length];
            ReadBytes(buffer, 0, buffer.Length);
            return encoding.GetString(buffer, 0, buffer.Length).Trim('\0');
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Reads a byte from the stream and caches it.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the byte was successfully cached; otherwise, <c>false</c>.
        /// </returns>
        private bool CacheByte()
        {
            var b = BaseStream.ReadByte();
            if (b == -1)
                return false;
            _currentByte = (byte)b;
            _currentBit = 0;
            return true;
        }

        /// <summary>
        /// Resets the cache window.
        /// </summary>
        private void ResetWindow()
        {
            _window = 0;
            _bitsAvailable = 0;
        }

        /// <summary>
        /// Ensures that a specified number of bits is available in the window. If not, the window
        /// is refilled as much as possible.
        /// </summary>
        /// <param name="count">The number of bits needed.</param>
        private void EnsureBitsAvailable(int count)
        {
            Contract.Requires<ArgumentOutOfRangeException>(count > 0 && count <= WindowSize);

            if (_bitsAvailable < count)
            {
                // Refill as much of the window as possible
                while (_bitsAvailable < WindowSize)
                {
                    if (_currentBit == 8 && !CacheByte())
                    {
                        //if (_bitsAvailable < count)
                        //    throw new EndOfStreamException("Unexpected end of stream encountered");

                        break;
                    }

                    // We can only extract at most 8 bits from the current byte
                    var extractCount = Math.Min(8 - _currentBit, WindowSize - _bitsAvailable);

                    // Shift the byte over and mask out any extra bits to the left
                    var bits = (ulong)((_currentByte >> (8 - _currentBit - extractCount)) & (0xFF >> (8 - extractCount)));

                    // Add it to the window
                    _window |= bits << (WindowSize - _bitsAvailable - extractCount);

                    // Advance by the number of bits read
                    _bitsAvailable += extractCount;
                    _currentBit += extractCount;
                }
            }
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
    }
}
