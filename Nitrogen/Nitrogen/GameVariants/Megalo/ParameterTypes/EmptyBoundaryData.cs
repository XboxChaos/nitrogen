using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class EmptyBoundaryData
		: BoundaryData
	{
		public EmptyBoundaryData () : base(Shape.None) { }

		internal override void SerializeData (BitStream s, ParameterDefinition definition) { }
	}
}