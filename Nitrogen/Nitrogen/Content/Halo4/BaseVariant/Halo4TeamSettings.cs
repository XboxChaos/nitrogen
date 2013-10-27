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

using Nitrogen.Content.Halo4.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.Content.Halo4.BaseVariant
{
    [Synchronizable]
    public class Halo4TeamSettings
    {
        private List<Halo4Team> teams;

        /// <summary>
        /// Initialize a new instance of the <see cref="Halo4TeamSettings"/> class with default
        /// values.
        /// </summary>
        public Halo4TeamSettings()
        {
            var defaultTeams = new List<Halo4Team>();
            for (int i = 0; i < 8; i++)
            {
                defaultTeams.Add(Halo4Team.CreateDefault(i));
            }
            this.teams = defaultTeams;

            DesignatorSwitchType = 0;
            TeamModelOverride = 0;
        }

        [PropertyBinding("Teams", Count = 5, Prefix = "Team")]
        public List<Halo4Team> Teams
        {
            get { return this.teams; }
            set
            {
                Contract.Requires<ArgumentException>(value.Count != 8);
                this.teams = value;
            }
        }

        public Halo4Team RedTeam
        {
            get { return this.teams[0]; }
            set { this.teams[0] = value; }
        }

        public Halo4Team BlueTeam
        {
            get { return this.teams[1]; }
            set { this.teams[1] = value; }
        }

        public Halo4Team GoldTeam
        {
            get { return this.teams[2]; }
            set { this.teams[2] = value; }
        }

        public Halo4Team GreenTeam
        {
            get { return this.teams[3]; }
            set { this.teams[3] = value; }
        }

        public Halo4Team PurpleTeam
        {
            get { return this.teams[4]; }
            set { this.teams[4] = value; }
        }

        public Halo4Team LimeTeam
        {
            get { return this.teams[5]; }
            set { this.teams[5] = value; }
        }

        public Halo4Team OrangeTeam
        {
            get { return this.teams[6]; }
            set { this.teams[6] = value; }
        }

        public Halo4Team CyanTeam
        {
            get { return this.teams[7]; }
            set { this.teams[7] = value; }
        }

        [PropertyBinding("TeamModelOverride")]
        private byte TeamModelOverride { get; set; }

        [PropertyBinding("DesignatorSwitchType")]
        private byte DesignatorSwitchType { get; set; }
    }
}
