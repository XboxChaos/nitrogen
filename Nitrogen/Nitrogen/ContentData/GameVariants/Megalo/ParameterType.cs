namespace Nitrogen.ContentData.GameVariants.Megalo
{
    public enum ParameterType
    {
        None,

        // Primitive types
        Integer,
        Float,

        // Reference types
        GenericReference,
        PlayerReference,
        ObjectReference,
        TeamReference,
        IntegerReference,
        TimerReference,
        TargetReference,
        StringReference,
        StringReferenceSingleToken,
        StringReferenceMultiToken,

        // Structs
        VirtualTrigger,
        Shape,
        MeterInfo,
        EntityFilter,
        WaypointIcon,
    }
}
