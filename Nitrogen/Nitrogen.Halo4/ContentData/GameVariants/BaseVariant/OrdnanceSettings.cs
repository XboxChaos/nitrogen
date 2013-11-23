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
using System;

namespace Nitrogen.Halo4.ContentData.GameVariants.BaseVariant
{
    public class OrdnanceSettings
        : ISerializable<BitStream>
    {


        #region ISerializable<BitStream>

        public void Serialize(BitStream stream)
        {
            /*Register<bool>("InitialOrdnanceEnabled");
            Register<bool>("RandomOrdnanceEnabled");
            Register<bool>("ObjectiveOrdnanceEnabled");
            Register<bool>("PersonalOrdnanceEnabled");
            Register<bool>("OrdnanceSystemEnabled");
            Register<sbyte>(); // timer or distance? -2 in forge like the other unknown int16's. -1 disables initial ordnance
            Register<short>("RandomOrdnanceMinTimer");
            Register<short>("RandomOrdnanceMaxTimer");
            Register<short>(); // timer
            Register<string>("InitialDropObjectFilterName", encoding: Encoding.ASCII, length: 32, padded: false, nullTerminated: true);
            Register<short>(); // timer
            Register<short>("InitialDropDelay");
            Register<string>("RandomDropSet", encoding: Encoding.ASCII, length: 32, padded: false, nullTerminated: true);
            Register<string>("PersonalDropSet", encoding: Encoding.ASCII, length: 32, padded: false, nullTerminated: true);
            Register<string>("SubstitutionSet", encoding: Encoding.ASCII, length: 32, padded: false, nullTerminated: true);

            Group("Personal", () =>
            {
                Register<bool>("IsCustomized");

                for (int i = 0; i < 4; i++)
                {
                    string slotName = i.ToString();
                    switch (i)
                    {
                        case 0: slotName = "Top"; break;
                        case 1: slotName = "Left"; break;
                        case 2: slotName = "Middle"; break;
                        case 3: slotName = "Right"; break;
                    }

                    Group(slotName, () =>
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            Group("Item[" + j + "]", () =>
                            {
                                Register<string>("Item", encoding: Encoding.ASCII, length: 32, padded: false, nullTerminated: true);
                                Register<float>("Weight", n: 30, minValue: 0, maxValue: 10000, isSigned: false, flag: true);
                            });
                        }
                    });
                }

                Register<float>("PointRequirement", n: 30, minValue: 0, maxValue: 10000, isSigned: false, flag: true);
                Register<float>("PointIncreaseMultiplier", n: 30, minValue: 0, maxValue: 10000, isSigned: false, flag: true);
            });*/
        }

        #endregion
    }
}