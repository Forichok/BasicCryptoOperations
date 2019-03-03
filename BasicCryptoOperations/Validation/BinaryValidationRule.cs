using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BasicCryptoOperations.Validation
{
    public class BinaryValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value.ToString().ToLower().Replace("0", "").Replace("1", "").Length == 0)
                return new ValidationResult(true, null);

            return new ValidationResult(false, "Please enter a valid binary number");
        }
    }
}
