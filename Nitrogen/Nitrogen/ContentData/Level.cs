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
using Nitrogen.ContentData.Localization;
using Nitrogen.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.ContentData
{
    /// <summary>
    /// Specifies the properties of a level.
    /// </summary>
    [Flags]
    public enum LevelFlags
    {
        None = 0,
        Unknown0 = 1 << 0,
        Unknown1 = 1 << 1,
        Visible = 1 << 2,
        GeneratesFilm = 1 << 3,
        IsMainMenu = 1 << 4,
        IsCampaign = 1 << 5,
        IsMultiplayer = 1 << 6,
        IsDLC = 1 << 7,
        Unknown8 = 1 << 8,
        Unknown9 = 1 << 9,
        IsFirefight = 1 << 10,
        IsCinematic = 1 << 11,
        IsForgeOnly = 1 << 12,
        Unknown13 = 1 << 13,
        Unknown14 = 1 << 14,
        Unknown15 = 1 << 15,
    }

    /// <summary>
    /// Provides a base implementation of an overview of a level (map).
    /// </summary>
    /// <remarks>Represents the 'levl' chunk in a map info BLF file.</remarks>
    public abstract class Level
        : Chunk
    {
        private int flags;
        private int mapId;
        private LocalizedString name;
        private LocalizedString description;

        /// <summary>
        /// Initializes a new instance of the <see cref="Level"/> class with the specified version
        /// and default values.
        /// </summary>
        /// <param name="version">The version of this instance.</param>
        public Level(short version)
            : base("levl", version)
        {
            this.mapId = -1;
            this.flags = (int)(LevelFlags.Visible | LevelFlags.GeneratesFilm);
        }

        /// <summary>
        /// Gets or sets the localized description of this level.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public LocalizedString Description
        {
            get { return this.description; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.description = value;
            }
        }

        /// <summary>
        /// Gets or sets the properties of this level.
        /// </summary>
        public new LevelFlags Flags
        {
            get { return (LevelFlags)this.flags; }
            set { this.flags = (int)value; }
        }

        /// <summary>
        /// Gets or sets the id of this level.
        /// </summary>
        public int MapId
        {
            get { return this.mapId; }
            set { this.mapId = value; }
        }

        /// <summary>
        /// Gets or sets the localized name of this level.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public LocalizedString Name
        {
            get { return this.name; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.name = value;
            }
        }

        #region Chunk Members

        protected override void SerializeEndianStreamData(IO.EndianStream s)
        {
            s.Stream(ref this.mapId);
            s.Stream(ref this.flags);
            this.name.Serialize(s);
            this.description.Serialize(s);
        }

        #endregion
    }
}
