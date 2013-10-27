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
using Nitrogen.Content.Halo4.Enums;
using Nitrogen.Content.Shared;
using System;

namespace Nitrogen.Content.Halo4.BaseVariant
{
    [Synchronizable]
    public class Halo4PowerupSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4PowerupSettings"/> class with default
        /// values.
        /// </summary>
        public Halo4PowerupSettings()
        {
            // DAMAGE BOOST
            DamageBoost = new Halo4Powerup
            {
                AlphaPhaseTraits = new TraitSetWithDuration<Halo4RuntimeTraitSet>
                {
                    Duration = 30,
                    Traits = new Halo4RuntimeTraitSet
                    {
                        LoopingEffect = Halo4ArmorEffect.DamageBoost,
                        DamageMultiplier = 1.45f,
                        MeleeDamageMultiplier = 1.6f,
                    }
                },

                BetaPhaseTraits = new TraitSetWithDuration<Halo4RuntimeTraitSet>()
            };

            // SPEED BOOST
            SpeedBoost = new Halo4Powerup
            {
                AlphaPhaseTraits = new TraitSetWithDuration<Halo4RuntimeTraitSet>
                {
                    Duration = 45,
                    Traits = new Halo4RuntimeTraitSet
                    {
                        LoopingEffect = Halo4ArmorEffect.SpeedBoost,
                        MovementSpeed = 1.5f,
                        ReloadSpeed = 1.75f,
                        SwitchSpeed = 1.75f,
                    }
                },

                BetaPhaseTraits = new TraitSetWithDuration<Halo4RuntimeTraitSet>()
            };

            // OVERSHIELD
            Overshield = new Halo4Powerup
            {
                AlphaPhaseTraits = new TraitSetWithDuration<Halo4RuntimeTraitSet>
                {
                    Duration = 1,
                    Traits = new Halo4RuntimeTraitSet
                    {
                        CancelCurrentPowerup = false,
                        ShieldMultiplier = 2f,
                        ShieldRechargeRate = 5f,
                        OvershieldDecayRate = 5f,
                    }
                },

                BetaPhaseTraits = new TraitSetWithDuration<Halo4RuntimeTraitSet>
                {
                    Duration = 120,
                    Traits = new Halo4RuntimeTraitSet
                    {
                        CancelCurrentPowerup = true,
                        LoopingEffect = Halo4ArmorEffect.Overshield,
                        DamageResistance = 3.4f,
                        OvershieldDecayRate = -0.03f,
                    }
                }
            };

            // CUSTOM
            Custom = new Halo4Powerup();
        }

        /// <summary>
        /// Gets or sets the Damage Boost powerup behavior.
        /// </summary>
        [PropertyBinding("DamageBoost")]
        public Halo4Powerup DamageBoost { get; set; }

        /// <summary>
        /// Gets or sets the Speed Boost powerup behavior.
        /// </summary>
        [PropertyBinding("SpeedBoost")]
        public Halo4Powerup SpeedBoost { get; set; }

        /// <summary>
        /// Gets or sets the Overshield powerup behavior.
        /// </summary>
        [PropertyBinding("Overshield")]
        public Halo4Powerup Overshield { get; set; }

        /// <summary>
        /// Gets or sets the custom powerup behavior.
        /// </summary>
        [PropertyBinding("Custom")]
        public Halo4Powerup Custom { get; set; }
    }
}
