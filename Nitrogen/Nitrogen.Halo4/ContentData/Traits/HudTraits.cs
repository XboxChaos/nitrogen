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
        /// Represents the group of HUD-related traits in a Halo 4 player trait set.
        /// </summary>
        public class HudTraits
        : ISerializable<BitStream>
        {
            private InheritableBool
                shieldHudVisible,
                motionTrackerWhileScoped,
                directionalDamageIndicator,
                battleAwareness,
                threatView,
                auralEnhancement,
                nemesis;

            private byte motionTrackerMode, visionMode;
            private float? nemesisDuration, motionTrackerRange;

            #region ISerializable<BitStream> Members

            public void Serialize(BitStream s)
            {
                AddBoolTrait(s, ref this.shieldHudVisible);
                AddModifier(s, ref this.nemesisDuration);
                AddModifier(s, ref this.motionTrackerRange);
                s.Stream(ref this.motionTrackerMode, 3);
                AddBoolTrait(s, ref this.motionTrackerWhileScoped);
                AddBoolTrait(s, ref this.directionalDamageIndicator);
                s.Stream(ref this.visionMode, 2);
                AddBoolTrait(s, ref this.battleAwareness);
                AddBoolTrait(s, ref this.threatView);
                AddBoolTrait(s, ref this.auralEnhancement);
                AddBoolTrait(s, ref this.nemesis);
            }

            #endregion
        }
    }
}