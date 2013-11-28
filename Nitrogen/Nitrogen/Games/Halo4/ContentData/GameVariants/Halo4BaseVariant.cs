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

using Nitrogen.IO;
using Nitrogen.Games.Halo4.ContentData.GameVariants.BaseVariant;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants
{
    /// <summary>
    /// Represents a set of data found in all multiplayer variants.
    /// </summary>
    public class Halo4BaseVariant
        : ISerializable<BitStream>
    {
        private bool unk0, unk1;
        private GeneralSettings general;
        private PrototypeSettings prototype;
        private RespawnSettings respawn;
        private SocialSettings social;
        private MapOverrides mapOverrides;
        private RequisitionSettings requisition;
        private TeamSettings teams;
        private LoadoutSettings loadouts;
        private OrdnanceSettings ordnance;

        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4BaseVariant"/> class with default values.
        /// </summary>
        public Halo4BaseVariant()
        {
            this.general = new GeneralSettings();
            this.prototype = new PrototypeSettings();
            this.respawn = new RespawnSettings();
            this.social = new SocialSettings();
            this.mapOverrides = new MapOverrides();
            this.requisition = new RequisitionSettings();
            this.teams = new TeamSettings();
            this.loadouts = new LoadoutSettings();
            this.ordnance = new OrdnanceSettings();
        }

        /// <summary>
        /// Gets or sets the general settings for this multiplayer variant.
        /// </summary>
        public GeneralSettings GeneralSettings
        {
            get { return this.general; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.general = value;
            }
        }

        /// <summary>
        /// Gets or sets the respawn settings for this multiplayer variant.
        /// </summary>
        public RespawnSettings RespawnSettings
        {
            get { return this.respawn; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.respawn = value;
            }
        }

        /// <summary>
        /// Gets or sets the social settings for this multiplayer variant.
        /// </summary>
        public SocialSettings SocialSettings
        {
            get { return this.social; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.social = value;
            }
        }

        /// <summary>
        /// Gets or sets the map overrides for this multiplayer variant.
        /// </summary>
        public MapOverrides MapOverrides
        {
            get { return this.mapOverrides; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.mapOverrides = value;
            }
        }

        /// <summary>
        /// Gets or sets the team settings for this multiplayer variant.
        /// </summary>
        public TeamSettings Teams
        {
            get { return this.teams; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.teams = value;
            }
        }

        /// <summary>
        /// Gets or sets the loadouts for this multiplayer variant.
        /// </summary>
        public LoadoutSettings Loadouts
        {
            get { return this.loadouts; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.loadouts = value;
            }
        }

        /// <summary>
        /// Gets or sets the ordnance settings for this multiplayer variant.
        /// </summary>
        public OrdnanceSettings Ordnance
        {
            get { return this.ordnance; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.ordnance = value;
            }
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.unk0);
            s.Stream(ref this.unk1);

            s.Serialize(this.general);
            s.Serialize(this.prototype);
            s.Serialize(this.respawn);
            s.Serialize(this.social);
            s.Serialize(this.mapOverrides);
            s.Serialize(this.requisition);
            s.Serialize(this.teams);
            s.Serialize(this.loadouts);
            s.Serialize(this.ordnance);
        }

        #endregion
    }
}
