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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nitrogen.Core.ContentData.MapVariants;
using Nitrogen.Core.IO;

namespace Nitrogen.Halo4.ContentData.MapVariants
{
    /// <summary>
    /// An object in a map variant.
    /// </summary>
    public abstract class Halo4MapVariantObject
        : ISerializable<BitStream>
    {
        private Halo4MapVariantObjectHeader header;

        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4MapVariantObject"/> class.
        /// Base values will be taken from a specified header.
        /// </summary>
        /// <param name="header">The header to base the object from.</param>
        internal Halo4MapVariantObject(Halo4MapVariantObjectHeader header)
        {
            this.header = header;
        }

        /// <summary>
        /// Gets the object's type.
        /// </summary>
        public ObjectType Type
        {
            get { return this.header.Type; }
        }

        /// <summary>
        /// Gets or sets the shape of the object's boundary.
        /// </summary>
        public IBoundary Shape
        {
            get { return this.header.Shape; }
            set { this.header.Shape = value; }
        }

        public abstract void Serialize(BitStream s);
    }
}
