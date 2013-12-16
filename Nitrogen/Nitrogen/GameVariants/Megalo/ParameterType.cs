namespace Nitrogen.GameVariants.Megalo
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
		StringReferenceOneToken,
		StringReferenceTwoTokens,
		StringReferenceThreeTokens,

		// Structs
		VirtualTrigger,
		Shape,
		Meter,
		EntityFilter,
		WaypointIcon,
	}
}
