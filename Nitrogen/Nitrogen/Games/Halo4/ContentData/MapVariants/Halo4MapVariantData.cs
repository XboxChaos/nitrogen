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

using Nitrogen.ContentData.MapVariants;
using Nitrogen.IO;
using Nitrogen.Games.Halo4.ContentData.Traits;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4.ContentData.MapVariants
{
    /// <summary>
    /// Represents the data in a Halo 4 map variant. 
    /// </summary>
    public class Halo4MapVariantData
        : MapVariantData<Halo4MapVariantObjectList, Halo4MapVariantObject>
    {
        /// <summary>
        /// Specifies the number of trait zones available.
        /// </summary>
        public const int TraitZoneCount = 4;

        private Halo4PlayerTraits[] traitZones;

        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4MapVariantData"/> class with default values.
        /// </summary>
        public Halo4MapVariantData()
        {
            this.traitZones = new Halo4PlayerTraits[TraitZoneCount];
            for (int i = 0; i < TraitZoneCount; i++)
            {
                this.traitZones[i] = new Halo4PlayerTraits();
            }
        }

        /// <summary>
        /// Gets or sets the trait zones contained in this map variant.
        /// </summary>
        public IList<Halo4PlayerTraits> TraitZones
        {
            get { return this.traitZones; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires(value.Count == TraitZoneCount);

                this.traitZones = (Halo4PlayerTraits[])value;
            }
        }

        #region MapVariantData Members

        public override void Serialize(BitStream s)
        {
            this.Objects.Boundaries = this.Boundaries;
            base.Serialize(s);

            s.Serialize(this.traitZones, 0, TraitZoneCount);
        }

        #endregion
    }
}
