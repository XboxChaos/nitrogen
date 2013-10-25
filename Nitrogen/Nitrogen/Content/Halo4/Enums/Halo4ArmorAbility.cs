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

namespace Nitrogen.Content.Halo4.Enums
{
    // Unavailable armor abilities are omitted from this enum.

    public enum Halo4ArmorAbility
    {
        Random = -4, // Supported by the engine but the weighting for each armor ability is set to 0.
        Unchanged = -3,
        MapDefault = -2,
        Disabled = -1,

        JetPack = 1,


        ActiveCamo = 4,


        Hologram = 7,





        HardlightShield = 12,
        Teleporter = 13, // Only available on Harvest
        Autosentry = 14,


        PrometheanVision = 17,
        ThrusterPack = 18,
        Technician = 19, // Only available on Harvest
        RegenerationField = 20,
        None = 21,
    }
}
