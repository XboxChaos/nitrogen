using System.Collections.Generic;
using System.ComponentModel;

namespace Nitrogen.Wumbalo.ViewModels
{
	public class Inpc : INotifyPropertyChanged
	{
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
