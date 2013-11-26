using Nitrogen.Wumbalo.ViewModels;

namespace Nitrogen.Wumbalo
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private MainViewModel _viewModel = new MainViewModel();

		public MainWindow()
		{
			InitializeComponent();

			DataContext = _viewModel;
		}
	}
}