using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Nitrogen.Wumbalo.Models.Home;
using Nitrogen.Wumbalo.ViewModels;
using Nitrogen.Wumbalo.Modern.Dialogs;
using Nitrogen.Wumbalo.ViewModels.Dialogs;
using Nitrogen.Wumbalo.ViewModels.GameVariants;

namespace Nitrogen.Wumbalo.Views.Apps
{
	/// <summary>
	/// Interaction logic for GametypeEditor.xaml
	/// </summary>
	public partial class GametypeEditor : UserControl
	{
		public GameVariantViewModel ViewModel;

		public GametypeEditor ()
		{
			InitializeComponent();

			ViewModel = new GameVariantViewModel();
			DataContext = ViewModel;
		}
	}
}
