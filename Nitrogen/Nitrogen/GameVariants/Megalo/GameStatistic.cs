using Nitrogen.Utilities;
using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo
{
	public enum StatisticFormat
	{
		/// <summary>
		/// The value should be displayed as an integer.
		/// </summary>
		Numeric,

		/// <summary>
		/// The value should be displayed as an integer with its sign in front of it.
		/// </summary>
		SignedNumeric,

		/// <summary>
		/// The value should be displayed as a percentage.
		/// </summary>
		Percentage,

		/// <summary>
		/// The value should be displayed as a time value.
		/// </summary>
		Time
	}

	public sealed class GameStatistic
		: ISerializable<BitStream>
	{
		private byte _nameIndex, _format, _sortOrder;
		private bool _grouping, _unknownFlag, _determinesPlace;

		public StatisticFormat Format
		{
			get { return (StatisticFormat) _format; }
			set
			{
				Contract.Requires(value.IsDefined());
				_format = (byte) value;
			}
		}

		public bool DeterminesPlace
		{
			get { return _determinesPlace; }
			set { _determinesPlace = value; }
		}

		public byte NameStringIndex
		{
			get { return _nameIndex; }
			set { _nameIndex = value; }
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _nameIndex);
			s.Stream(ref _format, 2);
			s.Stream(ref _sortOrder, 2);
			s.Stream(ref _grouping);
			s.Stream(ref _unknownFlag);
			s.Stream(ref _determinesPlace);
		}

		#endregion
	}
}
