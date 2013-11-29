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
using System;
using System.Collections.Generic;

namespace Nitrogen.Games.Halo4
{
    /// <summary>
    /// Contains various static properties specific to Halo 4.
    /// </summary>
    public static class Halo4Properties
    {
        /// <summary>
        /// Specifies the default build number to use.
        /// </summary>
        public const ushort BuildNumber = 21401; // Halo 4 TU7

        /// <summary>
        /// Specifies the index of the Community category.
        /// </summary>
        public const sbyte CommunityCategoryIndex = 25;

        /// <summary>
        /// Provides a table of supported languages in Halo 4.
        /// </summary>
        public static readonly LanguageTable Languages = new LanguageTable(new [] {
            Language.English,
            Language.Japanese,
            Language.German,
            Language.French,
            Language.Spanish,
            Language.SpanishMexican,
            Language.Italian,
            Language.Korean,
            Language.Chinese,
            Language.Unused,
            Language.Portuguese,
            Language.Polish,
            Language.Russian,
            Language.Danish,
            Language.Finnish,
            Language.Dutch,
            Language.Norwegian,
        });

        /// <summary>
        /// Provides a string table containing the name of the Community category.
        /// </summary>
        public static readonly StringTable CommunityCategoryName = new StringTable(Languages);

        static Halo4Properties()
        {
            CommunityCategoryName.Add(new LocalizedString(Languages, "Community"));
        }
    }
}
