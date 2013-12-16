using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class StringToken
		: IParameter
	{
		private IParameter _data;
		private bool _isSigned;

		public StringToken () { }
		public StringToken (PlayerReference playerRef) { _data = playerRef; }
		public StringToken (TeamReference teamRef) { _data = teamRef; }
		public StringToken (ObjectReference objectRef) { _data = objectRef; }
		public StringToken (IntegerReference integerRef, bool signed) { _data = integerRef; _isSigned = signed; }
		public StringToken (TimerReference timerRef) { _data = timerRef; }

		#region IParameter Members

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			sbyte type = -1;
			if ( s.State == StreamState.Write && _data != null )
			{
				if ( _data is PlayerReference ) { type = 0; }
				if ( _data is TeamReference ) { type = 1; }
				if ( _data is ObjectReference ) { type = 2; }
				if ( _data is IntegerReference && !_isSigned ) { type = 3; }
				if ( _data is IntegerReference && _isSigned ) { type = 4; }
				if ( _data is TimerReference ) { type = 5; }
			}
			s.StreamPlusOne(ref type, 3);

			if ( s.State == StreamState.Read )
			{
				switch ( type )
				{
					case 0: _data = new PlayerReference(); break;
					case 1: _data = new TeamReference(); break;
					case 2: _data = new ObjectReference(); break;
					case 3: _data = new IntegerReference(); _isSigned = false; break;
					case 4: _data = new IntegerReference(); _isSigned = true; break;
					case 5: _data = new TimerReference(); break;
				}
			}

			if ( _data != null )
				_data.SerializeObject(s, definition);
		}

		#endregion
	}
}