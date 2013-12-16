using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public enum IntegerReferenceType
	{
		Constant,
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
		Unknown25, // player
		Unknown26, // player
		Unknown27, // player
		Unknown28, // team
		Unknown29, // team
		Unknown30, // team
		Unknown31, // MP property index; object; used in dominion (dom_res_zone)
		Unknown32, // MP property index; object
		ScoreToWin,
		Unknown34, // bool
		TeamsEnabled, // bool
		RoundTimeLimit,
		NumberOfRounds,
		RoundsResetMap,
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

	public sealed class IntegerReference
		: IParameter
	{
		private byte _type;
		private short _value;
		private byte _index, _id;

		public IntegerReferenceType ReferenceType
		{
			get { return (IntegerReferenceType) _type; }
			set
			{
				Contract.Requires(value.IsDefined());
				_type = (byte) value;
			}
		}

		public short ConstantValue
		{
			get { return _value; }
			set { _value = value; }
		}

		public byte Target
		{
			get { return _id; }
			set { _id = value; }
		}

		public byte Index
		{
			get { return _index; }
			set { _index = value; }
		}

		#region IParameter Members

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			s.Stream(ref _type, 7);
			IntegerReferenceType type = (IntegerReferenceType) _type;
			switch ( type )
			{
				case IntegerReferenceType.Constant:
					s.Stream(ref _value);
					break;

				case IntegerReferenceType.PlayerMemberVariable:
					s.Stream(ref _id, 6);
					s.Stream(ref _index, 4);
					break;

				case IntegerReferenceType.ObjectMemberVariable:
				case IntegerReferenceType.TeamMemberVariable:
					s.Stream(ref _id, 5);
					s.Stream(ref _index, 4);
					break;

				case IntegerReferenceType.GlobalVariable:
					s.Stream(ref _index, 5);
					break;

				case IntegerReferenceType.ScratchVariable:
				case IntegerReferenceType.UserDefinedOption:
					s.Stream(ref _index, 4);
					break;

				case IntegerReferenceType.SpawnSequence:
				case IntegerReferenceType.UserData:
				case IntegerReferenceType.Unknown9:
				case IntegerReferenceType.Unknown31:
				case IntegerReferenceType.Unknown32:
				case IntegerReferenceType.TeamScore:
				case IntegerReferenceType.TeamLivesRemaining:
				case IntegerReferenceType.Unknown28:
				case IntegerReferenceType.Unknown29:
				case IntegerReferenceType.Unknown30:
					s.Stream(ref _id, 5);
					break;

				case IntegerReferenceType.PlayerScore:
				case IntegerReferenceType.Unknown12:
				case IntegerReferenceType.PlayerRating:
				case IntegerReferenceType.PlayerLivesRemaining:
				case IntegerReferenceType.PlayerSpawnDelay:
				case IntegerReferenceType.Unknown25:
				case IntegerReferenceType.Unknown26:
				case IntegerReferenceType.Unknown27:
					s.Stream(ref _id, 6);
					break;

				case IntegerReferenceType.PlayerStatistic:
					s.Stream(ref _id, 6);
					s.Stream(ref _index, 2);
					break;

				case IntegerReferenceType.TeamStatistic:
					s.Stream(ref _id, 5);
					s.Stream(ref _index, 2);
					break;
			}
		}

		#endregion
	}
}
