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
using System.Diagnostics;

namespace Nitrogen.Blob.Transport.BinaryTemplates.Shared.Megalo.ParameterTypes
{
    /// <summary>
    /// Defines the structure of a string reference.
    /// </summary>
    internal class StringReferenceData
        : DataTemplate
    {
        private int countSize;

        internal StringReferenceData(int countSize)
        {
            this.countSize = countSize;
        }

        protected override void Define()
        {
            Group("StringReference", () =>
            {
                // +1 encoding
                int index = -1;
                if (Mode == SerializationMode.Deserialize)
                {
                    index = Convert.ToInt32(Read(typeof(byte))) - 1;
                    SetValue("StringIndex", index);
                }
                else
                {
                    index = GetValue<int>("StringIndex");
                    Write(index + 1, n: 8);
                }

                Group("Tokens", () =>
                {
                    var count = Register<byte>("Count", n: this.countSize);
                    for (var i = 0; i < count; i++)
                    {
                        Group("Token[" + i + "]", () => Import<StringTokenData>());
                    }
                });
            });
        }
    }
}
