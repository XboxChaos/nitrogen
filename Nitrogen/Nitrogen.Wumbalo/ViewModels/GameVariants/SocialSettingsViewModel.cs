using Nitrogen.Enums;
using Nitrogen.GameVariants;
using Nitrogen.GameVariants.Base;
using Nitrogen.GameVariants.Megalo;
using Nitrogen.Metadata;
using System;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class SocialSettingsViewModel
		: Inpc
	{
		private SocialSettings _social;
		private GameVariant _variant;

		public SocialSettingsViewModel (GameVariant variant)
		{
			_variant = variant;
			_social = _variant.SocialSettings;
		}

		public int TeamChanging
		{
			get { return (int) _social.TeamChanging; }
			set
			{
				_social.TeamChanging = (TeamChangingMode) value;
				OnPropertyChanged();
			}
		}

		public bool FriendlyFire
		{
			get { return _social.FriendlyFireEnabled; }
			set
			{
				_social.FriendlyFireEnabled = value;
				OnPropertyChanged();
			}
		}

		public bool BetrayalBooting
		{
			get { return _social.BetrayalBooting; }
			set
			{
				_social.BetrayalBooting = value;
				OnPropertyChanged();
			}
		}

		public bool EnemyVoice
		{
			get { return _social.EnemyVoice; }
			set
			{
				_social.EnemyVoice = value;
				OnPropertyChanged();
			}
		}

		public bool DeadPlayerVoice
		{
			get { return _social.DeadPlayerVoice; }
			set
			{
				_social.DeadPlayerVoice = value;
				OnPropertyChanged();
			}
		}

		public bool OpenVoiceChannel
		{
			get { return _social.OpenChannelVoice; }
			set
			{
				_social.OpenChannelVoice = value;
				OnPropertyChanged();
			}
		}
	}
}
