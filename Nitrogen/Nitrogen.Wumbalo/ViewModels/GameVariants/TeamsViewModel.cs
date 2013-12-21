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
	public class TeamsViewModel
		: Inpc
	{
		private TeamSettings _teams;
		private GameVariant _variant;

		public TeamsViewModel (GameVariant variant)
		{
			_variant = variant;
			_teams = _variant.TeamSettings;
		}

		public SolidColorBrush Team0Color
		{
			get
			{
				if ( _teams.GetTeam(0).OverridePrimaryColor )
				{
					var teamColor = _teams.GetTeam(0).PrimaryColor;
					return new SolidColorBrush(Color.FromArgb(teamColor.Alpha, teamColor.Red, teamColor.Green, teamColor.Blue));
				}
				return new SolidColorBrush(Color.FromArgb(0xFF, 0x8D, 0x2A, 0x1B));
			}
		}

		public SolidColorBrush Team1Color
		{
			get
			{
				if ( _teams.GetTeam(1).OverridePrimaryColor )
				{
					var teamColor = _teams.GetTeam(1).PrimaryColor;
					return new SolidColorBrush(Color.FromArgb(teamColor.Alpha, teamColor.Red, teamColor.Green, teamColor.Blue));
				}
				return new SolidColorBrush(Color.FromArgb(0xFF, 0x28, 0x46, 0x7C));
			}
		}

		public SolidColorBrush Team2Color
		{
			get
			{
				if ( _teams.GetTeam(2).OverridePrimaryColor )
				{
					var teamColor = _teams.GetTeam(2).PrimaryColor;
					return new SolidColorBrush(Color.FromArgb(teamColor.Alpha, teamColor.Red, teamColor.Green, teamColor.Blue));
				}
				return new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xCA, 0x00));
			}
		}

		public SolidColorBrush Team3Color
		{
			get
			{
				if ( _teams.GetTeam(3).OverridePrimaryColor )
				{
					var teamColor = _teams.GetTeam(3).PrimaryColor;
					return new SolidColorBrush(Color.FromArgb(teamColor.Alpha, teamColor.Red, teamColor.Green, teamColor.Blue));
				}
				return new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x6C, 0x00));
			}
		}

		public SolidColorBrush Team4Color
		{
			get
			{
				if ( _teams.GetTeam(4).OverridePrimaryColor )
				{
					var teamColor = _teams.GetTeam(4).PrimaryColor;
					return new SolidColorBrush(Color.FromArgb(teamColor.Alpha, teamColor.Red, teamColor.Green, teamColor.Blue));
				}
				return new SolidColorBrush(Color.FromArgb(0xFF, 0x84, 0x3B, 0xD0));
			}
		}

		public SolidColorBrush Team5Color
		{
			get
			{
				if ( _teams.GetTeam(5).OverridePrimaryColor )
				{
					var teamColor = _teams.GetTeam(5).PrimaryColor;
					return new SolidColorBrush(Color.FromArgb(teamColor.Alpha, teamColor.Red, teamColor.Green, teamColor.Blue));
				}
				return new SolidColorBrush(Color.FromArgb(0xFF, 0x76, 0xBD, 0x12));
			}
		}

		public SolidColorBrush Team6Color
		{
			get
			{
				if ( _teams.GetTeam(6).OverridePrimaryColor )
				{
					var teamColor = _teams.GetTeam(6).PrimaryColor;
					return new SolidColorBrush(Color.FromArgb(teamColor.Alpha, teamColor.Red, teamColor.Green, teamColor.Blue));
				}
				return new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x7F, 0x24));
			}
		}

		public SolidColorBrush Team7Color
		{
			get
			{
				if ( _teams.GetTeam(7).OverridePrimaryColor )
				{
					var teamColor = _teams.GetTeam(7).PrimaryColor;
					return new SolidColorBrush(Color.FromArgb(teamColor.Alpha, teamColor.Red, teamColor.Green, teamColor.Blue));
				}
				return new SolidColorBrush(Color.FromArgb(0xFF, 0x1F, 0xA8, 0xCA));
			}
		}

		public string Team0DisplayName
		{
			get
			{
				string name = ( _teams.GetTeam(0).Name != null ) ? _teams.GetTeam(0).Name.Get(Language.English) : null;
				if ( string.IsNullOrEmpty(name) )
					name = "Red Team";

				return name;
			}
		}

		public string Team1DisplayName
		{
			get
			{
				string name = ( _teams.GetTeam(1).Name != null ) ? _teams.GetTeam(1).Name.Get(Language.English) : null;
				if ( string.IsNullOrEmpty(name) )
					name = "Blue Team";

				return name;
			}
		}

		public string Team2DisplayName
		{
			get
			{
				string name = ( _teams.GetTeam(2).Name != null ) ? _teams.GetTeam(2).Name.Get(Language.English) : null;
				if ( string.IsNullOrEmpty(name) )
					name = "Gold Team";

				return name;
			}
		}

		public string Team3DisplayName
		{
			get
			{
				string name = ( _teams.GetTeam(3).Name != null ) ? _teams.GetTeam(3).Name.Get(Language.English) : null;
				if ( string.IsNullOrEmpty(name) )
					name = "Green Team";

				return name;
			}
		}

		public string Team4DisplayName
		{
			get
			{
				string name = ( _teams.GetTeam(4).Name != null ) ? _teams.GetTeam(4).Name.Get(Language.English) : null;
				if ( string.IsNullOrEmpty(name) )
					name = "Purple Team";

				return name;
			}
		}

		public string Team5DisplayName
		{
			get
			{
				string name = ( _teams.GetTeam(5).Name != null ) ? _teams.GetTeam(5).Name.Get(Language.English) : null;
				if ( string.IsNullOrEmpty(name) )
					name = "Lime Team";

				return name;
			}
		}

		public string Team6DisplayName
		{
			get
			{
				string name = (_teams.GetTeam(6).Name != null) ? _teams.GetTeam(6).Name.Get(Language.English) : null;
				if ( string.IsNullOrEmpty(name) )
					name = "Orange Team";

				return name;
			}
		}

		public string Team7DisplayName
		{
			get
			{
				string name = ( _teams.GetTeam(7).Name != null ) ? _teams.GetTeam(7).Name.Get(Language.English) : null;
				if ( string.IsNullOrEmpty(name) )
					name = "Cyan Team";

				return name;
			}
		}
	}
}