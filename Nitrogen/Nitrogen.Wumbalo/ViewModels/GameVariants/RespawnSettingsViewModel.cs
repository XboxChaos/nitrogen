using Nitrogen.Enums;
using Nitrogen.GameVariants;
using Nitrogen.GameVariants.Base;
using Nitrogen.GameVariants.Megalo;
using Nitrogen.Metadata;
using System;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class RespawnSettingsViewModel
		: Inpc
	{
		private RespawnSettings _respawn;
		private GameVariant _variant;

		public RespawnSettingsViewModel (GameVariant variant)
		{
			_variant = variant;
			_respawn = _variant.RespawnSettings;
		}

		public byte SuicidePenalty
		{
			get { return _respawn.SuicidePenalty; }
			set
			{
				_respawn.SuicidePenalty = value;
				OnPropertyChanged();
			}
		}

		public byte BetrayalPenalty
		{
			get { return _respawn.BetrayalPenalty; }
			set
			{
				_respawn.BetrayalPenalty = value;
				OnPropertyChanged();
			}
		}

		public byte Lives
		{
			get { return _respawn.Lives; }
			set
			{
				_respawn.Lives = value;
				OnPropertyChanged();
			}
		}

		public byte TeamLives
		{
			get { return _respawn.SharedTeamLives; }
			set
			{
				_respawn.SharedTeamLives = value;
				OnPropertyChanged();
			}
		}

		public bool SyncWithTeam
		{
			get { return _respawn.SyncWithTeam; }
			set
			{
				_respawn.SyncWithTeam = value;
				OnPropertyChanged();
			}
		}

		public bool RespawnOnKill
		{
			get { return _respawn.RespawnOnKill; }
			set
			{
				_respawn.RespawnOnKill = value;
				OnPropertyChanged();
			}
		}

		public byte RespawnTimeGrowth
		{
			get { return _respawn.RespawnTimeGrowth; }
			set
			{
				_respawn.RespawnTimeGrowth = value;
				OnPropertyChanged();
			}
		}

		public byte MinimumRespawnTime
		{
			get { return _respawn.MinimumRespawnDuration; }
			set
			{
				_respawn.MinimumRespawnDuration = value;
				if ( value > InitialRespawnTime )
					InitialRespawnTime = value;

				OnPropertyChanged();
			}
		}

		public byte InitialRespawnTime
		{
			get { return _respawn.InitialRespawnDuration; }
			set
			{
				_respawn.InitialRespawnDuration = value;
				if ( value < MinimumRespawnTime )
					MinimumRespawnTime = value;

				OnPropertyChanged();
			}
		}
	}
}
