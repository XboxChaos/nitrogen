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

using Nitrogen.ContentData.Localization;
using Nitrogen.ContentData.Metadata;
using Nitrogen.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.ContentData.MapVariants
{
    /// <summary>
    /// Represents the data in a map variant. 
    /// </summary>
    public class MapVariantData<TMapObjectList, TMapObjectType>
        : ISerializable<BitStream>
        where TMapObjectList : MapVariantObjectList<TMapObjectType>, new()
    {
        /// <summary>
        /// Specifies the 
        /// </summary>
        private const int MaxObjectTypes = 256;

        private ContentMetadata metadata;
        private byte encodingVersion;
        private int unk0, unk1;
        private short objectTypeCount;
        private int mapId;
        private bool unk2, unk3;
        private float[] boundaries;
        private int budget, unk5;
        private StringTable stringTable;
        private ObjectTypeCount[] objectTypeCountTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapVariantData"/> class with default values.
        /// </summary>
        public MapVariantData()
        {
            this.metadata = new MapVariantMetadata();
            this.boundaries = new float[6];

            this.objectTypeCountTable = new ObjectTypeCount[MaxObjectTypes];
            for (int i = 0; i < MaxObjectTypes; i++)
            {
                this.objectTypeCountTable[i] = new ObjectTypeCount();
            }

            var table = new LanguageTable(new[] { Language.English });
            this.stringTable = new StringTable(table);
            this.Objects = new TMapObjectList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapVariantData"/> class with the specified
        /// object table.
        /// </summary>
        public MapVariantData(TMapObjectList objectTable)
            : this()
        {
            this.Objects = objectTable;
        }

        /// <summary>
        /// Gets the objects in the map variant.
        /// </summary>
        public TMapObjectList Objects { get; private set; }

        /// <summary>
        /// Gets or sets the table of object labels referenced by objects in this map variant.
        /// </summary>
        public StringTable Labels
        {
            get { return this.stringTable; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.stringTable = value;
            }
        }

        /// <summary>
        /// Gets the array of min-max pairs for object X, Y, and Z coordinates.
        /// </summary>
        public float[] Boundaries
        {
            get { return this.boundaries; }
        }

        #region ISerializable<BitStream> Members

        public virtual void Serialize(BitStream s)
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
            s.Stream(ref this.budget);
            s.Stream(ref this.unk5);

            // Labels
            this.stringTable.Serialize(s, 12, 13, 9);

            // Object Table
            this.Objects.Serialize(s);

            // Object Type Count Table
            for (int i = 0; i < this.objectTypeCountTable.Length; i++)
            {
                if (i < this.objectTypeCount)
                {
                    s.Serialize(this.objectTypeCountTable[i]);
                }
            }
        }

        #endregion
    }
}
