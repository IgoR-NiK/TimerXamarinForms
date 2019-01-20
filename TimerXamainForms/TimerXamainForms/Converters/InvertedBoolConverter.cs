using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TimerXamainForms.Converters
{
    class InvertedBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value is bool ? !(bool)value : throw new InvalidOperationException("The target must be a boolean");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value is bool ? !(bool)value : throw new InvalidOperationException("The target must be a boolean");
		}
	}
}
