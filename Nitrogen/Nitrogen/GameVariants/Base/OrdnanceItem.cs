using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.GameVariants.Base
{
	/// <summary>
	/// Represents an ordnance item choice.
	/// </summary>
	public sealed class OrdnanceItem
		: ISerializable<BitStream>
	{
		private string _item;
		private float _weighting;

		/// <summary>
		/// Initializes a new instance of the <see cref="OrdnanceItem"/> class with default values.
		/// </summary>
		public OrdnanceItem ()
		{
			_weighting = 1.0f;
			_item = "";
		}

		/// <summary>
		/// Gets or sets the ordnance item.
		/// </summary>
		public string Item
		{
			get { return _item; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires<ArgumentException>(Encoding.UTF8.GetByteCount(value) <= 32);

				_item = value;
			}
		}

		/// <summary>
		/// Gets or sets the probability of this item.
		/// 
		/// The value must fall in the range between 0 and 10000 or an exception will be thrown.
		/// </summary>
		public float Weight
		{
			get { return _weighting; }
			set
			{
				Contract.Requires<ArgumentOutOfRangeException>(value >= 0 && value <= 10000);
				_weighting = value;
			}
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.StreamNullTerminatedString(ref _item, Encoding.UTF8, maxLength: 32);
			s.StreamEncodedFloat(ref _weighting, bits: 30, min: 0, max: 10000, signed: false, isRounded: true, flag: true);
		}

		#endregion
	}
}
