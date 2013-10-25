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
using System.Collections.Generic;

namespace Nitrogen.Megalo
{
    internal class ParameterDefinition
    {
        internal ParameterType Type { get; set; }

        internal string Name { get; set; }

        internal IEnumerable<Constraints.IConstraint> Constraints { get; set; }

        internal bool Nullable { get; set; }

        internal bool Unsigned { get; set; }

        internal bool PlusOneEncoding { get; set; }

        internal float MinFloat { get; set; }

        internal float MaxFloat { get; set; }

        internal bool FloatFlag2 { get; set; }

        internal int BitSize { get; set; }
    }
}
