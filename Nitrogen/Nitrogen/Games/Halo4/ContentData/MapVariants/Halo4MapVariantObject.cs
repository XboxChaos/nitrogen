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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nitrogen.ContentData.MapVariants;
using Nitrogen.IO;

namespace Nitrogen.Games.Halo4.ContentData.MapVariants
{
    /// <summary>
    /// An object in a map variant.
    /// </summary>
    public abstract class Halo4MapVariantObject
        : ISerializable<BitStream>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4MapVariantObject"/> class.
        /// Base values will be taken from a specified header.
        /// </summary>
        /// <param name="header">The header to base the object from.</param>
        internal Halo4MapVariantObject(Halo4MapVariantObjectHeader header)
        {
            this.Header = header;
        }

        /// <summary>
        /// Gets the object's type.
        /// </summary>
        public ObjectType Type
        {
            get { return this.Header.Type; }
        }

        /// <summary>
        /// Gets or sets the shape of the object's boundary.
        /// </summary>
        public IBoundary Shape
        {
            get { return this.Header.Shape; }
            set { this.Header.Shape = value; }
        }

        /// <summary>
        /// Gets or sets the object's X coordinate.
        /// </summary>
        public float X
        {
            get { return this.Header.X; }
            set { this.Header.X = value; }
        }

        /// <summary>
        /// Gets or sets the object's Y coordinate.
        /// </summary>
        public float Y
        {
            get { return this.Header.Y; }
            set { this.Header.Y = value; }
        }

        /// <summary>
        /// Gets or sets the object's Z coordinate.
        /// </summary>
        public float Z
        {
            get { return this.Header.Z; }
            set { this.Header.Z = value; }
        }

        /// <summary>
        /// Gets the object's header information.
        /// </summary>
        internal Halo4MapVariantObjectHeader Header { get; private set; }

        public virtual void Serialize(BitStream s)
        {
            if (s.State == StreamState.Write)
                this.Header.Serialize(s);
        }
    }
}
