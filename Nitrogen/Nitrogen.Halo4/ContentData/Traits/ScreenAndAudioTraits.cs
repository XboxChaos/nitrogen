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
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace Nitrogen.Halo4.ContentData.Traits
{
    public partial class Halo4PlayerTraits
    {
        /// <summary>
        /// Represents the group of HUD-related traits in a Halo 4 player trait set.
        /// </summary>
        public class ScreenAndAudioTraits
        : ISerializable<BitStream>
        {
            private InheritableToggle
                shieldHudVisible,
                motionTrackerWhileScoped,
                directionalDamageIndicator,
                battleAwareness,
                threatView,
                auralEnhancement,
                nemesis;

            private byte motionTrackerMode, visionMode;
            private float? nemesisDuration, motionTrackerRange;

            /// <summary>
            /// Gets or sets whether the shield bar is visible.
            /// </summary>
            public InheritableToggle ShieldHud
            {
                get { return this.shieldHudVisible; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.shieldHudVisible = value;
                }
            }

            /// <summary>
            /// Gets or sets whether the motion sensor is visible while zoomed in with a weapon.
            /// </summary>
            public InheritableToggle MotionSensorWhileScoped
            {
                get { return this.motionTrackerWhileScoped; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.motionTrackerWhileScoped = value;
                }
            }

            /// <summary>
            /// Gets or sets whether the directional damage indicator is visible.
            /// </summary>
            public InheritableToggle DirectionalDamageIndicator
            {
                get { return this.directionalDamageIndicator; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.directionalDamageIndicator = value;
                }
            }

            /// <summary>
            /// Gets or sets whether other players' shields and service tag is visible when in sight.
            /// </summary>
            public InheritableToggle BattleAwareness
            {
                get { return this.battleAwareness; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.battleAwareness = value;
                }
            }

            /// <summary>
            /// Gets or sets whether players beep when moving near you.
            /// </summary>
            public InheritableToggle AuralEnhancement
            {
                get { return this.auralEnhancement; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.auralEnhancement = value;
                }
            }

            /// <summary>
            /// Gets or sets whether the players see a waypoint above their killer for a period of time.
            /// </summary>
            public InheritableToggle Nemesis
            {
                get { return this.nemesis; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.nemesis = value;
                }
            }

            /// <summary>
            /// Gets or sets the state of the motion sensor.
            /// </summary>
            public MotionSensorMode MotionSensor
            {
                get { return (MotionSensorMode)this.motionTrackerMode; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(MotionSensorMode), value));
                    this.motionTrackerMode = (byte)value;
                }
            }

            /// <summary>
            /// Gets or sets the duration in seconds the a waypoint above a player's killer is visible
            /// when the <see cref="Nemesis"/> trait is active.
            /// </summary>
            public float? NemesisDuration
            {
                get { return this.nemesisDuration; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.nemesisDuration = value;
                }
            }

            /// <summary>
            /// Gets or sets the effective range of a player's motion sensor.
            /// </summary>
            public float? MotionSensorRange
            {
                get { return this.motionTrackerRange; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.motionTrackerRange = value;
                }
            }

            #region ISerializable<BitStream> Members

            public void Serialize(BitStream s)
            {
                AddBoolTrait(s, ref this.shieldHudVisible);
                AddModifier(s, ref this.nemesisDuration);
                AddModifier(s, ref this.motionTrackerRange);
                s.Stream(ref this.motionTrackerMode, 3);
                AddBoolTrait(s, ref this.motionTrackerWhileScoped);
                AddBoolTrait(s, ref this.directionalDamageIndicator);
                s.Stream(ref this.visionMode, 2); // probably shield hud type but doesn't work (343.jpg)
                AddBoolTrait(s, ref this.battleAwareness);
                AddBoolTrait(s, ref this.threatView);
                AddBoolTrait(s, ref this.auralEnhancement);
                AddBoolTrait(s, ref this.nemesis);
            }

            #endregion
        }
    }
}