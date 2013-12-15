using Nitrogen.IO;
using Nitrogen.Shared;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Base
{
    /// <summary>
    /// Represents a set of respawn settings in a Halo 4 game variant.
    /// </summary>
    public sealed class RespawnSettings
        : ISerializable<BitStream>
    {
        private bool
            _syncWithTeam,
            _respawnAtTeammate, // doesn't seem to work in Halo 4
            _respawnInPlace, // doesn't seem to work in Halo 4
            _respawnOnKill,
            _dualRespawnTiming;

        private byte
            _lives,
            _sharedTeamLives,
            _respawnTime,
            _secondaryRespawnTime,
            _suicdePenalty,
            _betrayalPenalty,
            _respawnTimeGrowth,
            _initialLoadoutDuration, // broken in Halo 4; the lowest you can go is 7.
            _respawnTraitDuration;

        private PlayerTraits _traits;

        /// <summary>
        /// Initializes a new instance of the <see cref="RespawnSettings"/> class with default values.
        /// </summary>
        public RespawnSettings()
        {
            _betrayalPenalty = 5;
            _dualRespawnTiming = true;
            _respawnTime = 3;
            _secondaryRespawnTime = 7;
            _suicdePenalty = 5;
            _traits = new PlayerTraits();
            _respawnTraitDuration = 5;
        }

        /// <summary>
        /// Gets or sets the penalty added to the minimum respawn time when a player commits suicide.
        /// </summary>
        public byte SuicidePenalty
        {
            get { return _suicdePenalty; }
            set { _suicdePenalty = value; }
        }

        /// <summary>
        /// Gets or sets the penalty added to the minimum respawn time whenever a player betrays a
        /// teammate.
        /// </summary>
        public byte BetrayalPenalty
        {
            get { return _betrayalPenalty; }
            set { _betrayalPenalty = value; }
        }

        /// <summary>
        /// Gets or sets the amount of lives each player starts with. Set to 0 for unlimited lives.
        /// 
        /// The value must fall in the range between 0 and 63 or an exception will be thrown.
        /// </summary>
        public byte Lives
        {
            get { return _lives; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= 63);
                _lives = value;
            }
        }

        /// <summary>
        /// Gets or sets the amount of lives which is shared among players in each team. Set to 0
        /// for unlimited lives.
        /// 
        /// The value must fall in the range between 0 and 127 or an exception will be thrown.
        /// </summary>
        public byte SharedTeamLives
        {
            get { return _sharedTeamLives; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= 127);
                _sharedTeamLives = value;
            }
        }

        /// <summary>
        /// Gets or sets whether players respawn at the same time as their teammates.
        /// </summary>
        public bool SyncWithTeam
        {
            get { return _syncWithTeam; }
            set { _syncWithTeam = value; }
        }

        /// <summary>
        /// Gets or sets whether players respawn when their teammate earns a kill.
        /// </summary>
        public bool RespawnOnKill
        {
            get { return _respawnOnKill; }
            set { _respawnOnKill = value; }
        }

        /// <summary>
        /// Gets or sets the initial respawn duration.
        /// </summary>
        public byte InitialRespawnDuration
        {
            get { return _secondaryRespawnTime; }
            set { _secondaryRespawnTime = value; }
        }

        /// <summary>
        /// Gets or sets the minimum respawn duration (which may not be skipped).
        /// </summary>
        public byte MinimumRespawnDuration
        {
            get { return _respawnTime; }
            set
            {
                _respawnTime = value;
                _dualRespawnTiming = (value != 0);
            }
        }

        /// <summary>
        /// Gets or sets the amount added to the minimum respawn duration every time a player dies.
        /// 
        /// The value must fall in the range between 0 and 15 or an exception will be thrown.
        /// </summary>
        public byte RespawnTimeGrowth
        {
            get { return _respawnTimeGrowth; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= 15);
                _respawnTimeGrowth = value;
            }
        }

        /// <summary>
        /// Gets or sets how long in seconds the respawn traits are active after respawning.
        /// </summary>
        public byte RespawnTraitsDuration
        {
            get { return _respawnTraitDuration; }
            set { _respawnTraitDuration = value; }
        }

        /// <summary>
        /// Gets or sets the trait set applied to players whenever they respawn.
        /// </summary>
        public PlayerTraits RespawnTraits
        {
            get { return _traits; }
            set { _traits = value; }
        }

        void ISerializable<BitStream>.SerializeObject(BitStream s)
        {
            /* Crash Prevention: Respawn time must be less than secondary respawn time.
             * 
             * Matt: Should we put this in a separate method since we are technically supposed to
             *       stream data, not modify them here? There's also the possibility of different
             *       streams (i.e. JSON input/output).
             */
            if (_respawnTime > _secondaryRespawnTime)
                _respawnTime = _secondaryRespawnTime;

            s.Stream(ref _syncWithTeam);
            s.Stream(ref _respawnAtTeammate);
            s.Stream(ref _respawnInPlace);
            s.Stream(ref _respawnOnKill);
            s.Stream(ref _dualRespawnTiming);
            s.Stream(ref _lives, bits: 6);
            s.Stream(ref _sharedTeamLives, bits: 7);
            s.Stream(ref _respawnTime);
            s.Stream(ref _secondaryRespawnTime);
            s.Stream(ref _suicdePenalty);
            s.Stream(ref _betrayalPenalty);
            s.Stream(ref _respawnTimeGrowth, bits: 4);
            s.Stream(ref _initialLoadoutDuration, bits: 4);
			s.Stream(ref _respawnTraitDuration, bits: 6);
            s.SerializeObject(_traits);
        }
    }
}
