using Nitrogen.Utilities;
using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Base
{
	/// <summary>
	/// Indicates whether players are able to change teams during a match.
	/// </summary>
	public enum TeamChangingMode
	{
		/// <summary>
		/// Players may not switch teams during the match.
		/// </summary>
		Disabled,

		/// <summary>
		/// Players may switch teams during the match.
		/// </summary>
		Enabled,

		/// <summary>
		/// Players may switch teams only to balance out teams.
		/// </summary>
		TeamBalancing,
	}

	/// <summary>
	/// Represents a set of social settings in a Halo 4 game variant.
	/// </summary>
	public sealed class SocialSettings
		: ISerializable<BitStream>
	{
		private bool
            _observerTeam,
            _friendlyFire,
            _betrayalBooting,
            _enemyVoice,
            _openChannelVoice,
            _deadPlayerVoice;

		private byte _teamChangingMode;

		/// <summary>
		/// Initializes a new instance of the <see cref="SocialSettings"/> class with default values.
		/// </summary>
		public SocialSettings ()
		{
			_enemyVoice = true;
			_openChannelVoice = true;
			_deadPlayerVoice = true;
			_friendlyFire = true;
		}

		/// <summary>
		/// Gets or sets whether players can damage their teammates.
		/// </summary>
		public bool FriendlyFireEnabled
		{
			get { return _friendlyFire; }
			set { _friendlyFire = value; }
		}

		/// <summary>
		/// Gets or sets whether players can boot other players.
		/// </summary>
		public bool BetrayalBooting
		{
			get { return _betrayalBooting; }
			set { _betrayalBooting = value; }
		}

		/// <summary>
		/// Gets or sets whether players can hear enemy players within close proximity.
		/// </summary>
		public bool EnemyVoice
		{
			get { return _enemyVoice; }
			set { _enemyVoice = value; }
		}

		/// <summary>
		/// Gets or sets whether the voice channel is open to all players in the match.
		/// </summary>
		public bool OpenChannelVoice
		{
			get { return _openChannelVoice; }
			set { _openChannelVoice = value; }
		}

		/// <summary>
		/// Gets or sets whether dead players and their killer can hear each other.
		/// </summary>
		public bool DeadPlayerVoice
		{
			get { return _deadPlayerVoice; }
			set { _deadPlayerVoice = value; }
		}

		/// <summary>
		/// Gets or sets whethers players are able to switch teams during the match.
		/// </summary>
		public TeamChangingMode TeamChanging
		{
			get { return (TeamChangingMode) _teamChangingMode; }
			set
			{
				Contract.Requires(value.IsDefined());
				_teamChangingMode = (byte) value;
			}
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _observerTeam);
			s.Stream(ref _teamChangingMode, 2);
			s.Stream(ref _friendlyFire);
			s.Stream(ref _betrayalBooting);
			s.Stream(ref _enemyVoice);
			s.Stream(ref _openChannelVoice);
			s.Stream(ref _deadPlayerVoice);
		}

		#endregion
	}
}
