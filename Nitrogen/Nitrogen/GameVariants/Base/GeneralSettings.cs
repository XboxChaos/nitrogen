using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Base
{
    /// <summary>
    /// Represents a set of general settings in a Halo 4 game variant.
    /// </summary>
    public sealed class GeneralSettings
        : ISerializable<BitStream>
    {
        private bool
            _teamsEnabled,
            _roundsResetPlayers,
            _roundsResetMap,
            _unknownFlag3,
            _deathCamEnabled,
            _pointsSystemEnabled,
            _finalKillCamEnabled,
            _unknownFlag1,
            _unknownFlag2;

        private byte
            _roundTimeLimit,
            _numberOfRounds,
            _earlyVictoryWinCount,
            _suddenDeathDuration,
            _moshDifficulty;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralSettings"/> class with default values.
        /// </summary>
        public GeneralSettings()
        {
            _teamsEnabled = true;
            _roundTimeLimit = 15;
            _numberOfRounds = 1;
            _deathCamEnabled = true;
            _pointsSystemEnabled = true;
            _suddenDeathDuration = 90;
        }

        /// <summary>
        /// Gets or sets the number of rounds per match.
        /// 
        /// The value must fall in the range between 0 and 31 or an exception will be thrown.
        /// </summary>
        public byte NumberOfRounds
        {
            get { return _numberOfRounds; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= 31);
                _numberOfRounds = value;
            }
        }

        /// <summary>
        /// Gets or sets whether teams are enabled or not.
        /// </summary>
        public bool TeamsEnabled
        {
            get { return _teamsEnabled; }
            set { _teamsEnabled = value; }
        }

        /// <summary>
        /// Gets or sets whether the death cam (aka killcam) is enabled for all players.
        /// </summary>
        public bool DeathCamEnabled
        {
            get { return _deathCamEnabled; }
            set { _deathCamEnabled = value; }
        }

        /// <summary>
        /// Gets or sets whether the final kill cam is enabled for all players.
        /// </summary>
        public bool FinalKillCamEnabled
        {
            get { return _finalKillCamEnabled; }
            set { _finalKillCamEnabled = value; }
        }

        /// <summary>
        /// Gets or sets whether the Halo 4 style points system is enabled.
        /// </summary>
        public bool PointsSystemEnabled
        {
            get { return _pointsSystemEnabled; }
            set { _pointsSystemEnabled = value; }
        }

        /// <summary>
        /// Gets or sets the number of wins a player or team can reach for an early victory (e.g. best 2 out of 3).
        /// </summary>
        public byte EarlyVictoryWinCount
        {
            get { return _earlyVictoryWinCount; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= 15);
                _earlyVictoryWinCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the duration of the overtime period.
        /// </summary>
        public byte OvertimeDuration
        {
            get { return _suddenDeathDuration; }
            set { _suddenDeathDuration = value; }
        }

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject(BitStream s)
        {
            s.Stream(ref _teamsEnabled);
            s.Stream(ref _roundsResetPlayers);
            s.Stream(ref _roundsResetMap);
            s.Stream(ref _unknownFlag3);
            s.Stream(ref _roundTimeLimit);
            s.Stream(ref _numberOfRounds, 5);
            s.Stream(ref _earlyVictoryWinCount, 4);
            s.Stream(ref _deathCamEnabled);
            s.Stream(ref _pointsSystemEnabled);
            s.Stream(ref _finalKillCamEnabled);
            s.Stream(ref _suddenDeathDuration);
            s.Stream(ref _unknownFlag1);
            s.Stream(ref _unknownFlag2);
            s.Stream(ref _moshDifficulty, 2);
		}

		#endregion
	}
}
