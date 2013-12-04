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

using Nitrogen.ContentData.GameVariants.Megalo;
using Nitrogen.Games.Halo4.ContentData.GameVariants.Megalo.ParameterTypes;
using System;
using System.Collections.Generic;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.Megalo
{
    public class Parameters
    {
        // FOOD FOR THOUGHT: IS THIS SHIT EVEN NECESSARY??

        private static Dictionary<ParameterType, IParameter> Definitions =
            new Dictionary<ParameterType, IParameter>
            {
                { ParameterType.IntegerReference, new IntegerReference() },
                { ParameterType.TimerReference, new TimerReference() },
                { ParameterType.TeamReference, new TeamReference() },
                { ParameterType.StringReference, new StringReference() },
                { ParameterType.StringReferenceSingleToken, new StringReferenceSingleToken() },
                { ParameterType.StringReferenceMultiToken, new StringReferenceMultiToken() },
                { ParameterType.PlayerReference, new PlayerReference() },
                { ParameterType.ObjectReference, new ObjectReference() },
                { ParameterType.TargetReference, new TargetReference() },

                { ParameterType.Shape, new ShapeData() },
                { ParameterType.Meter, new MeterData() },

                /*
                 * GenericReference
                 * TargetReference
                 * VirtualTrigger
                 * EntityFilter
                 * WaypointIcon
                 */
            };
    }
}
