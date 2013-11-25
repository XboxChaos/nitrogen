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

namespace Nitrogen.IO
{
    public static class BitStreamExtensions
    {
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
                (s.Writer as BitWriter).Write(value + 1, bitLength);
            }
        }

        public static void StreamPlusOne<T>(this T s, ref short value, int bitLength = 16)
            where T : BitStream
        {
            if (s.State == StreamState.Read)
            {
                long temp;
                (s.Reader as BitReader).Read(out temp, bitLength);
                value = (short)(temp - 1);
            }
            else if (s.State == StreamState.Write)
            {
                (s.Writer as BitWriter).Write(value + 1, bitLength);
            }
        }

        public static void StreamOptional<T>(this T s, ref byte? value, int bitLength = 8, bool invert = true)
            where T : BitStream
        {
            bool hasValue = value.HasValue;
            
            if (invert)
                hasValue = !hasValue;
            s.Stream(ref hasValue);
            if (invert)
                hasValue = !hasValue;

            if (hasValue)
            {
                byte temp = (value != null && value.HasValue) ? value.Value : (byte)0;
                s.Stream(ref temp, bitLength);
                value = temp;
            }
            else
            {
                value = null;
            }
        }

        public static void StreamOptional<T>(this T s, ref sbyte? value, int bitLength = 8, bool invert = true)
            where T : BitStream
        {
            bool hasValue = value.HasValue;

            if (invert)
                hasValue = !hasValue;
            s.Stream(ref hasValue);
            if (invert)
                hasValue = !hasValue;

            if (hasValue)
            {
                sbyte temp = (value != null && value.HasValue) ? value.Value : (sbyte)0;
                s.Stream(ref temp, bitLength);
                value = temp;
            }
            else
            {
                value = null;
            }
        }

        public static void StreamOptional<T>(this T s, ref int? value, int bitLength = 8, bool invert = true)
            where T : BitStream
        {
            bool hasValue = value.HasValue;

            if (invert)
                hasValue = !hasValue;
            s.Stream(ref hasValue);
            if (invert)
                hasValue = !hasValue;

            if (hasValue)
            {
                int temp = (value != null && value.HasValue) ? value.Value : 0;
                s.Stream(ref temp, bitLength);
                value = temp;
            }
            else
            {
                value = null;
            }
        }
    }
}
