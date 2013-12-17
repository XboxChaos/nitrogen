namespace Nitrogen.GameVariants.Megalo
{
	public enum ParameterType
	{
		None,

		// Primitive types
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

		// Enums
		ComparisonType, // 3 bits, unsigned
		PlayerKillerTypeFlags, // 5 bits, unsigned
		TeamDisposition, // 2 bits, unsigned
		MultiplayerObjectType, // nullable, 11 bits, unsigned
		ObjectFilter, // 4 bits, unsigned, nullable
		Incident, // 32 bits, unsigned
	}
}
