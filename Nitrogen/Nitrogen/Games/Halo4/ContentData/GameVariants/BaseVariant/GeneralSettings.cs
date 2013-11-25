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
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.BaseVariant
{
    /// <summary>
    /// Represents a set of general settings in a Halo 4 multiplayer variant.
    /// </summary>
    public class GeneralSettings
        : ISerializable<BitStream>
    {
        private bool
            teamsEnabled,
            roundsResetPlayers,
            roundsResetMap,
            roundsResetEverythingElse,
            deathCamEnabled,
            pointsSystemEnabled,
            finalKillCamEnabled,
            unk0,
            unk1;

        private byte
            roundTimeLimit,
            numberOfRounds,
            earlyVictoryWinCount,
            suddenDeathDuration,
            moshDifficulty;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralSettings"/> class with default values.
        /// </summary>
        public GeneralSettings()
        {
            this.teamsEnabled = true;
            this.roundTimeLimit = 15;
            this.numberOfRounds = 1;
            this.deathCamEnabled = true;
            this.pointsSystemEnabled = true;
            this.suddenDeathDuration = 90;
        }

        /// <summary>
        /// Gets or sets the duration in seconds of each round.
        /// </summary>
        public byte RoundTimeLimit
        {
            get { return this.roundTimeLimit; }
            set { this.roundTimeLimit = value; }
        }

        /// <summary>
        /// Gets or sets the number of rounds per match.
        /// </summary>
        public byte NumberOfRounds
        {
            get { return this.numberOfRounds; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= 31);
                this.numberOfRounds = value;
            }
        }

        /// <summary>
        /// Gets or sets whether teams are enabled or not.
        /// </summary>
        public bool TeamsEnabled
        {
            get { return this.teamsEnabled; }
            set { this.teamsEnabled = value; }
        }

        /// <summary>
        /// Gets or sets whether the death cam (aka killcam) is enabled for all players.
        /// </summary>
        public bool DeathCamEnabled
        {
            get { return this.deathCamEnabled; }
            set { this.deathCamEnabled = value; }
        }

        /// <summary>
        /// Gets or sets whether the final kill cam is enabled for all players.
        /// </summary>
        public bool FinalKillCamEnabled
        {
            get { return this.finalKillCamEnabled; }
            set { this.finalKillCamEnabled = value; }
        }

        /// <summary>
        /// Gets or sets whether the Halo 4 style points system is enabled.
        /// </summary>
        public bool PointsSystemEnabled
        {
            get { return this.pointsSystemEnabled; }
            set { this.pointsSystemEnabled = value; }
        }

        /// <summary>
        /// Gets or sets the number of wins a player or team can reach for an early victory (e.g. best 2 out of 3).
        /// </summary>
        public byte EarlyVictoryWinCount
        {
            get { return this.earlyVictoryWinCount; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= 15);
                this.earlyVictoryWinCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the duration of the overtime period.
        /// </summary>
        public byte OvertimeDuration
        {
            get { return this.suddenDeathDuration; }
            set { this.suddenDeathDuration = value; }
        }

        #region ISerializable<BitStream>

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.teamsEnabled);
            s.Stream(ref this.roundsResetPlayers);
            s.Stream(ref this.roundsResetMap);
            s.Stream(ref this.roundsResetEverythingElse);
            s.Stream(ref this.roundTimeLimit);
            s.Stream(ref this.numberOfRounds, 5);
            s.Stream(ref this.earlyVictoryWinCount, 4);
            s.Stream(ref this.deathCamEnabled);
            s.Stream(ref this.pointsSystemEnabled);
            s.Stream(ref this.finalKillCamEnabled);
            s.Stream(ref this.suddenDeathDuration);
            s.Stream(ref this.unk0);
            s.Stream(ref this.unk1);
            s.Stream(ref this.moshDifficulty, 2);
        }

        #endregion
    }
}
