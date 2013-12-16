using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class CylindricalBoundaryData
		: BoundaryData
	{
		private IntegerReference _top, _bottom, _radius;

		public CylindricalBoundaryData ()
			: base(Shape.Cylinder)
		{
			_top = new IntegerReference();
			_bottom = new IntegerReference();
			_radius = new IntegerReference();
		}

		public CylindricalBoundaryData (IntegerReference radius, IntegerReference top, IntegerReference bottom)
			: this()
		{
			_radius = radius;
			_top = top;
			_bottom = bottom;
		}

		public IntegerReference Top
		{
			get { return _top; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_top = value;
			}
		}

		public IntegerReference Bottom
		{
			get { return _bottom; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_bottom = value;
			}
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
			( _top as IParameter ).SerializeObject(s, definition);
			( _bottom as IParameter ).SerializeObject(s, definition);
		}
	}
}