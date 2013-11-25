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
using Nitrogen.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.Games.Halo4.ContentData
{
    /// <summary>
    /// Provides a table of default categories in a specific language.
    /// </summary>
    /// <remarks>Represents the 'fmca' chunk in a BLF file.</remarks>
    public class FileMegaloCategories
        : Chunk
    {
        private List<MegaloCategory> categories;

        public FileMegaloCategories()
            : base("fmca", 1)
        {
            this.categories = new List<MegaloCategory>();
        }

        /// <summary>
        /// Gets or sets the category located at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index in the category table.</param>
        /// <returns>
        /// A <see cref="MegaloCategory"/> instance stored at the specified <paramref name="index"/>.
        /// </returns>
        public MegaloCategory this[int index]
        {
            get { return this.categories[index]; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.categories[index] = value;
            }
        }

        /// <summary>
        /// Gets the category located at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index in the category table.</param>
        /// <returns>
        /// A <see cref="MegaloCategory"/> instance stored at the specified <paramref name="index"/>.
        /// </returns>
        public MegaloCategory Get(int index)
        {
            return this.categories[index];
        }

        #region Chunk Members

        protected override void SerializeEndianStreamData(EndianStream s)
        {
            int count = this.categories.Count;
            s.Stream(ref count);
            s.Serialize(this.categories, 0, count);
        }

        #endregion

        /// <summary>
        /// Represents a category in a <see cref="FileMegaloCategories"/> table.
        /// </summary>
        public class MegaloCategory
            : ISerializable<EndianStream>
        {
            private int id;
            private string name;

            /// <summary>
            /// Initializes a new instance of the <see cref="MegaloCategory"/> class with default values.
            /// </summary>
            public MegaloCategory()
            {
                this.name = "Undefined";
            }

            /// <summary>
            /// Gets or sets the category ID.
            /// </summary>
            public int Id
            {
                get { return this.id; }
                set { this.id = value; }
            }

            /// <summary>
            /// Gets or sets the non-localized name of this category.
            /// </summary>
            public string Name
            {
                get { return this.name; }
                set
                {
                    Contract.Requires<ArgumentNullException>(value != null);
                    Contract.Requires<ArgumentException>(Encoding.BigEndianUnicode.GetByteCount(value) <= 64);

                    this.name = value;
                }
            }

            #region ISerializable<EndianStream> Members

            public void Serialize(EndianStream s)
            {
                s.Stream(ref this.id);
                s.Stream(ref this.name, Encoding.BigEndianUnicode, 64);
            }

            #endregion
        }
    }
}
