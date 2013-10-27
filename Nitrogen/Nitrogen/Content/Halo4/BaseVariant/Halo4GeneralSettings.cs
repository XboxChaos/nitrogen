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

using System;

namespace Nitrogen.Content.Halo4.BaseVariant
{
    [Synchronizable]
    public class Halo4GeneralSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4GeneralSettings"/> class with default values.
        /// </summary>
        public Halo4GeneralSettings()
        {
            TeamsEnabled = true;
            RoundsResetPlayers = false;
            RoundsResetMap = false;
            RoundsResetEverythingElse = false;
            RoundTimeLimit = 0;
            NumberOfRounds = 1;
            EarlyVictoryWinCount = 0;
            KillCamEnabled = true;
            PointsSystemEnabled = true;
            FinalKillCamEnabled = false;
            SuddenDeathDuration = 90;

            Unknown0 = false;
            Unknown1 = false;
            Unknown2 = false;
            MoshDifficulty = 0;
        }

        [PropertyBinding("TeamsEnabled")]
        public bool TeamsEnabled { get; set; }

        [PropertyBinding("RoundsResetPlayers")]
        public bool RoundsResetPlayers { get; set; }

        [PropertyBinding("RoundsResetMap")]
        public bool RoundsResetMap { get; set; }

        [PropertyBinding("RoundsResetEverythingElse")]
        public bool RoundsResetEverythingElse { get; set; }

        [PropertyBinding("RoundTimeLimit")]
        public byte RoundTimeLimit { get; set; }

        [PropertyBinding("NumberOfRounds")]
        public byte NumberOfRounds { get; set; }

        [PropertyBinding("EarlyVictoryWinCount")]
        public byte EarlyVictoryWinCount { get; set; }

        [PropertyBinding("DeathCamEnabled")]
        public bool KillCamEnabled { get; set; }

        [PropertyBinding("PointsSystemEnabled")]
        public bool PointsSystemEnabled { get; set; }

        [PropertyBinding("FinalKillCamEnabled")]
        public bool FinalKillCamEnabled { get; set; }

        [PropertyBinding("SuddenDeathDuration")]
        public byte SuddenDeathDuration { get; set; }

        [PropertyBinding("__unnamed_0")]
        private bool Unknown0 { get; set; }

        [PropertyBinding("__unnamed_1")]
        private bool Unknown1 { get; set; }

        [PropertyBinding("__unnamed_2")]
        private bool Unknown2 { get; set; }

        [PropertyBinding("MoshDifficulty")]
        private byte MoshDifficulty { get; set; }
    }
}
