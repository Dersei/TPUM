﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using TPUM.Client.Logic.DTO;

namespace TPUM.Client.GUI.Converters
{
    internal class GenresConverter : IValueConverter
    {

        private static string GenresToString(Genre genres)
        {
            return genres.ToString();
            //if (genres.Length == 0) return string.Empty;
            //string result = "";
            //if(genres.Length > 1)
            //{
            //    foreach (Genre genre in genres.SkipLast(1))
            //    {
            //        result += genre;
            //        result += ", ";
            //    }
            //}
            //result += genres.Last();
            //return result;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Genre genres)
            {
                return GenresToString(genres);
            }

            return "ERROR";
        }

        private Genre ParseGenres(string? genres)
        {
            if(genres is null) throw new NullReferenceException(nameof(genres));
            string[] genresArray = genres.Split(',', StringSplitOptions.RemoveEmptyEntries);
            Genre result = 0;
            foreach (string s in genresArray)
            {
                if (Enum.TryParse(s.Trim(), out Genre genre))
                {
                    result |= genre;
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ParseGenres(value as string);
        }
    }
}
