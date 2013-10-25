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
    /// Defines the structure of a waypoint icon in a Megalo action or condition.
    /// </summary>
    internal class WaypointIconData
        : DataTemplate
    {
        protected override void Define()
        {
            Group("WaypointIcon", () =>
            {
                // +1 encoding
                int index = -1;
                if (Mode == SerializationMode.Deserialize)
                {
                    index = Convert.ToInt32(Read(typeof(byte), n: 5)) - 1;
                    SetValue("IconIndex", index);
                }
                else
                {
                    index = GetValue<int>("IconIndex");
                    Write(index + 1, n: 5);
                }

                if (index == 11)
                    Group("LetterIndex", () => Import<IntegerReferenceData>());
            });
        }
    }
}
