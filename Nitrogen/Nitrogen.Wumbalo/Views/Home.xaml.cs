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
	}
}
