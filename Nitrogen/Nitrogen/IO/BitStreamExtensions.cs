using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.IO
{
    public static class BitStreamExtensions
    {
		public static void SerializeObject<T> (this BitStream s, T value)
			where T : ISerializable<BitStream>
		{
			Contract.Requires<ArgumentNullException>(s != null & value != null);
			value.SerializeObject(s);
		}

		public static void SerializeObjects<T> (this BitStream s, IList<T> values, int offset, int count)
			where T : ISerializable<BitStream>, new()
		{
			Contract.Requires<ArgumentNullException>(s != null && values != null);
			Contract.Requires<ArgumentOutOfRangeException>(offset >= 0 && count >= 0);

			for ( int i = offset; i < count; i++ )
			{
				T value;
				if ( i < values.Count )
				{
					value = values[i];
				}
				else
				{
					value = new T();
					values.Add(value);
				}
				s.SerializeObject(value);
			}
		}

		public static void SerializeObjects<T> (this BitStream s, IList<T> values, int countBitLength)
			where T : ISerializable<BitStream>, new()
		{
			int count = values.Count;
			s.Stream(ref count, countBitLength);
			SerializeObjects(s, values, 0, count);
		}

		public static void SerializeObjects<T> (this BitStream s, IList<T> values)
			where T : ISerializable<BitStream>, new()
		{
			SerializeObjects(s, values, 32);
		}

		public static void StreamPlusOne (this BitStream s, ref sbyte value, int bits = sizeof(sbyte) * 8)
		{
			Contract.Requires<ArgumentNullException>(s != null);

			if ( s.State == StreamState.Read )
				value = (sbyte) ( s.Reader.ReadUIntN(bits) - 1 );
			else if ( s.State == StreamState.Write )
				s.Writer.Write(value + 1, bits);
		}

		public static void StreamPlusOne (this BitStream s, ref short value, int bits = sizeof(short) * 8)
		{
			Contract.Requires<ArgumentNullException>(s != null);

			if ( s.State == StreamState.Read )
				value = (sbyte) ( s.Reader.ReadUIntN(bits) - 1 );
			else if ( s.State == StreamState.Write )
				s.Writer.Write(value + 1, bits);
		}

		public static void StreamPlusOne (this BitStream s, ref int value, int bits = sizeof(int) * 8)
		{
			Contract.Requires<ArgumentNullException>(s != null);

			if ( s.State == StreamState.Read )
				value = (sbyte) ( s.Reader.ReadUIntN(bits) - 1 );
			else if ( s.State == StreamState.Write )
				s.Writer.Write(value + 1, bits);
		}

		public static void StreamPlusOneOptional (this BitStream s, ref short? value, int bits = sizeof(short) * 8, bool inverted = true)
		{
			Contract.Requires<ArgumentNullException>(s != null);

			s.StreamOptional(ref value, bits, inverted);
			if ( s.State == StreamState.Read && value != null )
				value--;
			else if ( s.State == StreamState.Write && value != null )
				value++;
		}

		public static void StreamOptional(this BitStream s, ref byte? value, int bits = sizeof(byte) * 8, bool inverted = true)
		{
			Contract.Requires<ArgumentNullException>(s != null);

			bool hasValue = value.HasValue;
			if ( inverted ) { hasValue = !hasValue; }
			s.Stream(ref hasValue);
			if ( inverted ) { hasValue = !hasValue; }

			value = null;
			if (hasValue)
			{
				byte temp = value ?? 0;
				s.Stream(ref temp, bits);
				value = temp;
			}
		}

		public static void StreamOptional (this BitStream s, ref sbyte? value, int bits = sizeof(sbyte) * 8, bool inverted = true)
		{
			Contract.Requires<ArgumentNullException>(s != null);

			bool hasValue = value.HasValue;
			if ( inverted ) { hasValue = !hasValue; }
			s.Stream(ref hasValue);
			if ( inverted ) { hasValue = !hasValue; }

			value = null;
			if ( hasValue )
			{
				sbyte temp = value ?? 0;
				s.Stream(ref temp, bits);
				value = temp;
			}
		}

		public static void StreamOptional (this BitStream s, ref short? value, int bits = sizeof(short) * 8, bool inverted = true)
		{
			Contract.Requires<ArgumentNullException>(s != null);

			bool hasValue = value.HasValue;
			if ( inverted ) { hasValue = !hasValue; }
			s.Stream(ref hasValue);
			if ( inverted ) { hasValue = !hasValue; }

			value = null;
			if ( hasValue )
			{
				short temp = value ?? 0;
				s.Stream(ref temp, bits);
				value = temp;
			}
		}

		public static void StreamOptional (this BitStream s, ref ushort? value, int bits = sizeof(ushort) * 8, bool inverted = true)
		{
			Contract.Requires<ArgumentNullException>(s != null);

			bool hasValue = value.HasValue;
			if ( inverted ) { hasValue = !hasValue; }
			s.Stream(ref hasValue);
			if ( inverted ) { hasValue = !hasValue; }

			value = null;
			if ( hasValue )
			{
				ushort temp = value ?? 0;
				s.Stream(ref temp, bits);
				value = temp;
			}
		}

		public static void StreamOptional (this BitStream s, ref int? value, int bits = sizeof(int) * 8, bool inverted = true)
		{
			Contract.Requires<ArgumentNullException>(s != null);

			bool hasValue = value.HasValue;
			if ( inverted ) { hasValue = !hasValue; }
			s.Stream(ref hasValue);
			if ( inverted ) { hasValue = !hasValue; }

			value = null;
			if ( hasValue )
			{
				int temp = value ?? 0;
				s.Stream(ref temp, bits);
				value = temp;
			}
		}

        public static void StreamEncodedFloat(this BitStream s, ref float value, int bits, float min, float max, bool signed, bool isRounded = true, bool flag = true)
        {
            Contract.Requires<ArgumentNullException>(s != null);

            if (s.State == StreamState.Read)
            {
                value = s.Reader.ReadEncodedFloat(bits, min, max, signed, isRounded, flag);
            }
            else if (s.State == StreamState.Write)
            {
                s.Writer.WriteEncodedFloat(value, bits, min, max, signed, isRounded, flag);
            }
        }

        private static float ReadEncodedFloat(this BitReader s, int n, float min, float max, bool signed, bool isRounded, bool flag)
        {
            Contract.Requires<ArgumentNullException>(s != null);

            ulong encodedValue = s.ReadUIntN(n);

            uint maxInt = (uint)(1 << n);
            float result;
            if (signed)
            {
                maxInt--;
                if ((encodedValue << 1) == maxInt - 1)
                    result = 0.5f * (max + min);
            }

            if (flag)
            {
                if (encodedValue == 0)
                    return min;
                if (encodedValue == max - 1)
                    return max;

                float y = (max - min) / (float)(maxInt - 2);
                result = (float)(encodedValue - 1) * y + y * 0.5f + min;
            }
            else
            {
                float y = (max - min) / (float)maxInt;
                result = (float)encodedValue * y + y * 0.5f + min;
            }

            if (isRounded)
            {
                int rounded = (int)(result + 0.5f);
                if (Math.Abs((float)rounded - result) <= .002f)
                    result = (float)rounded;
            }

            return result;
        }

        private static void WriteEncodedFloat(this BitWriter s, float value, int n, float min, float max, bool signed, bool isRounded, bool flag)
        {
            uint maxInt = (uint)(1 << n);
            if (signed)
            {
                maxInt--;
                if (value == 0.5f * (max + min))
                {
                    s.Write((maxInt - 1) >> 1, n);
                    return;
                }
            }

            if (flag)
            {
                if (value == min)
                {
                    s.Write(0, n);
                    return;
                }
                    
                if (value == max)
                {
                    s.Write(maxInt - 1, n);
                    return;
                }

                float y = (max - min) / (float)(maxInt - 2);
                s.Write((uint)((value - min - y * 0.5f) / y + 1), n);
            }
            else
            {
                float y = (max - min) / (float)maxInt;
                s.Write((uint)((value - min - y * 0.5f) / y), n);
            }
        }
    }
}