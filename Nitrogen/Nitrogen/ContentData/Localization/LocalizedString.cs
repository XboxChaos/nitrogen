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

using Nitrogen.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.ContentData.Localization
{
    public class LocalizedString
        : ISerializable<EndianStream>
    {
        private Dictionary<Language, string> table;

        public LocalizedString(LanguageTable map)
        {
            Map = map;
            this.table = new Dictionary<Language, string>();
        }

        public LocalizedString(LanguageTable map, string value)
            : this(map)
        {
            Set(value);
        }

        protected LanguageTable Map { get; private set; }

        public virtual void Set(string value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            foreach (var language in Map)
                Set(language, value);
        }

        public virtual void Set(Language language, string value)
        {
            Contract.Requires<ArgumentNullException>(value != null);
            Contract.Requires<ArgumentException>(Map.LanguageSupported(language));

            this.table[language] = value;
        }

        public virtual string Get(Language language)
        {
            Contract.Requires<ArgumentException>(Map.LanguageSupported(language));
            return this.table[language];
        }

        public virtual void Serialize(EndianStream s)
        {

        }
    }
}
