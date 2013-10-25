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

using Nitrogen.Megalo;
using Nitrogen.Megalo.Transport;
using System;

namespace Nitrogen
{
    /// <summary>
    /// Provides a registry of supported games.
    /// </summary>
    internal static class GameRegistry
    {
        // Halo: Reach isn't 100% supported at this time.
        internal static Game HaloReach = new Game
        {
            Name = "Halo: Reach",
            Sha1Salt = new byte[] {
                0xED, 0xD4, 0x30, 0x09, 0x66, 0x6D, 0x5C, 0x4A, 0x5C, 0x36, 0x57,
                0xFA, 0xB4, 0x0E, 0x02, 0x2F, 0x53, 0x5A, 0xC6, 0xC9, 0xEE, 0x47,
                0x1F, 0x01, 0xF1, 0xA4, 0x47, 0x56, 0xB7, 0x71, 0x4F, 0x1C, 0x36, 
                0xEC
            }
        };

        // Halo 4 Beta isn't 100% supported at this time.
        internal static Game Halo4Beta = new Game
        {
            Name = "Halo 4 Beta",
            InitialBuildNumber = 14064,
            Sha1Salt = new byte[] {
                0xED, 0xD4, 0x30, 0x09, 0x66, 0x6D, 0x5C, 0x4A, 0x5C, 0x36, 0x57,
                0xFA, 0xB4, 0x0E, 0x02, 0x2F, 0x53, 0x5A, 0xC6, 0xC9, 0xEE, 0x47,
                0x1F, 0x01, 0xF1, 0xA4, 0x47, 0x56, 0xB7, 0x71, 0x4F, 0x1C, 0x36, 
                0xEC
            }
        };

        internal static Game Halo4 = new Game
        {
            Name = "Halo 4",
            MegaloConditionsDatabase = new MegaloScriptDatabase(new XmlDefinitionsTransport("Nitrogen.Assets.Halo4.H4_Megalo_Conditions.xml", true)),
            MegaloActionsDatabase = new MegaloScriptDatabase(new XmlDefinitionsTransport("Nitrogen.Assets.Halo4.H4_Megalo_Actions.xml", true)),
            InitialBuildNumber = 20810,
            Sha1Salt = new byte[] {
                0xED, 0xD4, 0x30, 0x09, 0x66, 0x6D, 0x5C, 0x4A, 0x5C, 0x36, 0x57,
                0xFA, 0xB4, 0x0E, 0x02, 0x2F, 0x53, 0x5A, 0xC6, 0xC9, 0xEE, 0x47,
                0x1F, 0x01, 0xF1, 0xA4, 0x47, 0x56, 0xB7, 0x71, 0x4F, 0x1C, 0x36, 
                0xEC
            }
        };

        /// <summary>
        /// Provides information on a supported game.
        /// </summary>
        internal struct Game
        {
            /// <summary>
            /// Gets or sets the name of this game.
            /// </summary>
            internal string Name { get; set; }

            /// <summary>
            /// Gets or sets the reference to the Megalo conditions database for this game.
            /// </summary>
            internal MegaloScriptDatabase MegaloConditionsDatabase { get; set; }

            /// <summary>
            /// Gets or sets the reference to the Megalo actions database for this game.
            /// </summary>
            internal MegaloScriptDatabase MegaloActionsDatabase { get; set; }

            /// <summary>
            /// Gets or sets the starting build number of this game.
            /// </summary>
            internal int InitialBuildNumber { get; set; }

            /// <summary>
            /// Gets or sets the salt used for the SHA-1 hash.
            /// </summary>
            internal byte[] Sha1Salt { get; set; }
        }
    }
}