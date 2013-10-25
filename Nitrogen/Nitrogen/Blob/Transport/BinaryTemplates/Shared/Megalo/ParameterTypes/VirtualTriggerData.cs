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
    /// Defines the structure of a virtual trigger.
    /// </summary>
    internal class VirtualTriggerData
        : DataTemplate
    {
        protected override void Define()
        {
            Group("VirtualTrigger", () =>
            {
                // Conditions
                int condIndex = -1;
                if (Mode == SerializationMode.Deserialize)
                {
                    condIndex = Convert.ToInt32(Read(typeof(byte), n: 10)) - 1;
                    SetValue("ConditionIndex", condIndex);
                }
                else
                {
                    condIndex = GetValue<int>("ConditionIndex");
                    Write(condIndex + 1, n: 10);
                }
                Register<ushort>("ConditionCount", n: 10);

                // Actions
                int actionIndex = -1;
                if (Mode == SerializationMode.Deserialize)
                {
                    actionIndex = Convert.ToInt32(Read(typeof(byte), n: 11)) -1;
                    SetValue("ActionIndex", actionIndex);
                }
                else
                {
                    actionIndex = GetValue<int>("ActionIndex");
                    Write(actionIndex + 1, n: 11);
                }
                Register<ushort>("ActionCount", n: 11);
            });
        }
    }
}
