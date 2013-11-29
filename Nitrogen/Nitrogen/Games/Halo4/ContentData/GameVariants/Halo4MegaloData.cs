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

using Nitrogen.ContentData.Localization;
using Nitrogen.ContentData.Traits;
using Nitrogen.Games.Halo4.ContentData.GameVariants.Megalo;
using Nitrogen.Games.Halo4.ContentData.Traits;
using Nitrogen.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants
{
    public class Halo4MegaloData
        : ISerializable<BitStream>
    {
        public const int MaxUserDefinedTraits = 16;

        private List<UserDefinedTraits<Halo4PlayerTraits>> userDefinedTraits;
        private List<UserDefinedOption> userDefinedOptions;
        private StringTable userDefinedStrings, name, description, introDescription, categoryName;
        private byte baseNameStringIndex;
        private sbyte iconIndex, categoryIndex;
        private List<ushort> mapPermissions;
        private bool mapPermissionIsExclusive;
        private int[] playerRatingParams;
        private bool unk0;
        private GlobalGameOptions globalOptions;
        private List<MapLoadout> mapLoadouts;

        public Halo4MegaloData()
        {
            this.userDefinedTraits = new List<UserDefinedTraits<Halo4PlayerTraits>>();
            this.userDefinedOptions = new List<UserDefinedOption>();
            this.userDefinedStrings = new StringTable(Halo4Properties.Languages);
            this.name = new StringTable(Halo4Properties.Languages);
            this.description = new StringTable(Halo4Properties.Languages);
            this.introDescription = new StringTable(Halo4Properties.Languages);
            this.categoryName = new StringTable(Halo4Properties.Languages);
            this.categoryIndex = Halo4Properties.CommunityCategoryIndex;
            this.mapPermissions = new List<ushort>();
            this.playerRatingParams = new int[15];
            this.globalOptions = new GlobalGameOptions();
            this.mapLoadouts = new List<MapLoadout>();
        }

        /// <summary>
        /// Gets or sets the user-defined trait sets.
        /// </summary>
        public List<UserDefinedTraits<Halo4PlayerTraits>> UserDefinedTraits
        {
            get { return this.userDefinedTraits; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.userDefinedTraits = value;
            }
        }

        /// <summary>
        /// Gets or sets the user-defined game variant options.
        /// </summary>
        public List<UserDefinedOption> UserDefinedOptions
        {
            get { return this.userDefinedOptions; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.userDefinedOptions = value;
            }
        }

        /// <summary>
        /// Gets or sets the string table containing user-defined strings.
        /// </summary>
        public StringTable UserDefinedStrings
        {
            get { return this.userDefinedStrings; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.userDefinedStrings = value;
            }
        }

        /// <summary>
        /// Gets or sets the localized name of this game variant.
        /// </summary>
        public StringTable LocalizedName
        {
            get { return this.name; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the localized description of this game variant.
        /// </summary>
        public StringTable LocalizedDescription
        {
            get { return this.description; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.description = value;
            }
        }

        /// <summary>
        /// Gets or sets a set of global game options for this game variant.
        /// </summary>
        public GlobalGameOptions GlobalGameOptions
        {
            get { return this.globalOptions; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.globalOptions = value;
            }
        }

        public List<MapLoadout> MapLoadouts
        {
            get { return this.mapLoadouts; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.mapLoadouts = value;
            }
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            var traitCount = this.userDefinedTraits.Count;
            s.Stream(ref traitCount, 5);
            for (int i = 0; i < traitCount; i++)
            {
                if (s.State == StreamState.Read)
                    this.userDefinedTraits.Add(new UserDefinedTraits<Halo4PlayerTraits>());

                s.Serialize(this.userDefinedTraits[i]);
            }

            var optionCount = this.userDefinedOptions.Count;
            s.Stream(ref optionCount, 5);
            for (int i = 0; i < optionCount; i++)
            {
                if (s.State == StreamState.Read)
                    this.userDefinedOptions.Add(new UserDefinedOption());

                s.Serialize(this.userDefinedOptions[i]);
            }

            this.userDefinedStrings.Serialize(s, 16, 16, 8);
            s.Stream(ref this.baseNameStringIndex);
            this.name.Serialize(s, 11, 11, 1);
            this.description.Serialize(s, 13, 13, 1);
            this.introDescription.Serialize(s, 13, 13, 1);
            this.categoryName.Serialize(s, 10, 10, 1);
            s.StreamPlusOne(ref this.iconIndex, 5);
            s.StreamPlusOne(ref this.categoryIndex, 5);

            int permissionCount = this.mapPermissions.Count;
            s.Stream(ref permissionCount, 6);
            if (permissionCount >= 0 && permissionCount < 32)
            {
                if (s.State == StreamState.Read)
                    this.mapPermissions = new List<ushort>(new ushort[permissionCount]);

                s.Stream(this.mapPermissions, 0, permissionCount);
                s.Stream(ref this.mapPermissionIsExclusive);
            }

            s.Stream(this.playerRatingParams, 0, this.playerRatingParams.Length);
            s.Stream(ref this.unk0);
            s.Serialize(this.globalOptions);

            int mapLoadoutsCount = this.mapLoadouts.Count;
            s.Stream(ref mapLoadoutsCount);
            if (s.State == StreamState.Read)
            {
                this.mapLoadouts = new List<MapLoadout>();
                for (int i = 0; i < mapLoadoutsCount; i++)
                    this.mapLoadouts.Add(new MapLoadout());
            }
            s.Serialize(this.mapLoadouts, 0, mapLoadoutsCount);


            /* 
                { "MenuFlags", DefineMenuFlags },
                { "Conditions", DefineConditions },
                { "Actions", DefineActions },
                { "Triggers", DefineTriggers },
                { "CustomStats", DefineCustomStats },
                { "Variables", DefineVariables },
                { "Widgets", DefineHudWidgets },
                { "Events", DefineEvents },
                { "RequiredObjectTypes", DefineRequiredObjectTypes },
                { "ObjectFilters", DefineObjectFilters },
                { "CandySpawnerFilters", DefineCandySpawnersFilters },*/
        }

        #endregion
    }
}
