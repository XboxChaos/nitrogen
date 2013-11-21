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

using Nitrogen.Core.Blf;
using Nitrogen.Core.ContentData;
using Nitrogen.Core.ContentData.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.Core
{
    /// <summary>
    /// Provides a base, game-neutral implementation of a blob which contains all necessary chunks
    /// of data in order to produce a base multiplayer variant.
    /// </summary>
    public abstract class MultiplayerVariantBlob
        : Blob
    {
        private ContentHeader header;
        private MultiplayerVariant mpvrData;

        protected MultiplayerVariantBlob(ushort buildNumber, MultiplayerVariant gameData)
        {
            Contract.Requires<ArgumentNullException>(gameData != null);

            this.mpvrData = gameData;

            this.header = new ContentHeader(buildNumber);
            this.header.Metadata = new GameVariantMetadata();
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
        /// Gets or sets the multiplayer variant contained in this blob.
        /// </summary>
        public MultiplayerVariant MultiplayerVariant
        {
            get { return this.mpvrData; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.mpvrData = value;
            }
        }

        #region Blob Members
        protected override void Initialize(IList<Blf.Chunk> contents)
        {
            contents.Add(this.header);
            contents.Add(this.mpvrData);
        }
        #endregion
    }
}