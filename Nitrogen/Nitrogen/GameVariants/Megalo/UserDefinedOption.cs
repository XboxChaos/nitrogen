using Nitrogen.IO;
using System;
using System.Collections.Generic;

namespace Nitrogen.GameVariants.Megalo
{
	public sealed class UserDefinedOption
		: ISerializable<BitStream>
	{
		private byte _nameStringIndex, _descStringIndex;
		private bool _isRange;
		private ushort _defaultRangeValue, _selectedRangeValue;
		private byte _defaultValueIndex, _selectedValueIndex;
		private List<IUserDefinedOptionValue> _values;

		public UserDefinedOption ()
		{
			_values = new List<IUserDefinedOptionValue>();
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _nameStringIndex);
			s.Stream(ref _descStringIndex);
			s.Stream(ref _isRange);

			int count;
			if ( _isRange )
			{
				s.Stream(ref _defaultRangeValue, 10);
				count = 2;
			}
			else
			{
				s.Stream(ref _defaultValueIndex, 4);
				count = _values.Count;
				s.Stream(ref count, 5);
			}

			for ( int i = 0; i < count; i++ )
			{
				if ( s.State == StreamState.Read )
				{
					if ( _isRange )
						_values.Add(new UserDefinedOptionRangeEndpoint());
					else
						_values.Add(new UserDefinedOptionValue());
				}

				s.SerializeObject(_values[i]);
			}

			if ( _isRange )
				s.Stream(ref _selectedRangeValue, 10);
			else
				s.Stream(ref _selectedValueIndex, 4);
		}

		#endregion
	}
}
