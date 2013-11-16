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

using Nitrogen.Core.IO;
using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.Core.ContentData.Metadata
{
    /// <summary>
    /// Represents an author of a user-generated content.
    /// </summary>
    public class Author
        : ISerializable<EndianStream>, ISerializable<BitStream>
    {
        /*
         * According to kornman, the XUID of the player is different based on the player's online
         * state. The "signed into Xbox LIVE" field exists for the game to differentiate between
         * a player's online and offline XUIDs.
         */

        private String gamertag;
        private bool signedIntoXboxLive;
        private long xuid;

        /// <summary>
        /// Creates a new instance of the <see cref="Author"/> class with the specified Gamertag.
        /// </summary>
        /// <remarks>The XUID will be set to 0 and cannot be modified.</remarks>
        /// <param name="gamertag">This author's Gamertag.</param>
        public Author(string gamertag)
        {
            this.gamertag = gamertag;
            this.xuid = 0;
            this.signedIntoXboxLive = false;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Author"/> class with the default Gamertag,
        /// which is "Nitrogen".
        /// </summary>
        public Author()
            : this("Nitrogen") { }

        /// <summary>
        /// Gets or sets the Gamertag of this author.
        /// </summary>
        public String Gamertag
        {
            get { return this.gamertag; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(Encoding.ASCII.GetByteCount(value) <= 16);

                this.gamertag = value;
            }
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.xuid);
            s.Stream(ref this.gamertag, Encoding.ASCII);
            s.Stream(ref this.signedIntoXboxLive);
        }

        #endregion

        #region ISerializable<EndianStream> Members

        public void Serialize(EndianStream s)
        {
            s.Stream(ref this.xuid);
            s.Stream(ref this.gamertag, Encoding.ASCII, 16);
            s.Stream(ref this.signedIntoXboxLive);
            s.Pad(3);
        }

        #endregion
    }
}
