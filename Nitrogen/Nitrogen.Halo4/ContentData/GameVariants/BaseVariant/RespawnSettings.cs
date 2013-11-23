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
using Nitrogen.Halo4.ContentData.Traits;
using System;

namespace Nitrogen.Halo4.ContentData.GameVariants.BaseVariant
{
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

        #region ISerializable<BitStream>

        public void Serialize(BitStream s)
        {
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