using Nitrogen.Utilities;
using Nitrogen.IO;
using System;
using System.Collections.Generic;
using Nitrogen.GameVariants.Megalo.ParameterTypes;
using Nitrogen.GameVariants.Megalo.VariableDefinitions;

namespace Nitrogen.GameVariants.Megalo
{
	public enum NetworkPriority
	{
		None,
		Low,
		High,
		Default
	}

	public sealed class VariableTable
		: ISerializable<BitStream>
	{
		int _intCountSize, _timerCountSize, _teamCountSize, _playerCountSize, _objectCountSize;
		IList<IntegerVariableDefinition> _integers;
		IList<TimerVariableDefinition> _timers;
		IList<TeamVariableDefinition> _teams;
		IList<PlayerVariableDefinition> _players;
		IList<ObjectVariableDefinition> _objects;

		public VariableTable(int intCountSize, int timerCountSize, int teamCountSize, int playerCountSize, int objectCountSize)
		{
			_intCountSize = intCountSize;
			_timerCountSize = timerCountSize;
			_teamCountSize = teamCountSize;
			_playerCountSize = playerCountSize;
			_objectCountSize = objectCountSize;

			_integers = new List<IntegerVariableDefinition>();
			_timers = new List<TimerVariableDefinition>();
			_teams = new List<TeamVariableDefinition>();
			_players = new List<PlayerVariableDefinition>();
			_objects = new List<ObjectVariableDefinition>();
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.SerializeObjects(_integers, _intCountSize);
			s.SerializeObjects(_timers, _timerCountSize);
			s.SerializeObjects(_teams, _teamCountSize);
			s.SerializeObjects(_players, _playerCountSize);
			s.SerializeObjects(_objects, _objectCountSize);
		}

		#endregion
	}
}
