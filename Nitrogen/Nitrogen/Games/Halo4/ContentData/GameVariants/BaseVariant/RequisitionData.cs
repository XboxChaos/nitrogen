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

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.BaseVariant
{
    /// <summary>
    /// Represents a set of requisition-related data in an instance of the
    /// <see cref="RequisitionSettings"/> class.
    /// </summary>
    public class RequisitionData
        : ISerializable<BitStream>
    {
        private byte paletteIndex;
        private bool unk0;
        private int unk1;
        private int maxInstances;
        private float unk2;
        private int modelVariantStringIndex;
        private int initialAmmo;
        private float unk3, unk4, unk5, unk6;
        private byte maxBuyPlayer, maxBuyTeam;

        #region ISerializable<BitStream>

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.paletteIndex);
            s.Stream(ref this.unk0);
            s.Stream(ref this.unk1);
            s.Stream(ref this.maxInstances, 30);
            s.Stream(ref this.unk2);
            s.Stream(ref this.modelVariantStringIndex, 30);
            s.Stream(ref this.initialAmmo);
            s.Stream(ref this.unk3);
            s.Stream(ref this.unk4);
            s.Stream(ref this.unk5);
            s.Stream(ref this.unk6);
            s.Stream(ref this.maxBuyPlayer);
            s.Stream(ref this.maxBuyTeam);
        }

        #endregion
    }
}
