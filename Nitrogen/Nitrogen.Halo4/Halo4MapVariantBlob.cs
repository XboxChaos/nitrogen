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

using Nitrogen.Core;
using Nitrogen.Halo4.ContentData;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.Halo4
{
    /// <summary>
    /// Represents a blob containing all necessary chunks of data to produce a valid Halo 4 map variant.
    /// </summary>
    public class Halo4MapVariantBlob
        : MapVariantBlob
    {
        /// <summary>
        /// Specifies the default build number to use.
        /// </summary>
        public const ushort BuildNumber = 21401; // Halo 4 TU7

        /// <summary>
        /// Specifies the maximum amount of objects which can be placed in a map variant.
        /// </summary>
        public const int MaximumMapObjects = 651;

        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4MapVariantBlob"/> class with default
        /// values.
        /// </summary>
        public Halo4MapVariantBlob()
            : base(BuildNumber, new ContentData.Halo4MapVariant()) { }

        /// <summary>
        /// Gets or sets the map variant contained in this blob.
        /// </summary>
        public new Halo4MapVariant MapVariant
        {
            get { return base.MapVariant as Halo4MapVariant; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                base.MapVariant = value;
            }
        }
    }
}
