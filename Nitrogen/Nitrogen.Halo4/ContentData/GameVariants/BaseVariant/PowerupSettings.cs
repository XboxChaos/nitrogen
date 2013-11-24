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

using Nitrogen.Core.IO;
using Nitrogen.Halo4.ContentData.Traits;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.Halo4.ContentData.GameVariants.BaseVariant
{
    public class PowerupSettings
        : ISerializable<BitStream>
    {
        private byte duration1, duration2;
        private Halo4PlayerTraits phase1, phase2;

        /// <summary>
        /// Creates a new instance of the <see cref="PowerupSettings"/> class based on the
        /// Overshield powerup.
        /// </summary>
        /// <returns>
        /// A new instance of the <see cref="PowerupSettings"/> class representing the Overshield
        /// powerup.
        /// </returns>
        public static PowerupSettings CreateOvershield()
        {
            return new PowerupSettings
            {
                AlphaPhaseDuration = 1,
                AlphaPhaseTraits = new Halo4PlayerTraits
                {
                    // shield multiplier: 2.0
                    // overshield decay rate: 5.0
                    // shield recharge rate = 5.0
                    // cancel current powerup: disabled
                },

                BetaPhaseDuration = 120,
                BetaPhaseTraits = new Halo4PlayerTraits
                {
                    // looping effect 2
                    // damage resistance: 3.40
                    // overshield decay: -0.30
                    // cancel current powerup: enabled
                }
            };
        }

        /// <summary>
        /// Creates a new instance of the <see cref="PowerupSettings"/> class based on the
        /// Speed Boost powerup.
        /// </summary>
        /// <returns>
        /// A new instance of the <see cref="PowerupSettings"/> class representing the Speed Boost
        /// powerup.
        /// </returns>
        public static PowerupSettings CreateSpeedBoost()
        {
            return new PowerupSettings
            {
                AlphaPhaseDuration = 45,
                AlphaPhaseTraits = new Halo4PlayerTraits
                {
                    // loop effect 3
                    // reload speed: 1.751
                    // switch speed: 1.751
                    // movement speed: 1.50
                }
            };
        }

        /// <summary>
        /// Creates a new instance of the <see cref="PowerupSettings"/> class based on the
        /// Damage Boost powerup.
        /// </summary>
        /// <returns>
        /// A new instance of the <see cref="PowerupSettings"/> class representing the Damage Boost
        /// powerup.
        /// </returns>
        public static PowerupSettings CreateDamageBoost()
        {
            return new PowerupSettings
            {
                AlphaPhaseDuration = 30,
                AlphaPhaseTraits = new Halo4PlayerTraits
                {
                    // looping effect: 4
                    // active camo: 1
                    // damage multiplier: 1.453
                    // melee dmg: 1.60
                }
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerupSettings"/> class with default values.
        /// </summary>
        public PowerupSettings()
        {
            this.phase1 = new Halo4PlayerTraits();
            this.phase2 = new Halo4PlayerTraits();
        }

        /// <summary>
        /// Gets or sets the duration in seconds the alpha phase of this powerup is active.
        /// </summary>
        public byte AlphaPhaseDuration
        {
            get { return this.duration1; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= 127);
                this.duration1 = value;
            }
        }

        /// <summary>
        /// Gets or sets the duration in seconds the beta phase of this powerup is active.
        /// </summary>
        public byte BetaPhaseDuration
        {
            get { return this.duration2; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= 127);
                this.duration2 = value;
            }
        }

        /// <summary>
        /// Gets or sets the traits to apply during the alpha phase.
        /// </summary>
        public Halo4PlayerTraits AlphaPhaseTraits
        {
            get { return this.phase1; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.phase1 = value;
            }
        }

        /// <summary>
        /// Gets or sets the traits to apply during the beta phase.
        /// </summary>
        public Halo4PlayerTraits BetaPhaseTraits
        {
            get { return this.phase2; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.phase2 = value;
            }
        }

        #region ISerializable<BitStream>

        public void Serialize(BitStream s)
        {
            s.Serialize(this.phase1);
            s.Stream(ref this.duration1, 7);

            s.Serialize(this.phase2);
            s.Stream(ref this.duration2, 7);
        }

        #endregion
    }
}