using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo
{
	internal sealed class PlayerRatingParameters
		: ISerializable<BitStream>
	{
		private int[] _playerRatingParams;
		private bool _unknownFlag;

		public PlayerRatingParameters ()
		{
			_playerRatingParams = new int[15];
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(_playerRatingParams);
			s.Stream(ref _unknownFlag);
		}

		#endregion
	}
}
