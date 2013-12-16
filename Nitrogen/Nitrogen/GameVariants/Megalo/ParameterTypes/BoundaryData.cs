using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public enum BoundaryType
	{
		None,
		Sphere,
		Cylinder,
		Box
	}

	public class BoundaryData
		: IParameter
	{
		private byte _shape;
		private IntegerReference _top, _bottom, _length, _width, _radius;

		#region IParameter Members

		ParameterType IParameter.ParameterType { get { return ParameterType.Shape; } }

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			s.Stream(ref _shape, 2);
			var shape = (BoundaryType) _shape;
			switch ( shape )
			{
				case BoundaryType.Sphere:
					( _radius as IParameter ).SerializeObject(s, definition);
					break;

				case BoundaryType.Cylinder:
					( _radius as IParameter ).SerializeObject(s, definition);
					( _top as IParameter ).SerializeObject(s, definition);
					( _bottom as IParameter ).SerializeObject(s, definition);
					break;

				case BoundaryType.Box:
					( _width as IParameter ).SerializeObject(s, definition);
					( _length as IParameter ).SerializeObject(s, definition);
					( _top as IParameter ).SerializeObject(s, definition);
					( _bottom as IParameter ).SerializeObject(s, definition);
					break;
			}
		}

		#endregion
	}
}
