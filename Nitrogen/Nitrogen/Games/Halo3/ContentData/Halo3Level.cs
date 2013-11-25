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

using Nitrogen.ContentData;
using Nitrogen.ContentData.Localization;
using Nitrogen.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.Games.Halo3.ContentData
{
    /// <summary>
    /// Provides an overview of a level (map).
    /// </summary>
    /// <remarks>Represents the 'levl' chunk in a map info BLF file.</remarks>
    public class Halo3Level
        : Level
    {
        public const int InsertionPointCount = 4;
        private const int LanguageCount = 12;

        private string mapImageFileName, mapFileName;
        private int mapIndex, unk1;
        private byte unk2, unk3, maxTeamsNone, maxTeamsCTF, maxTeamsSlayer, maxTeamsOddball, maxTeamsKOTH, maxTeamsRace, maxTeamsHeadhunter, maxTeamsJuggernaut, maxTeamsTerritories, maxTeamsAssault, maxTeamsVIP, maxTeamsInfection, unk4, unk5;
        private int unk6;
        private List<InsertionPoint> insertionPoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="Halo3Level"/> class with default values.
        /// </summary>
        public Halo3Level()
            : base(version: 3)
        {
            Name = new LocalizedName(Halo3Properties.Languages, "");
            Description = new LocalizedDescription(Halo3Properties.Languages, "");

            this.mapImageFileName = "";
            this.mapFileName = "";
            this.maxTeamsNone = 8;
            this.maxTeamsCTF = 8;
            this.maxTeamsSlayer = 8;
            this.maxTeamsOddball = 8;
            this.maxTeamsKOTH = 8;
            this.maxTeamsRace = 8;
            this.maxTeamsHeadhunter = 8;
            this.maxTeamsJuggernaut = 8;
            this.maxTeamsTerritories = 8;
            this.maxTeamsAssault = 8;
            this.maxTeamsVIP = 8;
            this.maxTeamsInfection = 8;

            this.insertionPoints = new List<InsertionPoint>();
            for (int i = 0; i < InsertionPointCount; i++)
            {
                this.insertionPoints.Add(new InsertionPoint());
            }
        }

        /// <summary>
        /// Gets a collection of all insertion points in this level.
        /// </summary>
        /// <seealso cref="InsertionPointCount"/>
        public IReadOnlyList<InsertionPoint> InsertionPoints
        {
            get { return this.insertionPoints; }
        }

        /// <summary>
        /// Gets or sets the map file name (without the extension).
        /// </summary>
        public string MapFileName
        {
            get { return this.mapFileName; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.mapFileName = value;
            }
        }

        /// <summary>
        /// Gets or sets the map image file name (without the extension).
        /// </summary>
        public string MapImageFileName
        {
            get { return this.mapImageFileName; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.mapImageFileName = value;
            }
        }

        /// <summary>
        /// Gets or sets the map index.
        /// </summary>
        public int MapIndex
        {
            get { return this.mapIndex; }
            set { this.mapIndex = value; }
        }

        /// <summary>
        /// Gets or sets the max team count when no gametype is selcted.
        /// </summary>
        public byte MaxTeamsNone
        {
            get { return this.maxTeamsNone; }
            set { this.maxTeamsNone = value; }
        }

        /// <summary>
        /// Gets or sets the max team count for Capture the Flag.
        /// </summary>
        public byte MaxTeamsCTF
        {
            get { return this.maxTeamsCTF; }
            set { this.maxTeamsCTF = value; }
        }

        /// <summary>
        /// Gets or sets the max team for Slayer.
        /// </summary>
        public byte MaxTeamsSlayer
        {
            get { return this.maxTeamsSlayer; }
            set { this.maxTeamsSlayer = value; }
        }

        /// <summary>
        /// Gets or sets the max team for Oddball.
        /// </summary>
        public byte MaxTeamsOddball
        {
            get { return this.maxTeamsOddball; }
            set { this.maxTeamsOddball = value; }
        }

        /// <summary>
        /// Gets or sets the max team for King of the Hill.
        /// </summary>
        public byte MaxTeamsKOTH
        {
            get { return this.maxTeamsKOTH; }
            set { this.maxTeamsKOTH = value; }
        }

        /// <summary>
        /// Gets or sets the max team for Race.
        /// </summary>
        public byte MaxTeamsRace
        {
            get { return this.maxTeamsRace; }
            set { this.maxTeamsRace = value; }
        }

        /// <summary>
        /// Gets or sets the max team for Headhunter.
        /// </summary>
        public byte MaxTeamsHeadhunter
        {
            get { return this.maxTeamsHeadhunter; }
            set { this.maxTeamsHeadhunter = value; }
        }

        /// <summary>
        /// Gets or sets the max team for Juggernaut.
        /// </summary>
        public byte MaxTeamsJuggernaut
        {
            get { return this.maxTeamsJuggernaut; }
            set { this.maxTeamsJuggernaut = value; }
        }

        /// <summary>
        /// Gets or sets the max team for Territories.
        /// </summary>
        public byte MaxTeamsTerritories
        {
            get { return this.maxTeamsTerritories; }
            set { this.maxTeamsTerritories = value; }
        }

        /// <summary>
        /// Gets or sets the max team for Assault.
        /// </summary>
        public byte MaxTeamsAssault
        {
            get { return this.maxTeamsAssault; }
            set { this.maxTeamsAssault = value; }
        }

        /// <summary>
        /// Gets or sets the max team for VIP.
        /// </summary>
        public byte MaxTeamsVIP
        {
            get { return this.maxTeamsVIP; }
            set { this.maxTeamsVIP = value; }
        }

        /// <summary>
        /// Gets or sets the max team for Infection.
        /// </summary>
        public byte MaxTeamsInfection
        {
            get { return this.maxTeamsInfection; }
            set { this.maxTeamsInfection = value; }
        }

        #region Level Members

        protected override void SerializeEndianStreamData(EndianStream s)
        {
            base.SerializeEndianStreamData(s);

            s.Stream(ref this.mapImageFileName, Encoding.ASCII, 256);
            s.Stream(ref this.mapFileName, Encoding.ASCII, 256);
            s.Stream(ref this.mapIndex);
            s.Stream(ref this.unk1);
            s.Stream(ref this.unk2);
            s.Stream(ref this.unk3);
            s.Stream(ref this.maxTeamsNone);
            s.Stream(ref this.maxTeamsCTF);
            s.Stream(ref this.maxTeamsSlayer);
            s.Stream(ref this.maxTeamsOddball);
            s.Stream(ref this.maxTeamsKOTH);
            s.Stream(ref this.maxTeamsRace);
            s.Stream(ref this.maxTeamsHeadhunter);
            s.Stream(ref this.maxTeamsJuggernaut);
            s.Stream(ref this.maxTeamsTerritories);
            s.Stream(ref this.maxTeamsAssault);
            s.Stream(ref this.maxTeamsVIP);
            s.Stream(ref this.maxTeamsInfection);
            s.Stream(ref this.unk4);
            s.Stream(ref this.unk5);
            s.Stream(ref this.unk6);
            s.Serialize(this.insertionPoints, 0, this.insertionPoints.Count);
        }

        #endregion

        /// <summary>
        /// Represents an insertion point in a level.
        /// </summary>
        public class InsertionPoint
            : ISerializable<EndianStream>
        {
            private bool isUsed;
            private byte unk0;
            private byte unk1;
            private byte insertionZoneIndex;
            private int unk2;
            private LocalizedName name;
            private LocalizedDescription description;

            /// <summary>
            /// Initializes a new instance of the <see cref="InsertionPoint"/> class with default values.
            /// </summary>
            public InsertionPoint()
            {
                this.name = new LocalizedName(Halo3Properties.Languages, "");
                this.description = new LocalizedDescription(Halo3Properties.Languages, "");
            }

            /// <summary>
            /// Gets or sets the description of this insertion point.
            /// </summary>
            public LocalizedDescription Description
            {
                get { return this.description; }
                set
                {
                    Contract.Requires<ArgumentNullException>(value != null);
                    this.description = value;
                }
            }

            /// <summary>
            /// Gets or sets whether this insertion point is enabled.
            /// </summary>
            public bool Enabled
            {
                get { return this.isUsed; }
                set { this.isUsed = value; }
            }

            /// <summary>
            /// Gets or sets the name of this insertion point.
            /// </summary>
            public LocalizedName Name
            {
                get { return this.name; }
                set
                {
                    Contract.Requires<ArgumentNullException>(value != null);
                    this.name = value;
                }
            }

            /// <summary>
            /// Gets or sets the index of the zone this insertion point loads.
            /// </summary>
            public byte ZoneIndex
            {
                get { return this.insertionZoneIndex; }
                set { this.insertionZoneIndex = value; }
            }

            #region IEndianStreamSerializable Members

            public void Serialize(EndianStream s)
            {
                s.Stream(ref this.isUsed);
                s.Stream(ref this.unk0);
                s.Stream(ref this.unk1);
                s.Stream(ref this.insertionZoneIndex);
                s.Stream(ref this.unk2);
                this.name.Serialize(s);
                this.description.Serialize(s);
            }

            #endregion
        }
    }
}