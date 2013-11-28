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

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.BaseVariant
{
    public class TeamEmblem
        : ISerializable<BitStream>
    {
        private byte foreground, background;
        private bool unk0, backgroundToggle, foregroundToggle;
        private byte primaryColor, secondaryColor, bgColor;

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.foreground);
            s.Stream(ref this.background);
            s.Stream(ref this.unk0);
            s.Stream(ref this.backgroundToggle);
            s.Stream(ref this.foregroundToggle);
            s.Stream(ref this.primaryColor, 6);
            s.Stream(ref this.secondaryColor, 6);
            s.Stream(ref this.bgColor, 6);
        }

        #endregion
    }
}
