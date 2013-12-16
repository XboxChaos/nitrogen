using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public enum MeterType
	{
		None,
		Fraction,
		Timer
	}

	public sealed class MeterData
		: IParameter
	{
		private byte _type;
		private IntegerReference _numerator, _denominator;
		private TimerReference _timer;

		public MeterType Type
		{
			get { return (MeterType) _type; }
			set
			{
				Contract.Requires(value.IsDefined());
				_type = (byte) value;
			}
		}

		public IntegerReference Numerator
		{
			get { return _numerator; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_numerator = value;
			}
		}

		public IntegerReference Denominator
		{
			get { return _denominator; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_denominator = value;
			}
		}

		public TimerReference Timer
		{
			get { return _timer; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_timer = value;
			}
		}

		#region IParameter Members

		ParameterType IParameter.ParameterType { get { return ParameterType.Meter; } }

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			s.Stream(ref _type, 2);
			var meterType = (MeterType) _type;
			switch ( meterType )
			{
				case MeterType.Fraction:
					( _numerator as IParameter ).SerializeObject(s, definition);
					( _denominator as IParameter ).SerializeObject(s, definition);
					break;

				case MeterType.Timer:
					( _timer as IParameter ).SerializeObject(s, definition);
					break;
			}
		}

		#endregion
	}
}
