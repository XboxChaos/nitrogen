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
        /// Represents the group of movement-related traits in a Halo 4 player trait set.
        /// </summary>
        public class MovementTraits
        : ISerializable<BitStream>
        {
            private float? playerSpeed, gravity, jumpHeight, controllerSensitivity;
            private byte vehicleUsage;
            private InheritableBool doubleJump, sprint, automaticMomentum, vaulting, stealth;

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
