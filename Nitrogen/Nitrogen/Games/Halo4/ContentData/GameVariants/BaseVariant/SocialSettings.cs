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
using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.BaseVariant
{
    /// <summary>
    /// Indicates whether players are able to change teams during a match.
    /// </summary>
    public enum TeamChangingMode
        : byte
    {
        /// <summary>
        /// Players may not switch teams during the match.
        /// </summary>
        Disabled,

        /// <summary>
        /// Players may switch teams during the match.
        /// </summary>
        Enabled,

        /// <summary>
        /// Players may switch teams only to balance out teams.
        /// </summary>
        TeamBalancing,
    }

    /// <summary>
    /// Represents a set of social settings in a Halo 4 multiplayer variant.
    /// </summary>
    public class SocialSettings
        : ISerializable<BitStream>
    {
        private bool
            observerTeam,
            friendlyFire,
            betrayalBooting,
            enemyVoice,
            openChannelVoice,
            deadPlayerVoice;

        private byte teamChangingMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialSettings"/> class with default values.
        /// </summary>
        public SocialSettings()
        {
            this.enemyVoice = true;
            this.openChannelVoice = true;
            this.deadPlayerVoice = true;
            this.friendlyFire = true;
        }

        /// <summary>
        /// Gets or sets whether players can damage their teammates.
        /// </summary>
        public bool FriendlyFireEnabled
        {
            get { return this.friendlyFire; }
            set { this.friendlyFire = value; }
        }

        /// <summary>
        /// Gets or sets whether players can boot other players.
        /// </summary>
        public bool BetrayalBooting
        {
            get { return this.betrayalBooting; }
            set { this.betrayalBooting = value; }
        }

        /// <summary>
        /// Gets or sets whether players can hear enemy players within close proximity.
        /// </summary>
        public bool EnemyVoice
        {
            get { return this.enemyVoice; }
            set { this.enemyVoice = value; }
        }

        /// <summary>
        /// Gets or sets whether the voice channel is open to all players in the match.
        /// </summary>
        public bool OpenChannelVoice
        {
            get { return this.openChannelVoice; }
            set { this.openChannelVoice = value; }
        }

        /// <summary>
        /// Gets or sets whether dead players and their killer can hear each other.
        /// </summary>
        public bool DeadPlayerVoice
        {
            get { return this.deadPlayerVoice; }
            set { this.deadPlayerVoice = value; }
        }

        /// <summary>
        /// Gets or sets whethers players are able to switch teams during the match.
        /// </summary>
        public TeamChangingMode TeamChanging
        {
            get { return (TeamChangingMode)this.teamChangingMode; }
            set
            {
                Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(TeamChangingMode), value));
                this.teamChangingMode = (byte)value;
            }
        }

        #region ISerializable<BitStream>

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.observerTeam);
            s.Stream(ref this.teamChangingMode, 2);
            s.Stream(ref this.friendlyFire);
            s.Stream(ref this.betrayalBooting);
            s.Stream(ref this.enemyVoice);
            s.Stream(ref this.openChannelVoice);
            s.Stream(ref this.deadPlayerVoice);
        }

        #endregion
    }
}