using Nitrogen.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace Nitrogen.IO
{
    [ContractVerification(true)]
    public class BinaryStream
        : IDisposable
    {
        private Lazy<BinaryReader> _reader;
        private Lazy<BinaryWriter> _writer;
        private StreamState _state;
        private Stream _stream;

        public BinaryStream(Stream stream, StreamState initialState, ByteOrder endianness = ByteOrder.Default, bool leaveOpen = false)
        {
            Contract.Requires<ArgumentNullException>(stream != null);
            Contract.Requires(initialState.IsDefined() && endianness.IsDefined());

            _reader = new Lazy<BinaryReader>(() => new BinaryReader(stream, endianness, leaveOpen));
            _writer = new Lazy<BinaryWriter>(() => new BinaryWriter(stream, endianness, leaveOpen));
            _state = initialState;
            _stream = stream;
        }

        public virtual BinaryReader Reader { get { return _reader.Value; } }

        public virtual BinaryWriter Writer { get { return _writer.Value; } }

        public virtual Stream BaseStream { get { return _stream; } }

        public StreamState State
        {
            get { return _state; }
            set
            {
                Contract.Requires(value.IsDefined());
                _state = value;
            }
        }

        public void Stream(ref bool value)
        {
            if (_state == StreamState.Read)
                value = Reader.ReadBoolean();
            else if (_state == StreamState.Write)
                Writer.Write(value);
        }

        public void Stream(ref byte value)
        {
            if (_state == StreamState.Read)
                value = Reader.ReadByte();
            else if (_state == StreamState.Write)
                Writer.Write(value);
        }

        public void Stream(ref sbyte value)
        {
            if (_state == StreamState.Read)
                value = Reader.ReadSByte();
            else if (_state == StreamState.Write)
                Writer.Write(value);
        }

        public void Stream(ref short value)
        {
            if (_state == StreamState.Read)
                value = Reader.ReadInt16();
            else if (_state == StreamState.Write)
                Writer.Write(value);
        }

        public void Stream(ref ushort value)
        {
            if (_state == StreamState.Read)
                value = Reader.ReadUInt16();
            else if (_state == StreamState.Write)
                Writer.Write(value);
        }

        public void Stream(ref int value)
        {
            if (_state == StreamState.Read)
                value = Reader.ReadInt32();
            else if (_state == StreamState.Write)
                Writer.Write(value);
        }

        public void Stream(ref uint value)
        {
            if (_state == StreamState.Read)
                value = Reader.ReadUInt32();
            else if (_state == StreamState.Write)
                Writer.Write(value);
        }

        public void Stream(ref long value)
        {
            if (_state == StreamState.Read)
                value = Reader.ReadInt64();
            else if (_state == StreamState.Write)
                Writer.Write(value);
        }

        public void Stream(ref ulong value)
        {
            if (_state == StreamState.Read)
                value = Reader.ReadUInt64();
            else if (_state == StreamState.Write)
                Writer.Write(value);
        }

        public void Stream(ref float value)
        {
            if (_state == StreamState.Read)
                value = Reader.ReadSingle();
            else if (_state == StreamState.Write)
                Writer.Write(value);
        }

        public void Stream(ref double value)
        {
            if (_state == StreamState.Read)
                value = Reader.ReadDouble();
            else if (_state == StreamState.Write)
                Writer.Write(value);
        }

        public void Stream(ref DateTime value)
        {
            if (_state == StreamState.Read)
                value = Reader.ReadDateTime();
            else if (_state == StreamState.Write)
                Writer.Write(value);
        }

        public void StreamNullTerminatedString(ref string value, Encoding encoding, int maxLength = 0)
        {
            if (_state == StreamState.Read)
                value = Reader.ReadNullTerminatedString(encoding, maxLength);
            else if (_state == StreamState.Write)
                Writer.WriteNullTerminatedString(value, encoding, maxLength);
        }

        public void StreamString(ref string value, Encoding encoding, int length)
        {
            if (_state == StreamState.Read)
                value = Reader.ReadString(encoding, length);
            else if (_state == StreamState.Write)
                Writer.WriteString(value, encoding, length);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_reader != null && _reader.IsValueCreated) { _reader.Value.Dispose(); }
                if (_writer != null && _writer.IsValueCreated) { _writer.Value.Dispose(); }

                _stream = null;
            }
        }
    }
}
