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
    /// <summary>
    /// Represents a set of traits in Halo 4.
    /// </summary>
    public partial class Halo4PlayerTraits
        : ISerializable<BitStream>
    {
        private ArmorTraits armor;
        private EquipmentTraits equipment;
        private MovementTraits movement;
        private AppearanceTraits appearance;
        private HudTraits hudTraits;

        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4PlayerTraits"/> class with default values.
        /// </summary>
        public Halo4PlayerTraits()
        {
            this.armor = new ArmorTraits();
            this.equipment = new EquipmentTraits();
            this.movement = new MovementTraits();
            this.appearance = new AppearanceTraits();
            this.hudTraits = new HudTraits();
        }

        // TODO: Add methods to enable/disable tactical packages and support upgrades by modifying applicable traits.

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Serialize(this.armor);
            s.Serialize(this.equipment);
            s.Serialize(this.movement);
            s.Serialize(this.appearance);
            s.Serialize(this.hudTraits);
        }

        #endregion

        protected static void AddModifier(BitStream s, ref float? value)
        {
            if (s.State == StreamState.Read)
            {
                var r = (s.Reader as BitReader);

                bool isPresent;
                s.Reader.Read(out isPresent);

                if (isPresent)
                    value = r.ReadEncodedFloat(n: 16, min: -200f, max: 200f, signed: true, flag: true, isRounded: true);
                else
                    value = null;
            }
            else if (s.State == StreamState.Write)
            {
                var w = (s.Writer as BitWriter);
                w.Write(value.HasValue);
                if (value.HasValue)
                {
                    w.WriteEncodedFloat(value.Value, n: 16, min: -200f, max: 200f, signed: true, flag: true, isRounded: true);
                }
            }
        }

        protected static void AddBoolTrait(BitStream s, ref InheritableBool value)
        {
            var temp = (byte)value;
            s.Stream(ref temp, 2);
            value = (InheritableBool)temp;
        }
    }
}
