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
using Nitrogen.Megalo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Nitrogen.Blob.Transport.BinaryTemplates.Shared.Megalo
{
    /// <summary>
    /// Defines the structure of parameters of a Megalo action or condition in Halo: Reach and Halo 4.
    /// </summary>
    internal class DefinitionParameters
        : DataTemplate
    {
        private static Dictionary<ParameterType, DataTemplate> ParameterTemplates = new Dictionary<ParameterType, DataTemplate>
        {
            // Reference Types
            { ParameterType.GenericReference, new GenericReferenceData() },
            { ParameterType.PlayerReference, new PlayerReferenceData() },
            { ParameterType.ObjectReference, new ObjectReferenceData() },
            { ParameterType.TeamReference, new TeamReferenceData() },
            { ParameterType.IntegerReference, new IntegerReferenceData() },
            { ParameterType.TimerReference, new TimerReferenceData() },
            { ParameterType.TargetReference, new TargetReferenceData() },
            { ParameterType.StringReference, new StringReferenceData(0) },
            { ParameterType.StringReferenceSingleToken, new StringReferenceData(1) },
            { ParameterType.StringReferenceMultiToken, new StringReferenceData(2) },
                    
            // Structs
            { ParameterType.VirtualTrigger, new VirtualTriggerData() },
            { ParameterType.Shape, new ShapeData() },
            { ParameterType.MeterInfo, new MeterInfoData() },
            { ParameterType.EntityFilter, new EntityFilterData() },
            { ParameterType.WaypointIcon, new WaypointIconData() },
        };

        private Dictionary<string, int> unnamedTypes = new Dictionary<string, int>();

        protected override void Define()
        {
            Debug.Fail("This method should never be called.");
        }

        protected override void Define(Context namedArgs)
        {
            var definition = namedArgs["Definition"] as MegaloDefinition;
            definition.Parameters.ForEach((ParameterDefinition parameter) =>
            {
                // Come up with a parameter name if one is not given.
                string paramName = parameter.Name;
                if (paramName == null)
                {
                    switch (parameter.Type)
                    {
                        case ParameterType.EntityFilter: { paramName = "filter"; } break;
                        case ParameterType.Float: { paramName = "float"; } break;
                        case ParameterType.GenericReference: { paramName = "value"; } break;
                        case ParameterType.Integer: { paramName = "value"; } break;
                        case ParameterType.IntegerReference: { paramName = "value"; } break;
                        case ParameterType.MeterInfo: { paramName = "meter"; } break;
                        case ParameterType.WaypointIcon: { paramName = "icon"; } break;
                        case ParameterType.ObjectReference: { paramName = "object"; } break;
                        case ParameterType.PlayerReference: { paramName = "player"; } break;
                        case ParameterType.Shape: { paramName = "boundary"; } break;
                        case ParameterType.TargetReference: { paramName = "target"; } break;
                        case ParameterType.TeamReference: { paramName = "team"; } break;
                        case ParameterType.TimerReference: { paramName = "timer"; } break;
                        case ParameterType.VirtualTrigger: { paramName = "trigger_data"; } break;

                        case ParameterType.StringReference:
                        case ParameterType.StringReferenceSingleToken:
                        case ParameterType.StringReferenceMultiToken:
                                paramName = "text";
                            break;

                        default: { paramName = "param"; } break;
                    }

                    if (unnamedTypes.ContainsKey(paramName))
                    {
                        paramName += unnamedTypes[paramName]++;
                    }
                    else
                    {
                        unnamedTypes.Add(paramName, 2);
                    }
                }

                // Define parameter.
                Group(paramName, () =>
                {
                    switch (parameter.Type)
                    {
                        case ParameterType.Integer:
                            DefineInteger(parameter, "Integer");
                            break;

                        case ParameterType.Float:
                            DefineFloat(parameter, "Float");
                            break;

                        default:
                            Import(ParameterTemplates[parameter.Type]);
                            break;
                    }
                });
            });
        }

        private void DefineInteger(ParameterDefinition parameter, string paramName)
        {
            // Has 'Is Null' bit?
            if (parameter.Nullable)
            {
                if (Mode == SerializationMode.Deserialize)
                {
                    if ((bool)Read(typeof(bool))) // is null?
                    {
                        SetValue(paramName, null);
                        return;
                    }
                }
                else
                {
                    bool isNull = (GetValue<object>(paramName) == null);
                    Write(isNull);

                    if (isNull)
                        return;
                }
            }

            // Get value.
            ulong value;
            if (Mode == SerializationMode.Deserialize)
                value = Convert.ToUInt64(Read(parameter.Unsigned ? typeof(uint) : typeof(int), n: parameter.BitSize));
            else
                value = GetValue<ulong>(paramName);

            // Uses +1 encoding?
            if (parameter.PlusOneEncoding)
            {
                if (Mode == SerializationMode.Deserialize)
                    value--;
                else if (Mode == SerializationMode.Serialize)
                    value++;
            }

            // Read or write modified value.
            if (Mode == SerializationMode.Deserialize)
                SetValue(paramName, Convert.ChangeType(value, parameter.Unsigned ? typeof(ulong) : typeof(long)));
            else
                Write(value, n: parameter.BitSize);
        }

        private void DefineFloat(ParameterDefinition parameter, string paramName)
        {
            // To be implemented
        }
    }
}
