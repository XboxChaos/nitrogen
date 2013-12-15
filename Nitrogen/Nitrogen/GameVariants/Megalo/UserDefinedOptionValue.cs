using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo
{
	public interface IUserDefinedOptionValue
		: ISerializable<BitStream>
	{
		short Value { get; set; }
	}

	public sealed class UserDefinedOptionValue
		: IUserDefinedOptionValue
	{
		private short _value;
		private sbyte _nameStringIndex, _descStringIndex;

		public sbyte NameStringIndex
		{
			get { return _nameStringIndex; }
			set { _nameStringIndex = value; }
		}

		public sbyte DescriptionStringIndex
		{
			get { return _descStringIndex; }
			set { _descStringIndex = value; }
		}

		#region IUserDefinedOptionValue Members

		public short Value
		{
			get { return _value; }
			set { _value = value; }
		}

		#endregion

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _value, 10);
			s.Stream(ref _nameStringIndex);
			s.Stream(ref _descStringIndex);
		}

		#endregion
	}

	public sealed class UserDefinedOptionRangeEndpoint
		: IUserDefinedOptionValue
	{
		private short _value;

		#region IUserDefinedOptionValue Members

		public short Value
		{
			get { return _value; }
			set { _value = value; }
		}

		#endregion

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _value, bits: 10);
		}

		#endregion
	}
}
