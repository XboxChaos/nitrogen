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

using Nitrogen.Core.ContentData.MapVariants;
using Nitrogen.Core.IO;
using Nitrogen.Halo4.ContentData.Traits;
using System;
using System.Collections.Generic;

namespace Nitrogen.Halo4.ContentData.MapVariants
{
    /// <summary>
    /// Represents the data in a Halo 4 map variant. 
    /// </summary>
    public class Halo4MapVariantData
        : MapVariantData<Halo4MapVariantObjectList, Halo4MapVariantObject>
    {
        public const int TraitZoneCount = 4;

        private Halo4PlayerTraits[] traitZones;

        public Halo4MapVariantData()
        {
            this.traitZones = new Halo4PlayerTraits[TraitZoneCount];
            for (int i = 0; i < TraitZoneCount; i++)
            {
                this.traitZones[i] = new Halo4PlayerTraits();
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
