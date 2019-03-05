using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCryptoOperations.Extensions
{
    public static class BitArrayExtension
    {
        public static IEnumerable<BitArray> Split(this BitArray bitArray, int chunkLength)
        {
            var list = new List<BitArray>();

            bool[] boolArray = new bool[chunkLength];
            var index = 0;
            for (int i = 0; i < bitArray.Length; i++)
            {
                boolArray[index++] = bitArray[i];
                if (index >= chunkLength)
                {
                    list.Add(new BitArray(boolArray));
                    boolArray = new bool[chunkLength];
                    index = 0;
                }
            }
            return list;
        }




        public static bool[] ToBoolArray(this BitArray bitArray)
        {
            var boolArray = bitArray.Cast<bool>().ToArray();
            return boolArray;
        }
        public static int ToInt(this BitArray bitArray)
        {
            var binaryOperand = string.Join("", bitArray.ToBoolArray().Select(Convert.ToInt32));
            binaryOperand = binaryOperand.Length > 9 ? binaryOperand : binaryOperand.PadLeft(9, '0');
            var result = Convert.ToInt32(binaryOperand, 2);
            return result;
        }
        public static string ToStr(this BitArray bitArray)
        {
            var result = string.Join("", bitArray.Cast<bool>().Select(Convert.ToInt32));
            return result;
        }
         
        
        
    }
}
