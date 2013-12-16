using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public abstract class BoundaryData
		: IParameter
	{
		private byte _shape;

		protected BoundaryData (Shape shape)
		{
			_shape = (byte) shape;
		}

		protected enum Shape
		{
			None,
			Sphere,
			Cylinder,
			Box
		}

		internal abstract void SerializeData (BitStream s, ParameterDefinition definition);

		#region IParameter Members

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			s.Stream(ref _shape, 2);
			SerializeData(s, definition);
		}

		#endregion
	}
}
