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
    /// Defines the structure of a generic reference.
    /// </summary>
    internal class GenericReferenceData
        : DataTemplate
    {
        protected override void Define()
        {
            Group("Reference", () =>
            {
                var type = Register<byte>("Type", n: 3);
                switch (type)
                {
                    case 0:
                        Import<IntegerReferenceData>();
                        break;

                    case 1:
                        Import<PlayerReferenceData>();
                        break;

                    case 2:
                        Import<ObjectReferenceData>();
                        break;

                    case 3:
                        Import<TeamReferenceData>();
                        break;

                    case 4:
                        Import<TimerReferenceData>();
                        break;
                }
            });
        }
    }
}
