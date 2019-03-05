using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BasicCryptoOperations.Extensions
{
   static class BigIntegerExtension
    {
        public static string To16Notation(this BigInteger value)
        {
            if (value == BigInteger.Zero)
            {
                return "0";
            }
            if (value < BigInteger.Zero)
            {
                return string.Concat("-", To16Notation(BigInteger.Abs(value)));
            }
            var resultBuilder = new StringBuilder();
            while (value != BigInteger.Zero)
            {
                var digitValue = (int)(value % 16);
                value /= 16;
                var digit = (char)(digitValue < 10 ? digitValue + '0' : digitValue + 'A' - 10);
                resultBuilder = resultBuilder.Append(digit);
            }
            var result = resultBuilder.ToString().ToCharArray();
            Array.Reverse(result);
            return new string(result);
        }

        public static BigInteger From16Notation(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (value[0] == '-')
            {
                return BigInteger.MinusOne * From16Notation(value.Substring(1));
            }
            var result = BigInteger.Zero;
            foreach (var c in value)
            {
                result = result * 16 + (char.IsDigit(c) ? c - '0' : c - 'A' + 10);
            }
            return result;
        }

        public static byte[] ToByteArray(this BigInteger value, int bytesCount)
        {
            var result = value.ToByteArray();
            Array.Resize(ref result, bytesCount);
            return result;
        }
    }
}
