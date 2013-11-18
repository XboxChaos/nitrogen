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
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.Core
{
    /// <summary>
    /// Provides a base implementation of a map info blob.
    /// </summary>
    public abstract class MapInfoBlob
        : Blob
    {
        private Level levelInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapInfoBlob"/> class with the specified
        /// <see cref="Level"/> object.
        /// </summary>
        /// <param name="level">
        /// A <see cref="Nitrogen.Core.ContentData.Level"/> object to use as a default value.
        /// </param>
        public MapInfoBlob(Level level)
        {
            Contract.Requires<ArgumentNullException>(level != null);
            this.levelInfo = level;
        }

        /// <summary>
        /// Gets or sets the level (map) information.
        /// </summary>
        public Level Level
        {
            get { return this.levelInfo; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.levelInfo = value;
            }
        }

        #region Blob Members

        protected override bool IsSigned { get { return true; } }

        protected override void Initialize(IList<Chunk> contents)
        {
            contents.Add(this.levelInfo);
        }

        #endregion
    }
}
