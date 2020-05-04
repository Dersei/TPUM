using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using TPUM.Data.Model;

namespace TPUM.GUI
{
    internal class GenresConverter : IValueConverter
    {

        private static string GenresToString(Genre[] genres)
        {
            string result = "";
            if(genres.Length > 1)
            {
                foreach (Genre genre in genres.SkipLast(1))
                {
                    result += genre;
                    result += ", ";
                }
            }
            result += genres.Last();
            return result;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Genre[] genres)
            {
                return GenresToString(genres);
            }

            return "ERROR";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Genre[0];
        }
    }
}
