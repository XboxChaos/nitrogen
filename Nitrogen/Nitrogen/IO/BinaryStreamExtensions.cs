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
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;

namespace Nitrogen.IO
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

        public static void SerializeColor<T>(this T s, ref Color color, bool includeAlpha = true)
            where T : BinaryStream
        {
            if (s.State == StreamState.Read)
            {
                byte a = 255, r, g, b;

                if (includeAlpha) { s.Reader.Read(out a); }
                s.Reader.Read(out r);
                s.Reader.Read(out g);
                s.Reader.Read(out b);

                color = Color.FromArgb(a, r, g, b);
            }
            else if (s.State == StreamState.Write)
            {
                if (includeAlpha) { s.Writer.Write(color.A); }
                s.Writer.Write(color.R);
                s.Writer.Write(color.G);
                s.Writer.Write(color.B);
            }
        }
    }
}
