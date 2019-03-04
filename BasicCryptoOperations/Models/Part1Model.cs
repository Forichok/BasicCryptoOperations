﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace BasicCryptoOperations.Models
{
    class Part1Model : ViewModelBase
    {
        private String number;

        public char GetBitValue(int bitPosition)
        {
            try
            {
               return number[bitPosition-1];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ' ';
            }
        }

        public void SetNumber(String number)
        {
            this.number = number;
        }

        public String InverseBit(int bitPosition)
        {
            try
            {
                char c = number[bitPosition - 1];
                StringBuilder sb = new StringBuilder(number);


                if (c == '0')
                {
                    sb[bitPosition-1] = '1';
                    return sb.ToString();
                }
                else
                {
                    sb[bitPosition - 1] = '0';
                    return sb.ToString();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return number;
            }
        }

        public String ChangeBits(int i, int j)
        {
            try
            {
                StringBuilder sb = new StringBuilder(number);
                char c = number[j - 1];
                sb[j - 1] = sb[i-1];
                sb[i - 1] = c;
                return sb.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return number;
            }
            
            
        }

        public String ResetBits(int count)
        {
            StringBuilder sb = new StringBuilder(number);
            try
            {
                
                for (int k = sb.Length-1; count-- >0 ; k--)
                {
                    sb[k] = '0';
                }
                return sb.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return sb.ToString();
            }
        }

        public String ConcatinateBits(int count)
        {
            try
            {
                return number.Substring(0, count) + number.Substring(number.Length -count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return number;
            }
            
            
        }

        public String GetBitsFromMiddle(int count)
        {
            try
            {
                return number.Substring(count, number.Length - count - 3);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return number;
            }
        }

        public String SwapBytes(int i, int j)
        {
            try
            {
                while (number.Length < i * 4 || number.Length < j * 4)
                {
                   number = number.Insert(0, "0");
                }
                return String.Join("", Swap<String>(new List<string>(Split(number, 4)), i-1, j-1));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return number;
            }
        }
        private static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
        private static IEnumerable<T> Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }
    }
}
