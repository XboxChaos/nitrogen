using Nitrogen.IO;
using System;

namespace Nitrogen.MapVariants
{
	public class ObjectTypeCount
		: ISerializable<BitStream>
	{
		private byte _min, _max, _total;

		public byte MinCount
		{
			get { return _min; }
			set { _min = value; }
		}

		public byte MaxCount
		{
			get { return _max; }
			set { _max = value; }
		}

		public byte Total
		{
			get { return _total; }
			set { _total = value; }
		}

		#region ISerializable<BitStream> Members

		public void SerializeObject (BitStream s)
		{
			s.Stream(ref _min);
			s.Stream(ref _max);
			s.Stream(ref _total);
		}

		#endregion
	}
}
