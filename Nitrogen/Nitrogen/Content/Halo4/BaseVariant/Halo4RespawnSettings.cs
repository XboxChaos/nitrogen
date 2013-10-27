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
using Nitrogen.Content.Shared;
using System;

namespace Nitrogen.Content.Halo4.BaseVariant
{
    [Synchronizable]
    public class Halo4RespawnSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4RespawnSettings"/> class with default values.
        /// </summary>
        public Halo4RespawnSettings()
        {
            SyncWithTeam = false;
            RespawnOnKill = false;
            DualRespawnTiming = false;
            Lives = 0;
            TeamLives = 0;
            MinimumRespawnTime = 3;
            SecondaryRespawnTime = 7;
            SuicidePenalty = 5;
            BetrayalPenalty = 5;
            RespawnTimeGrowth = 0;
            RespawnAtTeammate = false;
            RespawnInPlace = false;
            InitialLoadoutDuration = 0;
        }

        [PropertyBinding("SyncWithTeam")]
        public bool SyncWithTeam { get; set; }

        [PropertyBinding("RespawnOnKill")]
        public bool RespawnOnKill { get; set; }

        [PropertyBinding("DualRespawnTiming")]
        public bool DualRespawnTiming { get; set; }

        [PropertyBinding("Lives")]
        public byte Lives { get; set; }

        [PropertyBinding("SharedTeamLives")]
        public byte TeamLives { get; set; }

        [PropertyBinding("RespawnTime")]
        public byte MinimumRespawnTime { get; set; }

        [PropertyBinding("SecondaryRespawnTime")]
        public byte SecondaryRespawnTime { get; set; }

        [PropertyBinding("SuicidePenalty")]
        public byte SuicidePenalty { get; set; }

        [PropertyBinding("BetrayalPenalty")]
        public byte BetrayalPenalty { get; set; }

        [PropertyBinding("RespawnTimeGrowth")]
        public byte RespawnTimeGrowth { get; set; }

        [PropertyBinding("RespawnTraits")]
        public TraitSetWithDuration<Halo4PlayerTraitSet> RespawnTraits { get; set; }

        [PropertyBinding("RespawnAtTeammate")]
        private bool RespawnAtTeammate { get; set; }

        [PropertyBinding("RespawnInPlace")]
        private bool RespawnInPlace { get; set; }

        [PropertyBinding("InitialLoadoutDuration")]
        private byte InitialLoadoutDuration { get; set; }
    }
}
