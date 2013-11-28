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

using Nitrogen.ContentData.Localization;
using Nitrogen.IO;
using System;
using System.Drawing;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.BaseVariant
{
    public class TeamData
            : ISerializable<BitStream>
    {
        private bool
            overrideEmblem,
            overrideUI,
            overrideText,
            overridePrimary,
            overrideSecondary,
            isEnabled,
            overrideTeamModel;

        private StringTable teamName;
        private byte index;
        private Color primary, secondary, text, ui;
        private byte fireteamCount;
        private TeamEmblem emblem;

        public TeamData()
        {
            this.teamName = new StringTable(Halo4Properties.Languages);
            this.emblem = new TeamEmblem();
        }

        #region ISerializable<BitStream>

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.overrideEmblem);
            s.Stream(ref this.overrideUI);
            s.Stream(ref this.overrideText);
            s.Stream(ref this.overridePrimary);
            s.Stream(ref this.overrideSecondary);
            s.Stream(ref this.isEnabled);
            this.teamName.Serialize(s, 10, 10, 1);
            s.Stream(ref this.index, 4);
            s.Stream(ref this.overrideTeamModel);
            s.SerializeColor(ref this.primary);
            s.SerializeColor(ref this.secondary);
            s.SerializeColor(ref this.text);
            s.SerializeColor(ref this.ui);
            s.Stream(ref this.fireteamCount, 5);
            s.Serialize(this.emblem);
        }

        #endregion
    }
}
