using Nitrogen.IO;
using Nitrogen.MapVariants.MapObjects;
using System;

namespace Nitrogen.MapVariants.Shapes
{
	public struct Box
		: IShape
	{
		private short _width, _length, _top, _bottom;

		/// <summary>
		/// Gets or sets the width of this box.
		/// </summary>
		public short Width
		{
			get { return _width; }
			set { _width = value; }
		}

		/// <summary>
		/// Gets or sets the length of this box.
		/// </summary>
		public short Length
		{
			get { return _length; }
			set { _length = value; }
		}

		/// <summary>
		/// Gets or sets the upper height of this box from its center.
		/// </summary>
		public short Top
		{
			get { return _top; }
			set { _top = value; }
		}

		/// <summary>
		/// Gets or sets the lower height of this box from its center.
		/// </summary>
		public short Bottom
		{
			get { return _bottom; }
			set { _bottom = value; }
		}

		#region IShape Members

		byte IShape.BoundaryIndex { get { return 3; } }

		public bool IsInBoundary (float x, float y, float z, MapObject o)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _width);
			s.Stream(ref _length);
			s.Stream(ref _top);
			s.Stream(ref _bottom);
		}

		#endregion
	}
}
