using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo
{
	public sealed class GlobalGameOptions
		: ISerializable<BitStream>
	{
		private short _scoreLimit;
		private bool _unknownFlag1, _unknownFlag2, _unknownFlag3;

		/// <summary>
		/// Gets or sets the score limit for each round.
		/// </summary>
		public short ScoreLimit
		{
			get { return _scoreLimit; }
			set { _scoreLimit = value; }
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _scoreLimit);
			s.Stream(ref _unknownFlag1);
			s.Stream(ref _unknownFlag2);
			s.Stream(ref _unknownFlag3);
		}

		#endregion
	}
}