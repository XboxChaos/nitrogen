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

using Nitrogen.Megalo.Transport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nitrogen.Megalo
{
    internal sealed class MegaloScriptDatabase
    {
        private IDefinitionsTransport transport;

        internal MegaloScriptDatabase(IDefinitionsTransport transport)
        {
            this.transport = transport;
        }

        public MegaloDefinitionDatabase Definitions { get; private set; }

        internal MegaloDefinition GetDefinition(int opcode)
        {
            // Lazy initialization; read the database when needed.
            if (Definitions == null)
                Definitions = transport.ReadDefinitions();

            // Attempt to get the condition definition.
            var definition = Definitions.Get(opcode);
            if (definition != null)
                return definition;

            throw new Exception(string.Format("Condition with opcode {0} was not found.", opcode));
        }
    }
}
