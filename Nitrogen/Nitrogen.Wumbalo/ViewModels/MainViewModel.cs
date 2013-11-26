using System.Collections.Generic;
using System.ComponentModel;

namespace Nitrogen.Wumbalo.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
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

		#region Interface

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetField<T>(ref T field, T value, string propertyName)
		{
			if (EqualityComparer<T>.Default.Equals(field, value))
				return false;

			field = value;
			OnPropertyChanged(propertyName);

			return true;
		}

		#endregion
	}
}
