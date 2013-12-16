using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class SphericalBoundaryData
		: BoundaryData
	{
		private IntegerReference _radius;

		public SphericalBoundaryData ()
			: base(Shape.Sphere)
		{
			_radius = new IntegerReference();
		}

		public SphericalBoundaryData(IntegerReference radius)
			: this()
		{
			_radius = radius;
		}

		public IntegerReference Radius
		{
			get { return _radius; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_radius = value;
			}
		}

		internal override void SerializeData (BitStream s, ParameterDefinition definition)
		{
			( _radius as IParameter ).SerializeObject(s, definition);
		}
	}
}