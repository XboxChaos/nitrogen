using Nitrogen.Enums;
using Nitrogen.GameVariants;
using Nitrogen.GameVariants.Base;
using Nitrogen.GameVariants.Megalo;
using Nitrogen.Metadata;
using System;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class GeneralSettingsViewModel
		: Inpc
	{
		private GeneralSettings _general;
		private GameVariant _variant;

		public GeneralSettingsViewModel (GameVariant variant)
		{
			_variant = variant;
			_general = _variant.GeneralSettings;
		}

		public bool TeamsEnabled
		{
			get { return _general.TeamsEnabled; }
			set
			{
				_general.TeamsEnabled = value;
				OnPropertyChanged();
			}
		}

		public byte RoundDuration
		{
			get { return _general.RoundTimeLimit; }
			set
			{
				_general.RoundTimeLimit = value;
				OnPropertyChanged();
			}
		}

		public byte NumberOfRounds
		{
			get { return _general.NumberOfRounds; }
			set
			{
				_general.NumberOfRounds = value;
				OnPropertyChanged();
			}
		}

		public bool UnlimitedRounds
		{
			get { return _general.NumberOfRounds == 0; }
			set
			{
				_general.NumberOfRounds = (byte) ( value ? 0 : 1 );
				OnPropertyChanged();
			}
		}

		public bool PointsSystemEnabled
		{
			get { return _general.PointsSystemEnabled; }
			set
			{
				_general.PointsSystemEnabled = value;
				OnPropertyChanged();
			}
		}

		public bool FinalKillCam
		{
			get { return _general.FinalKillCamEnabled; }
			set
			{
				_general.FinalKillCamEnabled = value;
				OnPropertyChanged();
			}
		}


		public int EarlyVictoryWinMode
		{
			get { return (_general.EarlyVictoryWinCount == 0) ? 0 : 1; }
			set
			{
				// TODO: Change the victory win count based on number of rounds
				OnPropertyChanged();
			}
		}

		public byte OvertimeDuration
		{
			get { return _general.OvertimeDuration; }
			set
			{
				_general.OvertimeDuration = value;
				OnPropertyChanged();
			}
		}
	}
}
