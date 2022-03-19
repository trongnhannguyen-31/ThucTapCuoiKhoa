using System;
using System.Globalization;
using Xamarin.Forms;

namespace Phoenix.Mobile.Converters
{
    public class DateTimeValueConverter : IValueConverter
    {
        public static DateTimeValueConverter Instance = new DateTimeValueConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "--/--/----";
            DateTime.TryParse(value.ToString(), out DateTime date);
            return date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
