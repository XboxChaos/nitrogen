using Nitrogen.GameVariants.Base;
using Nitrogen.IO;
using Nitrogen.Shared;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo
{
	public sealed class MapLoadout
		: ISerializable<BitStream>
	{
		private byte _unknownByte;
		private Loadout _loadout;

		public MapLoadout()
		{
			_loadout = new Loadout();
		}

		public Loadout Loadout
		{
			get { return _loadout; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_loadout = value;
			}
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _unknownByte, 2);
			s.SerializeObject(_loadout);
		}

		#endregion
	}
}
