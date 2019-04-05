using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BasicCryptoOperations.Validation
{
    class DesKeyValidation:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var key = value as String;
            if (!IsHex(key))
            {
                return new ValidationResult(false, $"Please enter a valid Hex key");
            }

            if (key.Length != 16)
            {
                return new ValidationResult(false, $"Key length: {key.Length}/16");
            }

            return new ValidationResult(true, null);

        }

        public bool IsHex(string str)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(str, @"\A\b[0-9a-fA-F]+\b\Z");
        }
    }
}
