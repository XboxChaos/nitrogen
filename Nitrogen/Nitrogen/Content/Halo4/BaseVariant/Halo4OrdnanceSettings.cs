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

namespace Nitrogen.Content.Halo4.BaseVariant
{
    [Synchronizable]
    public class Halo4OrdnanceSettings
    {
        [PropertyBinding("InitialOrdnanceEnabled")]
        public bool InitialOrdnanceEnabled { get; set; }

        [PropertyBinding("RandomOrdnanceEnabled")]
        public bool RandomOrdnanceEnabled { get; set; }

        [PropertyBinding("ObjectiveOrdnanceEnabled")]
        public bool ObjectiveOrdnanceEnabled { get; set; }

        [PropertyBinding("PersonalOrdnanceEnabled")]
        public bool PersonalOrdnanceEnabled { get; set; }

        [PropertyBinding("OrdnanceSystemEnabled")]
        public bool OrdnanceSystemEnabled { get; set; }

        [PropertyBinding("RandomOrdnanceMinTimer")]
        public short RandomOrdnanceMinTimer { get; set; }

        [PropertyBinding("RandomOrdnanceMaxTimer")]
        public short RandomOrdnanceMaxTimer { get; set; }

        [PropertyBinding("InitialDropObjectFilterName")]
        public string InitialDropLabel { get; set; }

        [PropertyBinding("InitialDropDelay")]
        public short InitialDropDelay { get; set; }

        [PropertyBinding("RandomDropSet")]
        public string RandomOrdnanceSet { get; set; }

        [PropertyBinding("PersonalDropSet")]
        public string PersonalOrdnanceSet { get; set; }

        [PropertyBinding("SubstitutionSet")]
        public string SubstitutionSet { get; set; }

        [PropertyBinding("Personal")]
        public Halo4PersonalOrdnanceSettings Personal { get; set; }
    }
}
