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

using Nitrogen.Core.ContentData.Metadata;
using Nitrogen.Core.IO;
using System;
using System.Collections.Generic;

namespace Nitrogen.Core.ContentData.MapVariants
{
    /// <summary>
    /// Represents the data in a map variant. 
    /// </summary>
    public class MapVariantData
        : ISerializable<BitStream>
    {
        private ContentMetadata metadata;
        private byte encodingVersion;
        private int unk0, unk1;
        private short objectTypeCount;
        private int mapId;
        private bool unk2, unk3;
        private int[] boundaries;
        private int unk4, unk5;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapVariantData"/> class with default values.
        /// </summary>
        public MapVariantData()
        {
            this.metadata = new MapVariantMetadata();
            this.boundaries = new int[6];
        }

        public int Unk0 { get { return this.unk0; } set { this.unk0 = value; } }

        public int Unk1 { get { return this.unk1; } set { this.unk1 = value; } }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Serialize(this.metadata);
            s.Stream(ref this.encodingVersion);
            s.Stream(ref this.unk0);
            s.Stream(ref this.unk1);
            s.Stream(ref this.objectTypeCount, 9);
            s.Stream(ref this.mapId);
            s.Stream(ref this.unk2);
            s.Stream(ref this.unk3);
            s.Stream(this.boundaries, 0, this.boundaries.Length);
            s.Stream(ref this.unk4);
            s.Stream(ref this.unk5);
        }

        #endregion
    }
}
