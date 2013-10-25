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

namespace Nitrogen.Content.Shared
{
    /// <summary>
    /// Represents a Xbox LIVE identity.
    /// </summary>
    [Synchronizable]
    public class XIdentity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XIdentity"/> class with default values.
        /// </summary>
        public XIdentity() : this("Nitrogen") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="XIdentity"/> class with the given Gamertag.
        /// </summary>
        /// <param name="gamertag">The Gamertag associated with this identity.</param>
        public XIdentity(string gamertag)
        {
            UserId = 0;
            SignedIntoXboxLive = false;
            Gamertag = gamertag;
        }

        /// <summary>
        /// Gets or sets the user id of this identity.
        /// </summary>
        [PropertyBinding("UserId")]
        public ulong UserId { get; set; }

        /// <summary>
        /// Gets or sets the Gamertag associated with this identity.
        /// </summary>
        [PropertyBinding("Gamertag")]
        public string Gamertag { get; set; }

        /// <summary>
        /// Gets or sets a boolean value specifying whether this user was signed into Xbox LIVE at
        /// the time when this content was created or modified.
        /// </summary>
        [PropertyBinding("SignedIntoXboxLive")]
        public bool SignedIntoXboxLive { get; set; }
    }
}
