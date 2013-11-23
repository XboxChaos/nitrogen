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
using Nitrogen.Halo4.ContentData.GameVariants.BaseVariant;
using System;

namespace Nitrogen.Halo4.ContentData.GameVariants
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
