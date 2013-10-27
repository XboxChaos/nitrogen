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

using Nitrogen.Content.Halo4.Data;
using System;

namespace Nitrogen.Content.Halo4.BaseVariant
{
    [Synchronizable]
    public class Halo4PersonalOrdnanceSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4PersonalOrdnanceSettings"/> class
        /// with default values.
        /// </summary>
        public Halo4PersonalOrdnanceSettings()
        {
            PointRequirement = 70.0f;
            PointIncreaseMultiplier = 0.30f;
            IsEnabled = true;
            OverrideDefaultSet = false;
            DropSet = "?";
        }

        [PropertyBinding("PointRequirement")]
        public float PointRequirement { get; set; }

        [PropertyBinding("PointIncreaseMultiplier")]
        public float PointIncreaseMultiplier { get; set; }

        [PropertyBinding("IsCustomized")]
        public bool OverrideDefaultSet { get; set; }

        [PropertyBinding("IsEnabled")]
        public bool IsEnabled { get; set; }

        [PropertyBinding("DropSet")]
        public string DropSet { get; set; }

        /*
        [PropertyBinding("Top")]
        public Halo4OrdnanceSet Top { get; set; }

        [PropertyBinding("Left")]
        public Halo4OrdnanceSet Left { get; set; }

        [PropertyBinding("Middle")]
        public Halo4OrdnanceSet Middle { get; set; }

        [PropertyBinding("Right")]
        public Halo4OrdnanceSet Right { get; set; }*/
    }
}
