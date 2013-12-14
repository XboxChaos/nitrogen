using Nitrogen.IO;
using Nitrogen.MapVariants.MapObjects;
using System;

namespace Nitrogen.MapVariants.Shapes
{
	internal interface IShape
		: ISerializable<BitStream>
	{
		byte BoundaryIndex { get; }

		bool IsInBoundary (float x, float y, float z, MapObject o);
	}
}
