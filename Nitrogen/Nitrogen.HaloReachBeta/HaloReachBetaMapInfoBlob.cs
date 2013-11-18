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
using Nitrogen.HaloReachBeta.ContentData;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.HaloReachBeta
{
    /// <summary>
    /// Represents a map info blob for Halo: Reach Beta maps.
    /// </summary>
    public class HaloReachBetaMapInfoBlob
        : MapInfoBlob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HaloReachBetaMapInfoBlob"/> with default values.
        /// </summary>
        public HaloReachBetaMapInfoBlob()
            : base(new HaloReachBetaLevel()) { }

        /// <summary>
        /// Gets or sets the level (map) information.
        /// </summary>
        public new HaloReachBetaLevel Level
        {
            get { return base.Level as HaloReachBetaLevel; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                base.Level = value;
            }
        }
    }
}
