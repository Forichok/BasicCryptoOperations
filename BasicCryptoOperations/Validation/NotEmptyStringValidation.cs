using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BasicCryptoOperations.Validation
{
    class NotEmptyStringValidation:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var key = value as String;
            if (string.IsNullOrEmpty(key))
            {
                return new ValidationResult(false, $"Input key");
            }
            return new ValidationResult(true, null);
        }
    }
}
