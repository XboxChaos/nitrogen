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

namespace Nitrogen.Blob.Transport.BinaryTemplates.Shared.Megalo.ParameterTypes
{
    /// <summary>
    /// Defines the structure of an integer reference.
    /// </summary>
    internal class IntegerReferenceData
        : DataTemplate
    {
        protected override void Define()
        {
            Group("IntegerReference", () =>
            {
                var type = Register<byte>("Type", n: 7);
                switch (type)
                {
                    case 0:
                        Group("Constant", () => Register<short>("Value"));
                        break;

                    case 1:
                        Group("PlayerMember", () =>
                        {
                            Register<byte>("Owner", n: 6);
                            Register<byte>("Index", n: 4);
                        });
                        break;

                    case 2:
                        Group("ObjectMember", () =>
                        {
                            Register<byte>("Owner", n: 5);
                            Register<byte>("Index", n: 4);
                        });
                        break;

                    case 3:
                        Group("TeamMember", () =>
                        {
                            Register<byte>("Owner", n: 5);
                            Register<byte>("Index", n: 4);
                        });
                        break;

                    case 4:
                        Group("GlobalVariable", () => Register<byte>("Index", n: 5));
                        break;

                    case 5:
                        Group("ScratchVariable", () => Register<byte>("Index", n: 4));
                        break;

                    case 6:
                        Group("GameTypeOption", () => Register<byte>("Index", n: 4));
                        break;

                    case 7:
                    case 8:
                    case 9:
                    case 31:
                    case 32:
                        Group("ObjectProperty", () => Register<byte>("Id", n: 5));
                        break;

                    case 10:
                    case 22:
                    case 28:
                    case 29:
                    case 30:
                        Group("TeamProperty", () => Register<byte>("Id", n: 5));
                        break;

                    case 11:
                    case 12:
                    case 13:
                    case 23:
                    case 24:
                    case 25:
                    case 26:
                    case 27:
                        Group("PlayerProperty", () => Register<byte>("Id", n: 6));
                        break;

                    case 14:
                        Group("PlayerStat", () =>
                        {
                            Register<byte>("Id", n: 6);
                            Register<byte>("StatIndex", n: 2);
                        });
                        break;

                    case 15:
                        Group("TeamStat", () =>
                        {
                            Register<byte>("Id", n: 5);
                            Register<byte>("StatIndex", n: 2);
                        });
                        break;
                }
            });
        }
    }
}
