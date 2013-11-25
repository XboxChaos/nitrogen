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

using Nitrogen.ContentData;
using Nitrogen.ContentData.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen
{
    /// <summary>
    /// Provides a base, game-neutral implementation of a blob which contains all necessary chunks
    /// of data in order to produce a base map variant.
    /// </summary>
    public abstract class MapVariantBlob
        : Blf.Blob
    {
        /// <summary>
        /// Specifies the maximum number of object types allowed.
        /// </summary>
        public const int MaximumObjectTypes = 256;

        private ContentHeader header;
        private MapVariant mvarData;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapVariantBlob"/> class with the specified
        /// game build number and map variant data.
        /// </summary>
        /// <param name="buildNumber">
        /// The build number of the game which is then used to generate a game-specific <see cref="ContentHeader"/>
        /// instance.
        /// </param>
        /// <param name="mapData">The data contained in this map variant.</param>
        protected MapVariantBlob(ushort buildNumber, MapVariant mapData)
        {
            Contract.Requires<ArgumentNullException>(mapData != null);

            this.mvarData = mapData;

            this.header = new ContentHeader(buildNumber);
            this.header.Metadata = new MapVariantMetadata();
        }

        /// <summary>
        /// Gets or sets the content header contained in this blob.
        /// </summary>
        public ContentHeader ContentHeader
        {
            get { return this.header; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.header = value;
            }
        }

        /// <summary>
        /// Gets or sets the map variant contained in this blob.
        /// </summary>
        public MapVariant MapVariant
        {
            get { return this.mvarData; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.mvarData = value;
            }
        }

        #region Blob Members

        protected override void Initialize(IList<Blf.Chunk> contents)
        {
            contents.Add(this.header);
            contents.Add(this.mvarData);
        }

        #endregion
    }
}