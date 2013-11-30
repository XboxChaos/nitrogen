using Nitrogen.ContentData.GameVariants.Megalo;
using Nitrogen.IO;
using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.Megalo.ParameterTypes
{
    public class IntegerReference
        : IScriptObject
    {
        private byte type;
        private short value;
        private byte index, id; 

        public enum Type
            : byte
        {
            Constant, // readonly
            PlayerMemberVariable, // var index
            ObjectMemberVariable, // var index
            TeamMemberVariable, // var index
            GlobalVariable, // var index
            ScratchVariable, // scratch index
            UserDefinedOption, // option index; readonly
            SpawnSequence, // readonly
            UserData, // readonly
            Unknown9, // readonly; halo 4 only
            TeamScore,
            PlayerScore,
            Unknown12, // requisition related; involves scenario data; readonly
            PlayerRating, // readonly
            PlayerStatistic, // stat index
            TeamStatistic, // stat index
            Unused16, // readonly; always returns zero
            CurrentRound, // readonly
            GetSymmetry, // bool; readonly
            Symmetry, // bool
            GameVariantControlsVictory, // bool
            ScoreLimit,
            TeamLivesRemaining,
            PlayerLivesRemaining,
            PlayerSpawnDelay,

            // all properties below are readonly ---
            Unknown25, // readonly; player
            Unknown26, // readonly; player
            Unknown27, // readonly; player
            Unknown28, // readonly; team
            Unknown29, // readonly; team
            Unknown30, // readonly; team
            Unknown31, // readonly; MP property index; object; used in dominion (dom_res_zone)
            Unknown32, // readonly; MP property index; object
            ScoreToWin,
            Unknown34, // bool
            TeamsEnabled, // bool
            RoundTimeLimit,
            NumberOfRounds,
            RoundsResetMap, // bool
            EarlyVictoryWinCount,
            LivesPerRound,
            TeamLivesPerRound,
            RespawnDuration,
            SuicidePenalty,
            BetrayalPenalty,
            RespawnGrowthTime,
            InitialLoadoutSelectionTime,
            RespawnTraitsDuration,
            FriendlyFire, // bool
            BetrayalBooting, // bool
            EnemyVoice, // bool
            OpenChannelVoice, // bool
            DeadPlayerVoice, // bool
            GrenadesOnMap, // bool
            IndestructibleVehicles,
            DamageBoostAlphaDuration,
            SpeedBoostAlphaDuration,
            OvershieldAlphaDuration,
            CustomPowerupAlphaDuration,
            DamageBoostBetaDuration,
            SpeedBoostBetaDuration,
            OvershieldBetaDuration,
            CustomPowerupBetaDuration,
            MapLoadoutsEnabled,
            InitialOrdnanceEnabled,
            RandomOrdnanceEnabled,
            ObjectiveOrdnanceEnabled,
            PersonalOrdnanceEnabled,
            OrdnanceSystemEnabled,
            KillCamEnabled,
            FinalKillCamEnabled,
            OvertimeDuration,
            DamageType,
        }

        public Type IntegerReferenceType
        {
            get { return (Type)this.type; }
            set
            {
                Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(Type), value));
                this.type = (byte)value;
            }
        }

        public short ConstantValue
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public byte Target
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public byte Index
        {
            get { return this.index; }
            set { this.index = value; }
        }

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.type, 7);
            Type type = (Type)this.type;
            switch (type)
            {
                case Type.Constant:
                    s.Stream(ref this.value);
                    break;

                case Type.PlayerMemberVariable:
                    s.Stream(ref this.id, 6);
                    s.Stream(ref this.index, 4);
                    break;

                case Type.ObjectMemberVariable:
                case Type.TeamMemberVariable:
                    s.Stream(ref this.id, 5);
                    s.Stream(ref this.index, 4);
                    break;

                case Type.GlobalVariable:
                    s.Stream(ref this.index, 5);
                    break;

                case Type.ScratchVariable:
                case Type.UserDefinedOption:
                    s.Stream(ref this.index, 4);
                    break;

                case Type.SpawnSequence:
                case Type.UserData:
                case Type.Unknown9:
                case Type.Unknown31:
                case Type.Unknown32:
                case Type.TeamScore:
                case Type.TeamLivesRemaining:
                case Type.Unknown28:
                case Type.Unknown29:
                case Type.Unknown30:
                    s.Stream(ref this.id, 5);
                    break;

                case Type.PlayerScore:
                case Type.Unknown12:
                case Type.PlayerRating:
                case Type.PlayerLivesRemaining:
                case Type.PlayerSpawnDelay:
                case Type.Unknown25:
                case Type.Unknown26:
                case Type.Unknown27:
                    s.Stream(ref this.id, 6);
                    break;

                case Type.PlayerStatistic:
                    s.Stream(ref this.id, 6);
                    s.Stream(ref this.index, 2);
                    break;

                case Type.TeamStatistic:
                    s.Stream(ref this.id, 5);
                    s.Stream(ref this.index, 2);
                    break;
            }
        }
    }
}
