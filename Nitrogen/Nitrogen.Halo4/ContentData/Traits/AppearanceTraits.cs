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

using Nitrogen.Core.ContentData.Traits;
using Nitrogen.Core.IO;
using System;

namespace Nitrogen.Halo4.ContentData.Traits
{
    public partial class Halo4PlayerTraits
    {
        /// <summary>
        /// Represents the group of appearance-related traits in a Halo 4 player trait set.
        /// </summary>
        public class AppearanceTraits
        : ISerializable<BitStream>
        {
            private float? playerScale;
            private byte activeCamo, waypointVisibility, gamertagVisibility, aura;
            private int deathEffect, loopingEffect;
            private ArmorColor primary, secondary;
            private bool useDefaultModel;
            private sbyte modelStringIndex;

            /// <summary>
            /// Initializes a new instance of the <see cref="AppearanceTraits"/> class with default values.
            /// </summary>
            public AppearanceTraits()
            {
                this.primary = new ArmorColor();
                this.secondary = new ArmorColor();
                this.useDefaultModel = true;
                this.modelStringIndex = -1;
            }

            #region ISerializable<BitStream> Members

            public void Serialize(BitStream s)
            {
                AddModifier(s, ref this.playerScale);
                s.Stream(ref this.activeCamo, 3);
                s.Stream(ref this.waypointVisibility, 2);
                s.Stream(ref this.gamertagVisibility, 2);
                s.Stream(ref this.aura, 3);
                s.Serialize(this.primary);
                s.Serialize(this.secondary);
                s.Stream(ref this.useDefaultModel);
                s.Stream(ref this.modelStringIndex);
                s.Stream(ref this.deathEffect);
                s.Stream(ref this.loopingEffect);
            }

            #endregion
        }
    }
}