using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Nitrogen.Wumbalo.Models.Home;
using Nitrogen.Wumbalo.ViewModels;

namespace Nitrogen.Wumbalo.Views
{
	/// <summary>
	/// Interaction logic for Home.xaml
	/// </summary>
	public partial class Home
	{
		public HomeViewModel ViewModel;

		public Home()
		{
			InitializeComponent();

			ViewModel = new HomeViewModel();
			DataContext = ViewModel;
			
			#region Hide Hipster Designer Stuff
			
			HeadersForBlendPanel.Visibility = Visibility.Collapsed;
			
			#endregion
		}

		#region Events

		void HeaderButton_OnClick(object sender, RoutedEventArgs e)
		{
			var button = sender as ToggleButton;
			if (button == null) return;

			var header = button.Tag as Header;
			if (header == null) return;

			ScrollToSection(header);
		}

		#endregion

		#region Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="header"></param>
		public void ScrollToSection(Header header)
		{
			var element = ContentScrollViewer.FindName(string.Format("{0}Grid", header.Tag)) as Grid;
			if (element == null) return;

			var transform = element.TransformToVisual(ContentScrollViewer);
			var positionInScrollViewer = ContentScrollViewer.HorizontalOffset + (transform.Transform(new Point(0, 0)).X - 19); // 19 px padding
			var newPercentageInScrollViewer = (positionInScrollViewer / ContentScrollViewer.ViewportWidth) * 100;
			var oldPercentageInScrollViewer = (ContentScrollViewer.HorizontalOffset / ContentScrollViewer.ViewportWidth) * 100;
			ContentScrollViewer.ScrollToHorizontalOffset(positionInScrollViewer);

			// TODO: Figure this animate to position shit out
			//// ReSharper disable once PossibleNullReferenceException
			//(SlideToStoryboard.Children[0] as DoubleAnimation).To = newPercentageInScrollViewer;
			//// ReSharper disable once PossibleNullReferenceException
			//(SlideToStoryboard.Children[0] as DoubleAnimation).From = oldPercentageInScrollViewer;
			//SlideToStoryboard.Begin();
			
			foreach (var viewHeader in ViewModel.Headers)
				viewHeader.IsSelected = false;
			ViewModel.Headers.First(h => h.Tag == header.Tag).IsSelected = true;
		}

		#endregion
	}
}
