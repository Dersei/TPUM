using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TPUM.GUI.Converters
{
    public class VisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool visibility)
            {
                if (parameter is string s && s == "Invert") visibility = !visibility;
                return visibility ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility visibility && visibility == Visibility.Visible;
        }
    }
}