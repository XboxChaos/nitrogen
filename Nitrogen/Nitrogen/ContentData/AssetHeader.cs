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

using Nitrogen.Blf;
using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.ContentData
{
    /// <summary>
    /// Represents a header to a game asset.
    /// </summary>
    /// <remarks>Represents the 'athr' chunk in a BLF file.</remarks>
    public class AssetHeader
        : Chunk
    {
        private string description1, description2, buildString;
        private int buildNumber, unk0;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetHeader"/> class with default values.
        /// </summary>
        public AssetHeader()
            : base("athr", 3)
        {
            this.description1 = "";
            this.description2 = "";
            this.buildString = "00000.00.00.00.0343.wumbo";
        }

        /// <summary>
        /// Gets or sets the build number of the game targeted by this asset.
        /// </summary>
        public short BuildNumber
        {
            get { return (short)this.buildNumber; }
            set { this.buildNumber = value; }
        }

        /// <summary>
        /// Gets or sets the build string of the game targeted by this asset.
        /// </summary>
        public string BuildString
        {
            get { return this.buildString; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(Encoding.ASCII.GetByteCount(value) <= 28);

                this.buildString = value;
            }
        }

        #region Chunk Members

        protected override void SerializeEndianStreamData(EndianStream s)
        {
            s.StreamString(ref this.description1, Encoding.ASCII, 16);
            s.Stream(ref this.buildNumber);
            s.Stream(ref this.unk0);
            s.StreamString(ref this.buildString, Encoding.ASCII, 28);
            s.StreamString(ref this.description2, Encoding.ASCII, 16);
        }

        #endregion
    }
}
