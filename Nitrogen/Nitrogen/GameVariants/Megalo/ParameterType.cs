namespace Nitrogen.GameVariants.Megalo
{
	internal enum ParameterType
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
		Meter,
		EntityFilter,
		WaypointIcon,
	}
}
