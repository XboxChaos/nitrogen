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

namespace Nitrogen.ContentData.Metadata
{
    /// <summary>
    /// Provides metadata for a game variant.
    /// </summary>
    public class GameVariantMetadata
        : ContentMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameVariantMetadata"/> class with default
        /// values specialized for game variants.
        /// </summary>
        public GameVariantMetadata()
            : base(ContentType.GameVariant) { }
    }
}