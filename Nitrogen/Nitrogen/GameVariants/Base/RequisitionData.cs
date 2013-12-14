using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Base
{
	internal sealed class RequisitionData
		: ISerializable<BitStream>
	{
		private byte _paletteIndex;
		private bool _unknownFlag;
		private int _unknownInt;
		private int _maxInstances;
		private int _modelVariantStringIndex;
		private int _initialAmmo;

		private float
			_unknownFloat1,
			_unknownFloat2,
			_unknownFloat3,
			_unknownFloat4,
			_unknownFloat5;

		private byte
			_maxBuyPlayer,
			_maxBuyTeam;

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _paletteIndex);
			s.Stream(ref _unknownFlag);
			s.Stream(ref _unknownInt);
			s.Stream(ref _maxInstances, 30);
			s.Stream(ref _unknownFloat1);
			s.Stream(ref _modelVariantStringIndex, 30);
			s.Stream(ref _initialAmmo);
			s.Stream(ref _unknownFloat2);
			s.Stream(ref _unknownFloat3);
			s.Stream(ref _unknownFloat4);
			s.Stream(ref _unknownFloat5);
			s.Stream(ref _maxBuyPlayer);
			s.Stream(ref _maxBuyTeam);
		}

		#endregion
	}
}
