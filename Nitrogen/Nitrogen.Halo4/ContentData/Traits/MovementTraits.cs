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
        /// Represents the group of movement-related traits in a Halo 4 player trait set.
        /// </summary>
        public class MovementTraits
        : ISerializable<BitStream>
        {
            private float? playerSpeed, gravity, jumpHeight, controllerSensitivity;
            private byte vehicleUsage;
            private InheritableToggle doubleJump, sprint, automaticMomentum, vaulting, stealth;

            /// <summary>
            /// Initializes a new instance of the <see cref="MovementTraits"/> class with default values.
            /// </summary>
            public MovementTraits()
            {
                this.playerSpeed = 1.10f;
            }
            
            /// <summary>
            /// Gets or sets a player's movement speed.
            /// </summary>
            public float? MovementSpeed
            {
                get { return this.playerSpeed; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.playerSpeed = value;
                }
            }

            /// <summary>
            /// Gets or sets the gravity intensity for a player.
            /// </summary>
            public float? Gravity
            {
                get { return this.gravity; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.gravity = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's jump height.
            /// </summary>
            public float? JumpHeight
            {
                get { return this.jumpHeight; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.jumpHeight = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's controller sensitivity.
            /// </summary>
            public float? ControllerSensitivity
            {
                get { return this.controllerSensitivity; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.controllerSensitivity = value;
                }
            }

            /// <summary>
            /// Gets or sets whether sprint is enabled for a player.
            /// </summary>
            public InheritableToggle Sprint
            {
                get { return this.sprint; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.sprint = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player's footsteps are dampened.
            /// </summary>
            public InheritableToggle Stealth
            {
                get { return this.stealth; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.stealth = value;
                }
            }

            /// <summary>
            /// Gets or sets the vehicle usage permissions of a player.
            /// </summary>
            public VehicleUsageMode VehicleUsage
            {
                get { return (VehicleUsageMode)this.vehicleUsage; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(VehicleUsageMode), value));
                    this.vehicleUsage = (byte)value;
                }
            }

            #region ISerializable<BitStream> Members

            public void Serialize(BitStream s)
            {
                AddModifier(s, ref this.playerSpeed);
                AddModifier(s, ref this.gravity);
                AddModifier(s, ref this.jumpHeight);
                AddModifier(s, ref this.controllerSensitivity);
                s.Stream(ref this.vehicleUsage, 4);
                AddBoolTrait(s, ref this.doubleJump);
                AddBoolTrait(s, ref this.sprint);
                AddBoolTrait(s, ref this.automaticMomentum);
                AddBoolTrait(s, ref this.vaulting);
                AddBoolTrait(s, ref this.stealth);
            }

            #endregion
        }
    }
}
