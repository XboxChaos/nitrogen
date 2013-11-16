/*
 *   Nitrogen - Halo Content API
 *   Copyright (c) 2013 Matt Saville and Aaron Dierking
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
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;

namespace Nitrogen.Core.IO
{
    public static class BinaryStreamExtensions
    {
        public static void Serialize<T>(this T s, ISerializable<T> value)
            where T : BinaryStream
        {
            Contract.Requires<ArgumentNullException>(s != null && value != null);
            Contract.Requires<InvalidOperationException>(s.State != StreamState.None);

            value.Serialize(s);
        }

        public static void Serialize<T>(this T s, IEnumerable<ISerializable<T>> values, int offset, int count)
            where T : BinaryStream
        {
            Contract.Requires<ArgumentNullException>(s != null && values != null);
            Contract.Requires<InvalidOperationException>(s.State != StreamState.None);
            Contract.Requires<ArgumentOutOfRangeException>(offset >= 0 && count >= 0);

            var valueList = new List<ISerializable<T>>(values);
            for (int i = offset; i < offset + count; i++)
            {
                valueList[i].Serialize(s);
            }
        }

        public static void StreamBlock<T>(this T s, int length, Action data)
            where T : BinaryStream
        {
            Contract.Requires<ArgumentNullException>(s != null);
            Contract.Requires<ArgumentOutOfRangeException>(length > 0);

            long start = s.Position;

            data.Invoke();

            int remaining = length - (int)(s.Position - start);
            if (remaining < 0) 
            {
                throw new InvalidOperationException("Data was larger than the block size"); 
            }

            s.Position += remaining;
        }

        public static void StreamPlusOne<T>(this T s, ref sbyte value, int bitLength = 8)
            where T : BitStream
        {
            if (s.State == StreamState.Read)
            {
                long temp;
                (s.Reader as BitReader).Read(out temp, bitLength);
                value = (sbyte)(temp - 1);
            }
            else if (s.State == StreamState.Write)
            {
                (s.Writer as BitWriter).Write(value, bitLength);
            }
        }
    }
}
