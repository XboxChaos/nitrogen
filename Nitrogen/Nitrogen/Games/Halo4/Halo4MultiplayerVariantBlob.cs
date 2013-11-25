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

using Nitrogen;
using Nitrogen.Games.Halo4.ContentData;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4
{
    /// <summary>
    /// Represents a blob containing all necessary chunks of data to produce a valid Halo 4
    /// multiplayer variant.
    /// </summary>
    public class Halo4MultiplayerVariantBlob
        : MultiplayerVariantBlob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4MultiplayerVariant"/> class with
        /// default values.
        /// </summary>
        public Halo4MultiplayerVariantBlob()
            : base(Halo4Properties.BuildNumber, new Halo4MultiplayerVariant()) { }

        /// <summary>
        /// Gets or sets the multiplayer variant contained in this blob.
        /// </summary>
        public new Halo4MultiplayerVariant MultiplayerVariant
        {
            get { return base.MultiplayerVariant as Halo4MultiplayerVariant; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                base.MultiplayerVariant = value;
            }
        }
    }
}
