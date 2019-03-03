using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace BasicCryptoOperations.Models
{
    class Part1Model : ViewModelBase
    {

        public char GetBitValue(String number, int bitPosition)
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

        public String InverseBit(String number, int bitPosition)
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

        public String ChangeBits(String number, int i, int j)
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

        public String ResetBits(String number, int count)
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

        public String ConcatinateBits(String number, int count)
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

        public String GetBitsFromMiddle(String number, int count)
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
    }
}
