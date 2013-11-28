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

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.BaseVariant
{
    /// <summary>
    /// Represents a single personal ordnance slot.
    /// </summary>
    public class OrdnanceSlot
        : ISerializable<BitStream>
    {
        /// <summary>
        /// Specifies the number of ordnance items in a slot.
        /// </summary>
        public const int ItemCount = 8;

        private OrdnanceItem[] items;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdnanceSlot"/> class with default values.
        /// </summary>
        public OrdnanceSlot()
        {
            this.items = new OrdnanceItem[ItemCount];
            for (int i = 0; i < ItemCount; i++)
                this.items[i] = new OrdnanceItem();
        }

        /// <summary>
        /// Gets or sets the possible items in this slot.
        /// </summary>
        public OrdnanceItem[] Items
        {
            get { return this.items; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(value.Length != ItemCount);

                this.items = value;
                for (int i = 0; i < this.items.Length; i++)
                {
                    if (this.items[i] == null)
                        this.items[i] = new OrdnanceItem();
                }
            }
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Serialize(this.items, 0, ItemCount);
        }

        #endregion
    }
}
