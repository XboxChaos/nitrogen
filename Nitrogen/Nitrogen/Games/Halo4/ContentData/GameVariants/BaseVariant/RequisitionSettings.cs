﻿/*
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
    public class RequisitionSettings
        : ISerializable<BitStream>
    {
        private float unk0;
        private int unk1;

        #region ISerializable<BitStream>

        public void Serialize(BitStream stream)
        {
            /*Register<float>();
            Register<int>();
            var count = Register<byte>("Count", n: 7);
            Group("Settings", () =>
            {
                for (int i = 0; i < count; i++)
                {
                    Group("Settings[" + i + "]", () =>
                    {
                        Register<byte>("PaletteIndex");
                        Register<bool>();
                        Register<int>();
                        Register<int>("MaxInstances", n: 30);
                        Register<float>();
                        Register<int>("ModelVariantNameStringIndex", n: 30);
                        Register<int>("InitialAmmo");
                        Register<float>();
                        Register<float>();
                        Register<float>();
                        Register<float>();
                        Register<byte>("MaxBuyPlayer");
                        Register<byte>("MaxBuyTeam");
                    });
                }
            });
            Register<int>();*/
        }

        #endregion
    }
}