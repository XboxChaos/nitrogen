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

using Nitrogen.Blob.Transport.BinaryTemplates.Shared.Megalo.ParameterTypes;
using System;
using System.Diagnostics;

namespace Nitrogen.Blob.Transport.BinaryTemplates.Shared.Megalo
{
    /// <summary>
    /// Defines the structure of a Megalo variable table in Halo: Reach and Halo 4.
    /// </summary>
    internal class VariableTableData
        : DataTemplate
    {
        protected override void Define(Context namedArgs)
        {
            int integerCountSize = Convert.ToInt32(namedArgs["IntegerCountSize"]);
            int timerCountSize = Convert.ToInt32(namedArgs["TimerCountSize"]);
            int teamCountSize = Convert.ToInt32(namedArgs["TeamCountSize"]);
            int playerCountSize = Convert.ToInt32(namedArgs["PlayerCountSize"]);
            int objectCountSize = Convert.ToInt32(namedArgs["ObjectCountSize"]);

            Group("Integers", () =>
            {
                var count = Register<uint>("Count", n: integerCountSize);
                for (var i = 0; i < count; i++)
                {
                    Group("Integer[" + i + "]", () =>
                    {
                        Group("Value", () => Import<IntegerReferenceData>());
                        Register<byte>("NetworkPriority", n: 2);
                        Register<bool>();
                    });
                }
            });

            Group("Timers", () =>
            {
                var count = Register<uint>("Count", n: timerCountSize);
                for (var i = 0; i < count; i++)
                {
                    Group("Timer[" + i + "]", () =>
                    {
                        Group("Value", () => Import<IntegerReferenceData>());
                    });
                }
            });

            Group("Teams", () =>
            {
                var count = Register<uint>("Count", n: teamCountSize);
                for (var i = 0; i < count; i++)
                {
                    Group("Team[" + i + "]", () =>
                    {
                        Register<byte>("Value", n: 4);
                        Register<byte>("NetworkPriority", n: 2);
                        Register<bool>();
                    });
                }
            });

            Group("Players", () =>
            {
                var count = Register<uint>("Count", n: playerCountSize);
                for (var i = 0; i < count; i++)
                {
                    Group("Player[" + i + "]", () =>
                    {
                        Register<byte>("NetworkPriority", n: 2);
                        Register<bool>();
                    });
                }
            });

            Group("Objects", () =>
            {
                var count = Register<uint>("Count", n: objectCountSize);
                for (var i = 0; i < count; i++)
                {
                    Group("Object[" + i + "]", () =>
                    {
                        Register<byte>("NetworkPriority", n: 2);
                        Register<bool>();
                    });
                }
            });
        }

        protected override void Define()
        {
            Debug.Fail("This method should never be called.");
        }
    }
}
