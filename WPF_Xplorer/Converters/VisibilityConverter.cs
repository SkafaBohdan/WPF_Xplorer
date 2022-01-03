using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPF_Xplorer.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = value != null && (bool)value;

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility?)value;

            return visibility == Visibility.Visible;
        }
    }
}
