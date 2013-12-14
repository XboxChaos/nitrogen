using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace Nitrogen.IO
{
    [ContractVerification(true)]
    public class BinaryReader
        : IDisposable
    {
        private Stream _stream;
        private ByteOrder _endianness;

        private readonly bool _leaveOpen;
        private readonly byte[] _buffer = new byte[sizeof(double)];

        public BinaryReader(Stream input, ByteOrder endianness = ByteOrder.Default, bool leaveOpen = false)
        {
            Contract.Requires(input != null);
            Contract.Requires(endianness.IsDefined());

            _stream = input;
            _endianness = endianness;
            _leaveOpen = leaveOpen;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="BinaryReader"/> class from being created
        /// except in derived classes.
        /// </summary>
        protected BinaryReader() { }

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

                if (value == ByteOrder.Default)
                    value = BitConverter.IsLittleEndian ? ByteOrder.LittleEndian : ByteOrder.BigEndian;

                _endianness = value;
            }
        }

        public virtual bool ReadBoolean()
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanRead);
            return Read(sizeof(byte)) == 1;
        }

        public virtual byte ReadByte()
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanRead);
            return (byte)Read(sizeof(byte));
        }

        public virtual sbyte ReadSByte()
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanRead);
            return (sbyte)Read(sizeof(sbyte));
        }

        public virtual short ReadInt16()
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanRead);
            return (short)Read(sizeof(short));
        }

        public virtual ushort ReadUInt16()
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanRead);
            return (ushort)Read(sizeof(ushort));
        }

        public virtual int ReadInt32()
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanRead);
            return (int)Read(sizeof(int));
        }

        public virtual uint ReadUInt32()
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanRead);
            return (uint)Read(sizeof(uint));
        }

        public virtual long ReadInt64()
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanRead);
            return (long)Read(sizeof(long));
        }

        public virtual ulong ReadUInt64()
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanRead);
            return Read(sizeof(ulong));
        }

        public virtual DateTime ReadDateTime()
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanRead);
            return ((long)Read(sizeof(long))).ToDateTime();
        }

        public virtual float ReadSingle()
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanRead);
            return Read(sizeof(float));
        }

        public virtual double ReadDouble()
        {
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanRead);
            return Read(sizeof(double));
        }

        public virtual string ReadNullTerminatedString(Encoding encoding, int maxLength)
        {
            Contract.Requires<ArgumentNullException>(encoding != null);
            Contract.Requires<IOException>(BaseStream != null && BaseStream.CanRead);

            int delimiterSize = encoding.GetByteCount("\0");
            byte[] buffer = new byte[delimiterSize];
            var builder = new StringBuilder();
            long max = maxLength > 0 ? _stream.Position + maxLength : _stream.Length;
            while (_stream.Position < max)
            {
                _stream.Read(buffer, 0, buffer.Length);
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
            _stream.Read(buffer, 0, buffer.Length);
            return encoding.GetString(buffer, 0, buffer.Length).Trim('\0');
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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

        private ulong Read(int byteLength)
        {
            Contract.Requires<ArgumentOutOfRangeException>(byteLength > 0 && byteLength <= _buffer.Length);

            int count = _stream.Read(_buffer, 0, byteLength);
            Contract.Assert(count == byteLength);

            ulong value = 0;
            for (int i = 0; i < byteLength; i++)
            {
                int offset = i * 8;
                if (Endianness == ByteOrder.BigEndian)
                    offset = (byteLength - i - 1) * 8;

                value |= (uint)((_buffer[i] << offset));
            }
            return value;
        }
    }
}
