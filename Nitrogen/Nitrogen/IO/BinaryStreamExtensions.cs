using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;

namespace Nitrogen.IO
{
    [ContractVerification(true)]
    public static class BinaryStreamExtensions
    {
        public static void Serialize<T>(this BinaryStream s, T value)
            where T : ISerializable<BinaryStream>, new()
        {
            Contract.Requires<ArgumentNullException>(s != null);

            if (value == null) { value = new T(); }
            value.SerializeObject(s);
        }

        public static void Serialize<T>(this BinaryStream s, IList<T> values, int offset, int count)
            where T : ISerializable<BinaryStream>, new()
        {
            Contract.Requires<ArgumentNullException>(s != null && values != null);
            Contract.Requires<ArgumentOutOfRangeException>(offset >= 0 && count >= 0);

            for (int i = offset; i < count; i++)
            {
                T value;
                if (i < values.Count)
                {
                    value = values[i];
                }
                else
                {
                    value = new T();
                    values.Add(value);
                }
                s.Serialize(value);
            }
        }

        public static void PadBytes(this BinaryStream s, int byteCount)
        {
            Contract.Requires<ArgumentNullException>(s != null);

			if ( s.State == StreamState.Write )
			{
				s.Writer.BaseStream.Write(new byte[byteCount], 0, byteCount);
			}
			else if ( s.State == StreamState.Read )
			{
				s.BaseStream.Position += byteCount;
			}
        }
    }
}
