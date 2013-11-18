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
using Nitrogen.Halo4.ContentData;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.Halo4
{
    /// <summary>
    /// Represents a blob containing an instance of <see cref="FileMegaloCategories"/>.
    /// </summary>
    public class FileMegaloCategoriesBlob
        : Blob
    {
        private FileMegaloCategories data;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileMegaloCategoriesBlob"/> class with default values.
        /// </summary>
        public FileMegaloCategoriesBlob()
        {
            this.data = new FileMegaloCategories();
        }

        /// <summary>
        /// Gets or sets the categories contained in this blob.
        /// </summary>
        public FileMegaloCategories Categories
        {
            get { return this.data; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.data = value;
            }
        }

        #region Blob Members

        protected override void Initialize(IList<Chunk> contents)
        {
            contents.Add(this.data);
        }

        #endregion
    }
}
