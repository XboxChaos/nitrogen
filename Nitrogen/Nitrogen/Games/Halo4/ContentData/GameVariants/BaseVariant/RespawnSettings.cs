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

using Nitrogen.ContentData.Traits;
using Nitrogen.Games.Halo4.ContentData.Traits;
using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.BaseVariant
{
    /// <summary>
    /// Represents a set of respawn settings in a Halo 4 multiplayer variant.
    /// </summary>
    public class RespawnSettings
        : ISerializable<BitStream>
    {
        private bool
            syncWithTeam,
            respawnAtTeammate, // doesn't seem to work in Halo 4
            respawnInPlace, // doesn't seem to work in Halo 4
            respawnOnKill,
            dualRespawnTiming;

        private byte
            lives,
            sharedTeamLives,
            respawnTime,
            secondaryRespawnTime,
            suicdePenalty,
            betrayalPenalty,
            respawnTimeGrowth,
            initialLoadoutDuration; // broken in Halo 4; the lowest you can go is 7.

        private RespawnTraits<Halo4PlayerTraits> traits;

        /// <summary>
        /// Initializes a new instance of the <see cref="RespawnSettings"/> class with default values.
        /// </summary>
        public RespawnSettings()
        {
            this.traits = new RespawnTraits<Halo4PlayerTraits>();
            this.betrayalPenalty = 5;
            this.dualRespawnTiming = true;
            this.respawnTime = 3;
            this.secondaryRespawnTime = 7;
            this.suicdePenalty = 5;
        }

        /// <summary>
        /// Gets or sets the penalty added to the minimum respawn time when a player commits suicide.
        /// </summary>
        public byte SuicidePenalty
        {
            get { return this.suicdePenalty; }
            set { this.suicdePenalty = value; }
        }

        /// <summary>
        /// Gets or sets the penalty added to the minimum respawn time whenever a player betrays a
        /// teammate.
        /// </summary>
        public byte BetrayalPenalty
        {
            get { return this.betrayalPenalty; }
            set { this.betrayalPenalty = value; }
        }

        /// <summary>
        /// Gets or sets the amount of lives each player starts with. Set to 0 for unlimited lives.
        /// The value must fall in the range between 0 and 63 or an exception will be thrown.
        /// </summary>
        public byte Lives
        {
            get { return this.lives; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= 63);
                this.lives = value;
            }
        }

        /// <summary>
        /// Gets or sets the amount of lives which is shared among players in each team. Set to 0
        /// for unlimited lives. The value must fall in the range between 0 and 127 or an exception
        /// will be thrown.
        /// </summary>
        public byte SharedTeamLives
        {
            get { return this.sharedTeamLives; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= 127);
                this.sharedTeamLives = value;
            }
        }

        /// <summary>
        /// Gets or sets whether players respawn at the same time as their teammates.
        /// </summary>
        public bool SyncWithTeam
        {
            get { return this.syncWithTeam; }
            set { this.syncWithTeam = value; }
        }

        /// <summary>
        /// Gets or sets whether players respawn when their teammate earns a kill.
        /// </summary>
        public bool RespawnOnKill
        {
            get { return this.respawnOnKill; }
            set { this.respawnOnKill = value; }
        }

        /// <summary>
        /// Gets or sets the initial respawn duration.
        /// </summary>
        public byte InitialRespawnDuration
        {
            get { return this.secondaryRespawnTime; }
            set { this.secondaryRespawnTime = value; }
        }

        /// <summary>
        /// Gets or sets the minimum respawn duration (which may not be skipped).
        /// </summary>
        public byte MinimumRespawnDuration
        {
            get { return this.respawnTime; }
            set
            {
                this.respawnTime = value;
                this.dualRespawnTiming = (value != 0);
            }
        }

        /// <summary>
        /// Gets or sets the amount added to the minimum respawn duration every time a player dies.
        /// The value must fall in the range between 0 and 15 or an exception will be thrown.
        /// </summary>
        public byte RespawnTimeGrowth
        {
            get { return this.respawnTimeGrowth; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= 15);
                this.respawnTimeGrowth = value;
            }
        }

        #region ISerializable<BitStream>

        public void Serialize(BitStream s)
        {
            // Respawn time must be less than secondary respawn time.
            if (this.respawnTime > this.secondaryRespawnTime)
            {
                this.respawnTime = this.secondaryRespawnTime;
            }

            s.Stream(ref this.syncWithTeam);
            s.Stream(ref this.respawnAtTeammate);
            s.Stream(ref this.respawnInPlace);
            s.Stream(ref this.respawnOnKill);
            s.Stream(ref this.dualRespawnTiming);
            s.Stream(ref this.lives, 6);
            s.Stream(ref this.sharedTeamLives, 7);
            s.Stream(ref this.respawnTime);
            s.Stream(ref this.secondaryRespawnTime);
            s.Stream(ref this.suicdePenalty);
            s.Stream(ref this.betrayalPenalty);
            s.Stream(ref this.respawnTimeGrowth, 4);
            s.Stream(ref this.initialLoadoutDuration, 4);
            s.Serialize(this.traits);
        }

        #endregion
    }
}