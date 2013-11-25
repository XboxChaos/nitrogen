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

namespace Nitrogen.Games.Halo4.ContentData.Metadata
{
    /// <summary>
    /// Specifies a set of skulls to be active.
    /// </summary>
    [Flags]
    public enum Halo4Skulls
    {
        None,
        Iron = 1 << 0,
        BlackEye = 1 << 1,
        ToughLuck = 1 << 2,
        Catch = 1 << 3,
        Fog = 1 << 4,
        Famine = 1 << 5,
        Thunderstorm = 1 << 6,
        Tilt = 1 << 7,
        Mythic = 1 << 8,
        Assassin = 1 << 9,
        Blind = 1 << 10,
        Superman = 1 << 11, // lol at this
        GruntBirthdayParty = 1 << 12,
        IWHBYD = 1 << 13,
        Red = 1 << 14, // useless in Halo 4
        Yellow = 1 << 15, // useless in Halo 4
        Blue = 1 << 16, // useless in Halo 4
    }
}
