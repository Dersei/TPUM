using System;
using System.Globalization;
using System.Windows.Controls;
using TPUM.Data.Model;

namespace TPUM.GUI.Converters
{
    public class GenreValidation : ValidationRule
    {
        private bool ParseGenres(string? genres)
        {
            if (genres is null) throw new NullReferenceException(nameof(genres));
            string[] genresArray = genres.Split(',');
            foreach (string s in genresArray)
            {
                if (!Enum.TryParse(s.Trim(), out Genre _))
                {
                    return false;
                }
            }

            return true;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return ParseGenres(value as string) ? ValidationResult.ValidResult : new ValidationResult(false, "Incorrect genre");
        }
    }
}