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
using Nitrogen.ContentData.Metadata;
using Nitrogen.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;

namespace Nitrogen.ContentData
{
    /// <summary>
    /// Represents a content header.
    /// </summary>
    public class ContentHeader
        : Chunk
    {
        private ushort buildNumber;
        private ushort buildFlags;
        private ContentMetadata metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentHeader"/> class representing the
        /// content header of a particular content with the specified build number.
        /// </summary>
        /// <param name="buildNumber">The build number of the game to which the content belongs.</param>
        public ContentHeader(ushort buildNumber)
            : base("chdr", 10, 704)
        {
            this.metadata = new ContentMetadata(ContentType.NotApplicable);
            this.buildNumber = buildNumber;
        }

        [Flags]
        private enum BuildFlags
            : ushort
        {
            None,
            UntrackedBuild = 1 << 0,
        }

        /// <summary>
        /// Gets or sets the content metadata.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public ContentMetadata Metadata
        {
            get { return this.metadata; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.metadata = value;
            }
        }

        #region Chunk Members

        protected override ChunkFlags Flags { get { return ChunkFlags.IsHeader; } }

        protected override void SerializeEndianStreamData(EndianStream s)
        {
            s.Stream(ref this.buildNumber);
            s.Stream(ref this.buildFlags);
            s.Serialize(this.metadata);
        }

        #endregion
    }
}