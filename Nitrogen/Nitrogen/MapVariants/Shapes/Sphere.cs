using Nitrogen.IO;
using Nitrogen.MapVariants.MapObjects;
using System;

namespace Nitrogen.MapVariants.Shapes
{
	/// <summary>
	/// Represents a spherical boundary.
	/// </summary>
	public struct Sphere
		: IShape
	{
		private short _radius;

		/// <summary>
		/// Gets or sets the radius of this sphere.
		/// </summary>
		public short Radius
		{
			get { return _radius; }
			set { _radius = value; }
		}

		#region IShape Members

		byte IShape.BoundaryIndex { get { return 1; } }

		public bool IsInBoundary (float x, float y, float z, MapObject o)
		{
			/*return
				( o.X >= x - _radius && o.X <= x + _radius ) &&
				( o.Y >= y - _radius && o.Y <= y + _radius ) &&
				( o.Z >= z - _radius && o.Z <= z + _radius );*/
			return false;
		}

		#endregion

		#region ISerializable<BitStream> Members

		public void SerializeObject (BitStream s)
		{
			s.Stream(ref _radius);
		}

		#endregion
	}
}
