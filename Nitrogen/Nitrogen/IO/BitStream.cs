using Nitrogen.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace Nitrogen.IO
{
	[ContractVerification(true)]
	public class BitStream
		: IDisposable
	{
		private Lazy<BitReader> _reader;
		private Lazy<BitWriter> _writer;
		private StreamState _state;

		public BitStream (Stream stream, StreamState initialState, bool leaveOpen = false)
		{
			Contract.Requires<ArgumentNullException>(stream != null);
			Contract.Requires(initialState.IsDefined());

			_reader = new Lazy<BitReader>(() => new BitReader(stream, leaveOpen));
			_writer = new Lazy<BitWriter>(() => new BitWriter(stream, leaveOpen));
			_state = initialState;
		}

		public virtual BitReader Reader { get { return _reader.Value; } }

		public virtual BitWriter Writer { get { return _writer.Value; } }

		public StreamState State
		{
			get { return _state; }
			set
			{
				Contract.Requires(value.IsDefined());
				_state = value;
			}
		}

		public void Stream (ref bool value)
		{
			if ( _state == StreamState.Read ) { value = Reader.ReadBit(); }
			if ( _state == StreamState.Write ) { Writer.WriteBit(value); }
		}

		public void Stream (ref byte value, int bits = sizeof(byte) * 8)
		{
			if ( _state == StreamState.Read ) { value = (byte) Reader.ReadUIntN(bits); }
			if ( _state == StreamState.Write ) { Writer.Write(value, bits); }
		}

		public void Stream (ref short value, int bits = sizeof(short) * 8)
		{
			if ( _state == StreamState.Read ) { value = (short) Reader.ReadUIntN(bits); }
			if ( _state == StreamState.Write ) { Writer.Write(value, bits); }
		}

		public void Stream (ref int value, int bits = sizeof(int) * 8)
		{
			if ( _state == StreamState.Read ) { value = (int) Reader.ReadUIntN(bits); }
			if ( _state == StreamState.Write ) { Writer.Write(value, bits); }
		}

		public void Stream (ref long value, int bits = sizeof(long) * 8)
		{
			if ( _state == StreamState.Read ) { value = (long) Reader.ReadUIntN(bits); }
			if ( _state == StreamState.Write ) { Writer.Write(value, bits); }
		}

		public void Stream (ref DateTime value, int bits = sizeof(double) * 8)
		{
			if ( _state == StreamState.Read ) { value = Reader.ReadDateTime(); }
			if ( _state == StreamState.Write ) { Writer.Write(value); }
		}

		public void Stream (ref sbyte value, int bits = sizeof(sbyte) * 8)
		{
			if ( _state == StreamState.Read )
			{
				value = (sbyte) Reader.ReadIntN(bits);
			}
			if ( _state == StreamState.Write ) { Writer.Write(value, bits); }
		}

		public void Stream (ref ushort value, int bits = sizeof(ushort) * 8)
		{
			if ( _state == StreamState.Read ) { value = (ushort) Reader.ReadIntN(bits); }
			if ( _state == StreamState.Write ) { Writer.Write(value, bits); }
		}

		public void Stream (ref uint value, int bits = sizeof(uint) * 8)
		{
			if ( _state == StreamState.Read ) { value = (uint) Reader.ReadIntN(bits); }
			if ( _state == StreamState.Write ) { Writer.Write(value, bits); }
		}

		public void Stream (ref ulong value, int bits = sizeof(ulong) * 8)
		{
			if ( _state == StreamState.Read ) { value = (ulong) Reader.ReadIntN(bits); }
			if ( _state == StreamState.Write ) { Writer.Write(value, bits); }
		}

		public void Stream (ref float value)
		{
			if ( _state == StreamState.Read ) { value = (float) Reader.ReadIntN(sizeof(float) * 8); }
			if ( _state == StreamState.Write ) { Writer.Write((int) value, sizeof(float) * 8); }
		}

		public void StreamNullTerminatedString (ref string value, Encoding encoding, int maxLength = 0)
		{
			if ( _state == StreamState.Read )
				value = Reader.ReadNullTerminatedString(encoding, maxLength);
			else if ( _state == StreamState.Write )
				Writer.WriteNullTerminatedString(value, encoding, maxLength);
		}

		public void StreamString (ref string value, Encoding encoding, int length)
		{
			if ( _state == StreamState.Read )
				value = Reader.ReadString(encoding, length);
			else if ( _state == StreamState.Write )
				Writer.WriteString(value, encoding, length);
		}

		public void Stream (IList<short> values, int bits = sizeof(short) * 8)
		{
			for ( int i = 0; i < values.Count; i++ )
			{
				short value = values[i];
				Stream(ref value, bits);
				values[i] = value;
			}
		}

		public void Stream (IList<ushort> values, int bits = sizeof(ushort) * 8)
		{
			for ( int i = 0; i < values.Count; i++ )
			{
				ushort value = values[i];
				Stream(ref value, bits);
				values[i] = value;
			}
		}

		public void Stream (IList<int> values, int bits = sizeof(int) * 8)
		{
			for ( int i = 0; i < values.Count; i++ )
			{
				int value = values[i];
				Stream(ref value, bits);
				values[i] = value;
			}
		}

		public void Dispose ()
		{
			Dispose(true);
		}

		protected virtual void Dispose (bool disposing)
		{
			if ( disposing )
			{
				if ( _reader != null && _reader.IsValueCreated ) { _reader.Value.Dispose(); }
				if ( _writer != null && _writer.IsValueCreated ) { _writer.Value.Dispose(); }
			}
		}
	}
}
