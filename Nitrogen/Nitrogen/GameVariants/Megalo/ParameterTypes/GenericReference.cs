using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class GenericReference
		: IParameter
	{
		private IParameter _data;

		public GenericReference (PlayerReference playerRef) { _data = playerRef; }
		public GenericReference (TeamReference teamRef) { _data = teamRef; }
		public GenericReference (ObjectReference objectRef) { _data = objectRef; }
		public GenericReference (IntegerReference integerRef) { _data = integerRef; }
		public GenericReference (TimerReference timerRef) { _data = timerRef; }
		internal GenericReference () { }

		#region IParameter Members

		ParameterType IParameter.ParameterType { get { return ParameterType.GenericReference; } }

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			sbyte type = -1;

			if ( s.State == StreamState.Write )
			{
				if ( _data is IntegerReference ) { type = 0; }
				if ( _data is PlayerReference ) { type = 1; }
				if ( _data is ObjectReference ) { type = 2; }
				if ( _data is TeamReference ) { type = 3; }
				if ( _data is TimerReference ) { type = 4; }
			}

			s.StreamUnsigned(ref type, 3);

			if ( s.State == StreamState.Read )
			{
				switch ( type )
				{
					case 0: _data = new IntegerReference(); break;
					case 1: _data = new PlayerReference(); break;
					case 2: _data = new ObjectReference(); break;
					case 3: _data = new TeamReference(); break;
					case 4: _data = new TimerReference(); break;
				}
			}

			_data.SerializeObject(s, definition);
		}

		#endregion
	}
}
