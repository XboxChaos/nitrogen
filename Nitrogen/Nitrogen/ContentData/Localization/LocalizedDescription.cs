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

using Nitrogen.Core.IO;
using System;
using System.Text;

namespace Nitrogen.Core.ContentData.Localization
{
    public class LocalizedDescription
        : LocalizedString
    {
        public LocalizedDescription(LanguageTable map, string description)
            : base(map, description) { }

        public override void Serialize(EndianStream s)
        {
            foreach (Language language in Map)
            {
                string value = Get(language);
                s.Stream(ref value, Encoding.BigEndianUnicode, 256);
                Set(language, value);
            }
        }
    }
}