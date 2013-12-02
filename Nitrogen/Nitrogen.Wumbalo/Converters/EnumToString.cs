using System;
using System.Globalization;
using System.Windows.Data;
using Nitrogen.Wumbalo.ViewModels.Dialogs;

namespace Nitrogen.Wumbalo.Converters
{
	[ValueConversion(typeof(ModernMessageBoxButton), typeof(string))]
	public class EnumToString : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((ModernMessageBoxButton) value).ToString("G");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
