using System;
using System.Globalization;
using System.Windows.Data;

namespace Nitrogen.Wumbalo.Converters
{
	[ValueConversion(typeof(string), typeof(string))]
	public class StringNameConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var info = new CultureInfo("en-US", false).TextInfo;
			return info.ToTitleCase(( value as string ).Replace('_', ' '));
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return ( value as string ).ToLowerInvariant().Replace(' ', '_');
		}

		#endregion
	}
}
