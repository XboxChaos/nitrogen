using System.Windows;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Nitrogen.Wumbalo.Converters
{
	[ValueConversion(typeof (string), typeof (string))]
	public class CreateApplicationTitle : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return string.Format("{0} - {1}", Application.Current.Resources["AppTitle"], value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((string) value).Replace(string.Format("{0} - ", Application.Current.Resources["AppTitle"]), "");
		}
	}
}
