/*
 *   Nitrogen - Halo Content API
 *   Copyright © 2013 The Nitrogen Authors. All rights reserved.
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

using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.BaseVariant
{
    /// <summary>
    /// Represents a personal ordnance item choice.
    /// </summary>
    public class OrdnanceItem
        : ISerializable<BitStream>
    {
        private string item;
        private float weighting;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdnanceItem"/> class with default values.
        /// </summary>
        public OrdnanceItem()
        {
            this.weighting = 1.0f;
            this.item = "";
        }

        /// <summary>
        /// Gets or sets the ordnance item.
        /// </summary>
        public string Item
        {
            get { return this.item; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(Encoding.ASCII.GetByteCount(value) <= 32);

                this.item = value;
            }
        }

        /// <summary>
        /// Gets or sets the probability of this item.
        /// </summary>
        public float Weight
        {
            get { return this.weighting; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0 && value <= 10000);
                this.weighting = value;
            }
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.StreamNullTerminatedString(ref this.item, Encoding.ASCII, 32);
            s.Stream(ref this.weighting, bits: 30, min: 0, max: 10000, signed: false, rounded: true, flag: true);
        }

        #endregion
    }
}