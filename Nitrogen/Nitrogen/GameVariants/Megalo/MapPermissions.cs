using Nitrogen.IO;
using System;
using System.Collections.Generic;

namespace Nitrogen.GameVariants.Megalo
{
	public sealed class MapPermissions
		: ISerializable<BitStream>
	{
		private List<ushort> _mapPermissions;
		private bool _isExclusive;

		public MapPermissions ()
		{
			_mapPermissions = new List<ushort>();
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			int count = _mapPermissions.Count;
			s.Stream(ref count, 6);
			if ( count >= 0 && count < 32 )
			{
				if ( s.State == StreamState.Read )
					_mapPermissions = new List<ushort>(new ushort[count]);

				s.Stream(_mapPermissions);
				s.Stream(ref _isExclusive);
			}
		}

		#endregion
	}
}
