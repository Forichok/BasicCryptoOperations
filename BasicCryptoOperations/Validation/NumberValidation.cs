﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BasicCryptoOperations.Validation
{
    class NumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int i;
            if (!Int32.TryParse(value.ToString(), out i))
            {
                return new ValidationResult(false, "Please enter a valid number");
            }

            return new ValidationResult(true, null);
        }
    }
}
