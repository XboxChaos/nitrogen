using System;
using System.Windows;
using System.Windows.Data;

namespace Nitrogen.Wumbalo.Converters
{
	[ValueConversion(typeof(bool), typeof(bool))]
	public class InverseBooleanToVisibilityConverter
		: IValueConverter
	{
		#region IValueConverter Members

		public object Convert (object value, Type targetType, object parameter,
			System.Globalization.CultureInfo culture)
		{
			return !(bool) value;
		}

		public object ConvertBack (object value, Type targetType, object parameter,
			System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}