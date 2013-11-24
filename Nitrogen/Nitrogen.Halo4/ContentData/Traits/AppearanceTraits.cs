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
    /// <summary>
    /// Indicates an armor effect.
    /// </summary>
    public enum ArmorEffect
    {
        Unchanged = -3,
        MapDefault,
        Disabled,
        GruntBirthdayParty,
        Regicide,
        Overshield,
        SpeedBoost,
        DamageBoost,
        AlphaFloodSpeedBoost,
        Flood
    }

    public partial class Halo4PlayerTraits
    {
        /// <summary>
        /// Represents the group of appearance-related traits in a Halo 4 player trait set.
        /// </summary>
        public class AppearanceTraits
        : ISerializable<BitStream>
        {
            public const sbyte FloodModelVariant = 120;

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

            /// <summary>
            /// Gets or sets the scale of a player's model.
            /// </summary>
            public float? PlayerScale
            {
                get { return this.playerScale; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.playerScale = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's primary armor color.
            /// </summary>
            public ArmorColor PrimaryColor
            {
                get { return this.primary; }
                set
                {
                    Contract.Requires<ArgumentNullException>(value != null);
                    this.primary = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's primary armor color.
            /// </summary>
            public ArmorColor SecondaryColor
            {
                get { return this.secondary; }
                set
                {
                    Contract.Requires<ArgumentNullException>(value != null);
                    this.secondary = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's Active Camouflage state.
            /// </summary>
            public ActiveCamoMode ActiveCamo
            {
                get { return (ActiveCamoMode)this.activeCamo; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(ActiveCamoMode), value));
                    this.activeCamo = (byte)value;
                }
            }

            /// <summary>
            /// Gets or sets a player's waypoint visibility.
            /// </summary>
            public HudVisibility WaypointVisibility
            {
                get { return (HudVisibility)this.waypointVisibility; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(HudVisibility), value));
                    this.waypointVisibility = (byte)value;
                }
            }

            /// <summary>
            /// Gets or sets a player's gamertag visibility.
            /// </summary>
            public HudVisibility GamertagVisibility
            {
                get { return (HudVisibility)this.gamertagVisibility; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(HudVisibility), value));
                    this.gamertagVisibility = (byte)value;
                }
            }

            /// <summary>
            /// Gets or sets a player's continuous armor effect.
            /// </summary>
            public ArmorEffect LoopingEffect
            {
                get { return (ArmorEffect)this.loopingEffect; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(ArmorEffect), value));
                    this.loopingEffect = (int)value;
                }
            }

            /// <summary>
            /// Gets or sets a player's armor effect to display upon death.
            /// </summary>
            public ArmorEffect DeathEffect
            {
                get { return (ArmorEffect)this.deathEffect; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(ArmorEffect), value));
                    this.deathEffect = (int)value;
                }
            }
            
            /// <summary>
            /// Gets or sets the model variant to use based on its index in the Megalo String Index
            /// Table (msit).
            /// 
            /// Set to -1 to use the default model variant.
            /// 
            /// Use <see cref="FloodModelVariant"/> to use the Flood model variant.
            /// </summary>
            public sbyte ModelVariantStringIndex
            {
                get { return this.modelStringIndex; }
                set
                {
                    this.modelStringIndex = value;
                    if (value == -1) this.useDefaultModel = true;
                }
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