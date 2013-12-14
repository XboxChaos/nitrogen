using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Base
{
	/// <summary>
	/// Represents a set of team properties in a Halo 4 game variant.
	/// </summary>
	public sealed class TeamSettings
		: ISerializable<BitStream>
	{
		public const int TeamCount = 8;

		private byte _teamModelOverride, _designatorSwitchType;
		private Team[] _teams;

		/// <summary>
		/// Initializes a new instance of the <see cref="TeamSettings"/> class with default values.
		/// </summary>
		public TeamSettings ()
		{
			_teams = new Team[TeamCount];
			for ( int i = 0; i < TeamCount; i++ )
				_teams[i] = new Team();
		}

		public Team this[int index]
		{
			get
			{
				Contract.Requires<IndexOutOfRangeException>(index >= 0 && index < TeamCount);
				return _teams[index];
			}

			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires<IndexOutOfRangeException>(index >= 0 && index < TeamCount);
				_teams[index] = value;
			}
		}

		public Team[] GetTeams ()
		{
			return _teams;
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _teamModelOverride, bits: 3);
			s.Stream(ref _designatorSwitchType, bits: 2);
			s.Serialize(_teams, 0, TeamCount);
		}

		#endregion
	}
}
