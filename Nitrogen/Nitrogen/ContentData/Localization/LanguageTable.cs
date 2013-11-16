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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace Nitrogen.Core.ContentData.Localization
{
    /// <summary>
    /// Represents a table of supported languages.
    /// </summary>
    public sealed class LanguageTable
        : IReadOnlyList<Language>
    {
        private IList<Language> table;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageTable"/> class with the given
        /// collection of languages.
        /// </summary>
        /// <param name="languages">A collection of supported languages.</param>
        public LanguageTable(IEnumerable<Language> languages)
        {
            this.table = new List<Language>(languages);
        }

        /// <summary>
        /// Gets the number of languages in this table.
        /// </summary>
        public int Count
        {
            get { return this.table.Count; }
        }

        /// <summary>
        /// Gets the index of the specified <paramref name="language"/> in this table.
        /// </summary>
        /// <param name="language">The <see cref="Language"/> to search for.</param>
        /// <returns>The index of the language if found; otherwise, -1.</returns>
        public int this[Language language]
        {
            get { return this.table.IndexOf(language); }
        }

        /// <summary>
        /// Gets a boolean value specifying whether the specified <paramref name="language"/> is
        /// included in this table.
        /// </summary>
        /// <param name="language">The <see cref="Language"/> to search for.</param>
        /// <returns>true if supported; otherwise, false.</returns>
        [Pure]
        public bool LanguageSupported(Language language)
        {
            return this.table.Contains(language);
        }

        #region IReadOnlyList Members
        /// <summary>
        /// Gets a <see cref="Language"/> value from the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index in the table.</param>
        /// <returns>The equivalent <see cref="Language"/> value.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException"/>
        public Language this[int index]
        {
            get { return this.table[index]; }
        }

        public IEnumerator<Language> GetEnumerator()
        {
            return this.table.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.table.GetEnumerator();
        }
        #endregion
    }
}
