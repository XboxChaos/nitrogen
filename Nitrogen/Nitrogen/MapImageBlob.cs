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

using Nitrogen.Blf;
using Nitrogen.ContentData;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen
{
    /// <summary>
    /// Represents a blob containing a map image.
    /// </summary>
    public class MapImageBlob
        : Blob
    {
        private MapImage image;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapImageBlob"/> class with default values.
        /// </summary>
        public MapImageBlob()
        {
            this.image = new MapImage();
        }

        /// <summary>
        /// Gets or sets the map image data contained in this blob.
        /// </summary>
        public MapImage ImageData
        {
            get { return this.image; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.image = value;
            }
        }

        #region Blob Members

        protected override bool IsSigned { get { return true; } }

        protected override void Initialize(IList<Chunk> contents)
        {
            contents.Add(this.image);
        }

        #endregion
    }
}
