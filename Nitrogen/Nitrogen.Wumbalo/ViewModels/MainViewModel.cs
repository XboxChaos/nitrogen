namespace Nitrogen.Wumbalo.ViewModels
{
	public class MainViewModel : Inpc
	{
		/// <summary>
		/// Initalizes the Main Window View Model.
		/// </summary>
		public MainViewModel() { }

		/// <summary>
		/// Initalizes the Main Window View Model.
		/// </summary>
		/// <param name="pageTitle">Sets the default startup page title.</param>
		public MainViewModel(string pageTitle)
		{
			PageTitle = pageTitle;
		}

		/// <summary>
		/// The title of the current page (App Title - Page Title).
		/// </summary>
		public string PageTitle
		{
			get { return _pageTitle; }
			set { SetField(ref _pageTitle, value, "PageTitle"); }
		}
		private string _pageTitle = "Welcome!";
	}
}
