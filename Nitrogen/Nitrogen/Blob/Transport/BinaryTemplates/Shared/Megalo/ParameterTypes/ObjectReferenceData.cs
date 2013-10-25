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
    /// Defines the structure of an object reference.
    /// </summary>
    internal class ObjectReferenceData
        : DataTemplate
    {
        protected override void Define()
        {
            Group("ObjectReference", () =>
            {
                var type = Register<byte>("Type", n: 3);
                switch (type)
                {
                    case 0:
                        Group("Global", () => Register<byte>("Id", n: 5));
                        break;

                    case 1:
                        Group("PlayerMember", () =>
                        {
                            Register<byte>("Owner", n: 6);
                            Register<byte>("Index", n: 2);
                        });
                        break;

                    case 2:
                        Group("ObjectMember", () =>
                        {
                            Register<byte>("Owner", n: 5);
                            Register<byte>("Index", n: 3);
                        });
                        break;

                    case 3:
                        Group("TeamMember", () =>
                        {
                            Register<byte>("Owner", n: 5);
                            Register<byte>("Index", n: 3);
                        });
                        break;

                    case 4:
                        Group("GlobalPlayerBiped", () => Register<byte>("Id", n: 6));
                        break;

                    case 5:
                        Group("PlayerMemberBiped", () =>
                        {
                            Register<byte>("Owner", n: 6);
                            Register<byte>("Index", n: 2);
                        });
                        break;

                    case 6:
                        Group("ObjectMemberBiped", () =>
                        {
                            Register<byte>("Owner", n: 5);
                            Register<byte>("Index", n: 2);
                        });
                        break;

                    case 7:
                        Group("TeamMemberBiped", () =>
                        {
                            Register<byte>("Owner", n: 5);
                            Register<byte>("Index", n: 2);
                        });
                        break;
                }
            });
        }
    }
}
