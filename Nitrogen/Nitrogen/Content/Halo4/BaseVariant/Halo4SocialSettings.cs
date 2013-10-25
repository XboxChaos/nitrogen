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

using Nitrogen.Content.Shared.Enums;
using System;

namespace Nitrogen.Content.Halo4.BaseVariant
{
    [Synchronizable]
    public class Halo4SocialSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4SocialSettings"/> class with default
        /// values.
        /// </summary>
        public Halo4SocialSettings()
        {
            TeamChanging = TeamChangingMode.Enabled;
            FriendlyFire = true;
            BetrayalBooting = true;
            EnemyPlayerVoice = true;
            OpenChannelVoice = true;
            DeadPlayerVoice = true;
        }

        [PropertyBinding("TeamChangingMode")]
        public TeamChangingMode TeamChanging { get; set; }

        [PropertyBinding("FriendlyFireEnabled")]
        public bool FriendlyFire { get; set; }

        [PropertyBinding("BetrayalBootingEnabled")]
        public bool BetrayalBooting { get; set; }

        [PropertyBinding("EnemyVoice")]
        public bool EnemyPlayerVoice { get; set; }

        [PropertyBinding("OpenChannelVoice")]
        public bool OpenChannelVoice { get; set; }

        [PropertyBinding("DeadPlayerVoice")]
        public bool DeadPlayerVoice { get; set; }
    }
}
