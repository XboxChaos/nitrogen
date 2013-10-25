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

namespace Nitrogen.Megalo
{
    internal class MegaloDefinitionDatabase
        : IEnumerable<MegaloDefinition>
    {
        private Dictionary<int, MegaloDefinition> definitionsByOpcode;
        private Dictionary<string, MegaloDefinition> definitionsByName;

        internal MegaloDefinitionDatabase()
        {
            this.definitionsByName = new Dictionary<string, MegaloDefinition>();
            this.definitionsByOpcode = new Dictionary<int, MegaloDefinition>();
        }

        public IEnumerator<MegaloDefinition> GetEnumerator()
        {
            return definitionsByOpcode.Values.GetEnumerator();
        }

        internal int Add(MegaloDefinition definition)
        {
            definitionsByOpcode[definition.Opcode] = definition;

            if (!string.IsNullOrEmpty(definition.Name))
                definitionsByName[definition.Name] = definition;

            return -1;
        }

        internal void AddRange(IEnumerable<MegaloDefinition> definitions)
        {
            foreach (var definition in definitions)
                Add(definition);
        }

        internal MegaloDefinition Get(int opcode)
        {
            MegaloDefinition result;
            if (definitionsByOpcode.TryGetValue(opcode, out result))
                return result;

            return null;
        }

        internal MegaloDefinition Get(string name)
        {
            MegaloDefinition result;
            if (definitionsByName.TryGetValue(name, out result))
                return result;

            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return definitionsByOpcode.Values.GetEnumerator();
        }
    }
}
