using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BasicCryptoOperations.Validation
{
    class BitPositionValidation:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int i;
            if (!Int32.TryParse(value.ToString(), out i))
            {
                return new ValidationResult(false, "Please enter a valid bit position (1-32)");
            }

            if (i<1 || i >32)
                return new ValidationResult(false, "Please enter a valid bit position (1-32)");

            return new ValidationResult(true, null);
        }
    }
}
