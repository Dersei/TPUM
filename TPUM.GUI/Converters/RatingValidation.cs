using System.Diagnostics;
using System.Globalization;
using System.Windows.Controls;

namespace TPUM.GUI.Converters
{
    public class RatingValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value is string s && decimal.TryParse(s, out decimal d) && d >= 0 && d <= 10) return ValidationResult.ValidResult;
            return new ValidationResult(false, "Bad value");
        }
    }
}