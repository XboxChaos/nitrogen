using System.Windows.Controls;
using System.Windows.Input;
using Nitrogen.Wumbalo.ViewModels;

namespace Nitrogen.Wumbalo.Views
{
	/// <summary>
	/// Interaction logic for Home.xaml
	/// </summary>
	public partial class Home
	{
		private HomeViewModel homeViewModel;

		public Home()
		{
			InitializeComponent();

			homeViewModel = new HomeViewModel();
			DataContext = homeViewModel;
		}

		private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			var scrollviewer = sender as ScrollViewer;
			if (scrollviewer == null) return;

			if (e.Delta > 0)
				scrollviewer.LineLeft();
			else
				scrollviewer.LineRight();
			e.Handled = true;
		}
	}
}
