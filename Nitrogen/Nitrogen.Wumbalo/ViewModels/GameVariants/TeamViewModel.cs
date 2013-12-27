using Nitrogen.Enums;
using Nitrogen.GameVariants;
using Nitrogen.GameVariants.Base;
using Nitrogen.GameVariants.Megalo;
using Nitrogen.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class TeamViewModel
		: Inpc
	{
		private static readonly string[] DefaultNames =
		{
			"Red Team",
			"Blue Team",
			"Gold Team",
			"Green Team",
			"Purple Team",
			"Lime Team",
			"Orange Team",
			"Cyan Team"
		};

		private static readonly SolidColorBrush[] DefaultColors =
		{
			new SolidColorBrush(Color.FromArgb(0xFF, 0x8D, 0x2A, 0x1B)),
			new SolidColorBrush(Color.FromArgb(0xFF, 0x28, 0x46, 0x7C)),
			new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xCA, 0x00)),
			new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x6C, 0x00)),
			new SolidColorBrush(Color.FromArgb(0xFF, 0x84, 0x3B, 0xD0)),
			new SolidColorBrush(Color.FromArgb(0xFF, 0x76, 0xBD, 0x12)),
			new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x7F, 0x24)),
			new SolidColorBrush(Color.FromArgb(0xFF, 0x1F, 0xA8, 0xCA))
		};

		private static readonly int[] DefaultEmblems =
		{
		};

		private static readonly EmblemColor[] DefaultEmblemColors =
		{

		};

		private Team _team;
		private int _index;
		private BitmapSource _emblem;

		public TeamViewModel (Team team, int index)
		{
			_team = team;
			_index = index;
		}

		public SolidColorBrush PrimaryColor
		{
			get
			{
				if ( _team.OverridePrimaryColor )
				{
					var teamColor = _team.PrimaryColor;
					return new SolidColorBrush(Color.FromArgb(teamColor.Alpha, teamColor.Red, teamColor.Green, teamColor.Blue));
				}
				return DefaultColors[_index];
			}
		}

		public string DisplayName
		{
			get
			{
				string name = ( _team.Name != null ) ? _team.Name.Get(Language.English) : null;
				if ( string.IsNullOrEmpty(name) )
					name = DefaultNames[_index];

				return name;
			}
		}
	}
}