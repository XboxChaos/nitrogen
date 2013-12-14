using Nitrogen.IO;
using Nitrogen.MapVariants.MapObjects;
using System;

namespace Nitrogen.MapVariants.Shapes
{
	/// <summary>
	/// Represents a cylindrical boundary.
	/// </summary>
	public struct Cylinder
		: IShape
	{
		private short _radius, _top, _bottom;

		/// <summary>
		/// Gets or sets the radius of this cylinder.
		/// </summary>
		public short Radius
		{
			get { return _radius; }
			set { _radius = value; }
		}

		/// <summary>
		/// Gets or sets the upper height of this cylinder from its center.
		/// </summary>
		public short Top
		{
			get { return _top; }
			set { _top = value; }
		}

		/// <summary>
		/// Gets or sets the lower height of this cylinder from its center.
		/// </summary>
		public short Bottom
		{
			get { return _bottom; }
			set { _bottom = value; }
		}

		#region IShape Members

		byte IShape.BoundaryIndex { get { return 2; } }

		public bool IsInBoundary (float x, float y, float z, MapObject o)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region ISerializable<BitStream> Members

		public void SerializeObject (BitStream s)
		{
			s.Stream(ref _radius);
			s.Stream(ref _top);
			s.Stream(ref _bottom);
		}

		#endregion
	}
}
