using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicCryptoOperations.Extensions;

namespace BasicCryptoOperations.Crypto
{
    static class MyCrypto
    {
        private static Dictionary<String, bool[]> encryptTable;
        private static Dictionary<String, bool[]> decryptTable;

        private static int[] nums = { 12,15,10,4,3,7,1,0,9,8,6,5,14,2,13,11};
        static MyCrypto()
        {
            encryptTable = new Dictionary<String, bool[]>();
            decryptTable = new Dictionary<String, bool[]>();
            for (int i = 0; i < 16; i++)
            {
                encryptTable.Add(GetBitArray(i, 4).ToStr(), GetBitArray(nums[i], 4).ToBoolArray());
                decryptTable.Add(GetBitArray(nums[i], 4).ToStr(), GetBitArray(i, 4).ToBoolArray());
            }
        }


        public static void Encrypt(String path)
        {
            using (var reader = new BinaryReader(new FileStream(path, FileMode.Open)))
            {
                using (var writer = new BinaryWriter(new FileStream(path + ".tmp", FileMode.OpenOrCreate)))
                {
                    var i = 1;
                    while (true)
                    {
                        try
                        {
                            i = reader.ReadInt32();
                            var bitArray = new BitArray(new BitArray(BitConverter.GetBytes(i)));
                            var a = bitArray.Split(4);
                            var c = Encrypt(a);
                            writer.Write(BoolArrayToInt(c.ToArray()));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            break;
                        }
                    }
                }
            }
            File.Delete(path);
            File.Move(path + ".tmp", path);
        }

        

        public static void Decrypt(String path)
        {
            using (var reader = new BinaryReader(new FileStream(path, FileMode.Open)))
            {
                using (var writer = new BinaryWriter(new FileStream(path + ".tmp", FileMode.OpenOrCreate)))
                {
                    var i = 1;
                    while (true)
                    {
                        try
                        {
                            i = reader.ReadInt32();
                            var bitArray = new BitArray(new BitArray(BitConverter.GetBytes(i)));
                            var a = bitArray.Split(4);
                            var c = Decrypt(a);
                            writer.Write(BoolArrayToInt(c.ToArray()));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            break;
                        }
                    }
                }
            }
            File.Delete(path);
            File.Move(path+".tmp",path);

        }


        public static BitArray GetBitArray(int number, int lenght)
        {
            var bitArray = new BitArray(new[] { number });
            bitArray.Length = lenght;
            bitArray = new BitArray(bitArray.ToBoolArray().Reverse().ToArray());
            return bitArray;
        }

        static List<bool> Encrypt(IEnumerable<BitArray> data)
        {
            var list = new List<bool>();
            
            foreach (var element in data)
            {
                list.AddRange(encryptTable[element.ToStr()]);
            }

            return list;
        }

        static List<bool> Decrypt(IEnumerable<BitArray> data)
        {
            var list = new List<bool>();

            foreach (var element in data)
            {
                list.AddRange(decryptTable[element.ToStr()]);
            }

            return list;
        }



        static int BoolArrayToInt(bool[] arr)
        {
            if (arr.Length > 32)
                throw new ApplicationException("too many elements to be converted to a single int");
            int val = 0;
            for (int i = 0; i < arr.Length; ++i)
                if (arr[i]) val |= 1 << i;
            return val;
        }

        static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int parts)
        {
            return list.Select((item, index) => new { index, item })
                .GroupBy(x => (x.index + 1) / (list.Count() / parts) + 1)
                .Select(x => x.Select(y => y.item));
        }
    }
}
