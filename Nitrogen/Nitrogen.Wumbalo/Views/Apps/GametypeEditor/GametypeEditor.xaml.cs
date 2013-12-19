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
using System.IO;

namespace Nitrogen.Wumbalo.Views.Apps.GametypeEditor
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

#if DEBUG
			ViewModel = new GameVariantViewModel(ContentFactory.ReadGameVariant(File.OpenRead("C:/users/matt/desktop/h4_rumble_tu.game")));
#else
			ViewModel = new GameVariantViewModel();
#endif
			DataContext = ViewModel;
		}
	}
}
