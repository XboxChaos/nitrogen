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

namespace Nitrogen.Blob.Transport.BinaryTemplates.Shared.Enums
{
    /// <summary>
    /// Specifies the game engine.
    /// </summary>
    internal enum Engine
        : sbyte
    {
        /// <summary>
        /// Indicates that a multiplayer engine type is not applicable to this type of data.
        /// </summary>
        NotApplicable = -1,

        /// <summary>
        /// Indicates that a multiplayer engine type has yet to be specified.
        /// </summary>
        Unspecified,

        /// <summary>
        /// Indicates that the data is relevant to Forge.
        /// </summary>
        Forge,

        /// <summary>
        /// Indicates that the data is relevant to standard multiplayer.
        /// </summary>
        Standard,

        /// <summary>
        /// Indicates that the data is relevant to campaign.
        /// </summary>
        Campaign,

        /// <summary>
        /// Indicates that the data is relevant to Firefight (Halo: Reach).
        /// </summary>
        Firefight,

        /// <summary>
        /// Indicates that the data is relevant to Spartan Ops (Halo 4).
        /// </summary>
        SpartanOps,
    }
}
