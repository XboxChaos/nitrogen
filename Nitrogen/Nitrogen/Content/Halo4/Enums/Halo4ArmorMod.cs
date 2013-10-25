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
    public enum Halo4ArmorMod
    {
        Unchanged = -3,
        MapDefault = -2,
        Disabled = -1,
        Sensor = 0,
        Mobility,
        Grenadier,
        Dexterity,
        OrdnancePriority,
        Awareness,
        Shielding,
        AAEfficiency,
        Resupply,
        Firepower,

        Ammo = 11,
        Stealth,
        Stability,
        Nemesis,
        DropRecon,
        Wheelman,
        Gunner,
        Requisition,

        Explosives = 20,
        None = 21,

        Survivor = 23,
        Recharge = 24,
        Resistor = 25,
    }
}
