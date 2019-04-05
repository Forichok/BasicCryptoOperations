using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace BasicCryptoOperations.Models
{
    class Part2Model:ViewModelBase
    {
        private String number;
        private String binaryNumber;
        public String GetMaxDivider()
        {
            int.TryParse(number, out var res);

            if (res == 0)
                return "∞";

            var maxPower = (int) Math.Log(res & -res, 2);

            return maxPower.ToString();
        }
        

        public String LeftShift(int offsetindex)
        {

            var isNum = int.TryParse(number, out var num);

            var nearestDegree = (int) Math.Log(num, 2);
            var bitCount = nearestDegree + 1;

            var maxNum = (int) Math.Pow(2, bitCount) - 1;

            var realOffset = offsetindex - offsetindex / bitCount * bitCount;

            var res = 0;
            if (isNum)
                res = ((num << realOffset) & (maxNum)) | (num >> (bitCount - realOffset));

            return Convert.ToString(res, 2).PadLeft(binaryNumber.Length,'0');
        }


        public String RightShift(int offsetIndex)
        {
            var isNum = int.TryParse(number, out var num);

            var nearestDegree = (int) Math.Log(num, 2);
            var bitCount = nearestDegree + 1;

            var maxNum = (int) Math.Pow(2, bitCount) - 1;

            var realOffset = offsetIndex - offsetIndex / bitCount * bitCount;

            var res = 0;
            if (isNum)
                res = (num >> realOffset) | ((num << (bitCount - realOffset)) & (maxNum));

            return Convert.ToString(res, 2).PadRight(binaryNumber.Length, '0');
        }

        public String Limits()
        {
            int.TryParse(number, out var res);

            if (res == 0)
                return "∞";

            var nearestDegree = (int) Math.Log(res, 2);

            return $"2^{nearestDegree} <= x <= 2^{nearestDegree + 1}";
        }

        public bool XorItself()
        {
            try
            {

            var boolList = FromString(binaryNumber);


            if (boolList.Count == 1)
            {
                return boolList[0];
            }
            else if (boolList.Count == 2)
            {
                return boolList[0] ^ boolList[1];
            }
            else if (boolList.Count != 0)
            {
                var current = false;
                for (var i = 0; i < boolList.Count; i++)
                {
                    if (i == 0)
                    {
                        current = boolList[i] ^ boolList[i + 1];
                        i++;
                    }
                    else
                    {
                        current = current ^ boolList[i];
                    }

                }

                return current;
            }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            return false;
        }


        private static List<bool> FromString(string str)
        {
            var boolList = new List<bool>();

            foreach (var ch in str)
            {
                boolList.Add(ch == '1');
            }

            return boolList;
        }

        public void SetBinaryNumber(String binaryNumber)
        {
            this.binaryNumber = binaryNumber;
        }

        public void SetNumber(String number)
        {
            this.number = number;
        }

        public String Swap(String swapRules)
        {
            var sb = new StringBuilder();

            try
            {
                var permutArr = Array.ConvertAll(swapRules.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);


                if (permutArr.Length == binaryNumber.Length)
                {
                    sb.Append(Swap(binaryNumber, permutArr));
                }
                else
                {
                    sb.Append("#Error: In permute. input");
                }

            }
            catch (Exception)
            {
                sb.Append("#Error: Empty input");
            }

            return sb.ToString();
        }

        private static String Swap(String binaryNumber, int[] permutArr)
        {
            var result = new StringBuilder();

            foreach (var i in permutArr)
            {
                var posInSource = binaryNumber.Length - i - 1;
                result.Append(binaryNumber[posInSource]);
            }

            return result.ToString();
        }
    }
}
