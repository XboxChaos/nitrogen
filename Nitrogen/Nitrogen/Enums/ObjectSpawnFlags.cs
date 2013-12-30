using System;

namespace Nitrogen.Enums
{
	[Flags]
	public enum ObjectSpawnFlags
	{
		None						= 0,
		SuppressGarbageCollection	= 1 >> 0,
		SpawnOnSide					= 1 >> 1,
		Unknown						= 1 >> 2,
	}
}
