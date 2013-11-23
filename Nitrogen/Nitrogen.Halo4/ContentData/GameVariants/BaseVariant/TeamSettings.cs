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
    public class TeamSettings
        : ISerializable<BitStream>
    {


        #region ISerializable<BitStream>

        public void Serialize(BitStream stream)
        {
            /*Register<byte>("TeamModelOverride", n: 3);
            Register<byte>("DesignatorSwitchType", n: 2);

            for (int i = 0; i < 8; i++)
            {
                Group("Team[" + i + "]", () =>
                {
                    Register<bool>("OverrideEmblem");
                    Register<bool>("OverrideUIColor");
                    Register<bool>("OverrideTextColor");
                    Register<bool>("OverridePrimaryColor");
                    Register<bool>("OverrideSecondaryColor");
                    Register<bool>("IsEnabled");

                    Group("Name", () => Import<StringTable>(new Context
                    {
                        { "OffsetSize", 10 },
                        { "LengthSize", 10 },
                        { "CountSize", 1 },
                    }));

                    Register<uint>("Index", n: 4);
                    Register<bool>("OverrideTeamModel");

                    Group("PrimaryColor", () =>
                    {
                        Register<byte>("Alpha");
                        Register<byte>("Red");
                        Register<byte>("Green");
                        Register<byte>("Blue");
                    });

                    Group("SecondaryColor", () =>
                    {
                        Register<byte>("Alpha");
                        Register<byte>("Red");
                        Register<byte>("Green");
                        Register<byte>("Blue");
                    });

                    Group("TextColor", () =>
                    {
                        Register<byte>("Alpha");
                        Register<byte>("Red");
                        Register<byte>("Green");
                        Register<byte>("Blue");
                    });

                    Group("UIColor", () =>
                    {
                        Register<byte>("Alpha");
                        Register<byte>("Red");
                        Register<byte>("Green");
                        Register<byte>("Blue");
                    });

                    Register<uint>("FireTeamCount", n: 5);

                    Group("Emblem", () =>
                    {
                        Register<byte>("ForegroundIndex");
                        Register<byte>("BackgroundIndex");
                        Register<bool>();
                        Register<bool>("BackgroundToggle");
                        Register<bool>("ForegroundToggle");
                        Register<uint>("PrimaryColorIndex", n: 6);
                        Register<uint>("SecondaryColorIndex", n: 6);
                        Register<uint>("BackgroundColorIndex", n: 6);
                    });
                });
            }*/
        }

        #endregion
    }
}