using Nitrogen.ContentData.GameVariants.Megalo;
using Nitrogen.Games.Halo4.ContentData.GameVariants.Megalo.ParameterTypes;
using System;
using System.Collections.Generic;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.Megalo
{
    public class Parameters
    {
        private static Dictionary<ParameterType, IScriptObject> Definitions =
            new Dictionary<ParameterType, IScriptObject>
            {
                { ParameterType.IntegerReference, new IntegerReference() },

                /*
                 * GenericReference
                 * PlayerReference
                 * ObjectReference
                 * TeamReference
                 * TimerReference
                 * TargetReference
                 * StringReference
                 * StringReferenceSingleToken
                 * StringReferenceMultiToken
                 * VirtualTrigger
                 * Shape
                 * MeterInfo
                 * EntityFilter
                 * WaypointIcon
                 */
            };
    }
}
