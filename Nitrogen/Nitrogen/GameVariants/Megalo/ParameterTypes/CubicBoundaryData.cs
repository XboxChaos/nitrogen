using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class CubicBoundaryData
		: BoundaryData
	{
		private IntegerReference _top, _bottom, _length, _width;

		public CubicBoundaryData ()
			: base(Shape.Box)
		{
			_top = new IntegerReference();
			_bottom = new IntegerReference();
			_length = new IntegerReference();
			_width = new IntegerReference();
		}

		public CubicBoundaryData(IntegerReference top, IntegerReference bottom, IntegerReference length, IntegerReference width)
			: this()
		{
			_top = top;
			_bottom = bottom;
			_length = length;
			_width = width;
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

		public IntegerReference Width
		{
			get { return _width; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_width = value;
			}
		}

		public IntegerReference Length
		{
			get { return _length; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_length = value;
			}
		}

		internal override void SerializeData (BitStream s, ParameterDefinition definition)
		{
			( _width as IParameter ).SerializeObject(s, definition);
			( _length as IParameter ).SerializeObject(s, definition);
			( _top as IParameter ).SerializeObject(s, definition);
			( _bottom as IParameter ).SerializeObject(s, definition);
		}
	}
}
