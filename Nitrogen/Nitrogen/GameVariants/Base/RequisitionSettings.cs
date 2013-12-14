using Nitrogen.IO;
using System;
using System.Collections.Generic;

namespace Nitrogen.GameVariants.Base
{
	/// <summary>
	/// Represents a set of settings in a game variant for some kind of requisition-related feature
	/// which was cut from the release version of Halo 4.
	/// </summary>
	internal sealed class RequisitionSettings
		: ISerializable<BitStream>
	{
		private float _unk0;
		private int _unk1, _unk2;
		private List<RequisitionData> _data;

		/// <summary>
		/// Initializes a new instance of the <see cref="RequisitionSettings"/> class with default values.
		/// </summary>
		public RequisitionSettings ()
		{
			_data = new List<RequisitionData>();
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _unk0);
			s.Stream(ref _unk1);

			int count = _data.Count;
			s.Stream(ref count, 7);

			s.Serialize(_data, 0, count);
			s.Stream(ref _unk2);
		}

		#endregion
	}
}
