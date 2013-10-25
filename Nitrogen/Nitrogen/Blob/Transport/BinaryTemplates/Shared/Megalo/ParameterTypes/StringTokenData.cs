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

namespace Nitrogen.Blob.Transport.BinaryTemplates.Shared.Megalo.ParameterTypes
{
    /// <summary>
    /// Defines the structure of a string token.
    /// </summary>
    internal class StringTokenData
        : DataTemplate
    {
        protected override void Define()
        {
            // +1 encoding
            int type = -1;
            if (Mode == SerializationMode.Deserialize)
            {
                type = Convert.ToInt32(Read(typeof(byte), n: 3)) - 1;
                SetValue("Type", type);
            }
            else
            {
                type = GetValue<int>("Type");
                Write(type + 1, n: 3);
            }

            switch (type)
            {
                case 0:
                    Import<PlayerReferenceData>();
                    break;

                case 1:
                    Import<TeamReferenceData>();
                    break;

                case 2:
                    Import<ObjectReferenceData>();
                    break;

                case 3:
                case 4:
                    Import<IntegerReferenceData>();
                    break;

                case 5:
                    Import<TimerReferenceData>();
                    break;
            }
        }
    }
}
