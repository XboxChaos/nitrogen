using Nitrogen.Enums;
using Nitrogen.GameVariants;
using Nitrogen.GameVariants.Base;
using Nitrogen.GameVariants.Megalo;
using Nitrogen.Metadata;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class TeamsSettingsViewModel
		: Inpc
	{
		//private TeamSettings _teams;
		private GameVariant _variant;
		private ObservableCollection<TeamViewModel> _teams;

		public TeamsSettingsViewModel (GameVariant variant)
		{
			_variant = variant;

			_teams = new ObservableCollection<TeamViewModel>();
			for ( int i = 0; i < 8; i++ )
			{
				_teams.Add(new TeamViewModel(_variant.TeamSettings.GetTeam(i), i));
			}
		}

		public TeamViewModel Team0 { get { return _teams[0]; } }
		public TeamViewModel Team1 { get { return _teams[1]; } }
		public TeamViewModel Team2 { get { return _teams[2]; } }
		public TeamViewModel Team3 { get { return _teams[3]; } }
		public TeamViewModel Team4 { get { return _teams[4]; } }
		public TeamViewModel Team5 { get { return _teams[5]; } }
		public TeamViewModel Team6 { get { return _teams[6]; } }
		public TeamViewModel Team7 { get { return _teams[7]; } }
	}
}