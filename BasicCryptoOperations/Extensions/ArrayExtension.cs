using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCryptoOperations.Extensions
{
    static class ArrayExtension
    {
        public static void Swap<T>(this T[] array, int index1, int index2)
        {
            var temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }

        public static byte[] Xor(this byte[] source, byte[] second)
        {
            if (source.Length != second.Length)
                throw new Exception("Not equal sizes");

            var result = new byte[second.Length];
            for (var i = 0; i < second.Length; i++)
            {
                result[i] = (byte)(source[i] ^ second[i]);
            }
            return result;
        }
    }
}
