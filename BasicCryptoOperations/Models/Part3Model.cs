using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicCryptoOperations.Crypto;
using BasicCryptoOperations.Crypto.DES;
using BasicCryptoOperations.Extensions;

namespace BasicCryptoOperations.Models
{
    class Part3Model
    {
        private int RC4BlockSize = 1024;

        

        public string DESIV { get; set; } = "A1B2C3D4E5F6A7B8";
        public void Encrypt(String path)
        {
            MyCrypto.Encrypt(path);
        }

        public void Decrypt(String path)
        {
            MyCrypto.Decrypt(path);
        }

        public void StartRC4(String path, String key)
        {
            var encoder = new RC4(key);

            try
            {
                using (var reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    using (var writer = new BinaryWriter(File.Open(path + "tmp", FileMode.OpenOrCreate)))
                    {
                        while (true)
                        {
                            var bytes = reader.ReadBytes(RC4BlockSize);
                            if (bytes.Length == 0)
                                break;

                            var result = encoder.Encode(bytes, bytes.Length);
                            writer.Write(result);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            File.Delete(path);
            File.Move(path + "tmp", path);
        }

        public void EncodeDES(string path, string key, string mode)
        {
            switch (mode)
            {
                case "CBC":
                {
                    var des = new DesCBC(key, DESIV);
                    des.Create();
                    des.EncryptFile(path, path + "tmp");
                    break;
                }
                case "CFB":
                {
                    var des = new DesCFB(key, DESIV);
                    des.Create();
                    des.EncryptFile(path, path + "tmp");
                    break;
                }
                case "ECB":
                {
                    var des = new Des(key);
                    des.Create();
                    des.EncryptFile(path, path + "tmp");
                    break;
                }
                case "OFB":
                {
                    var des = new DesOFB(key, DESIV);
                    des.Create();
                    des.EncryptFile(path, path + "tmp");
                    break;
                }
            }
            File.Delete(path);
            File.Move(path + "tmp", path);
        }

        public void DecodeDES(string path, string key, string mode)
        {
            switch (mode)
            {
                case "CBC":
                {
                    var des = new DesCBC(key, DESIV);
                    des.Create();
                    des.DecodeFile(path, path + "tmp");
                    break;
                }
                case "CFB":
                {
                    var des = new DesCFB(key, DESIV);
                    des.Create();
                    des.DecodeFile(path, path + "tmp");
                    break;
                }
                case "ECB":
                {
                    var des = new Des(key);
                    des.Create();
                    des.DecryptFile(path, path + "tmp");
                    break;
                }
                case "OFB":
                {
                    var des = new DesOFB(key, DESIV);
                    des.Create();
                    des.DecodeFile(path, path + "tmp");
                    break;
                }
            }

            File.Delete(path);
            File.Move(path + "tmp", path);
        }

        public void StartVernam(String filePatch, String VernamKey)
        {
            try
            {
                using (var reader = new BinaryReader(File.Open(filePatch, FileMode.Open)))
                {
                    using (var writer = new BinaryWriter(File.Open(filePatch + "tmp", FileMode.OpenOrCreate)))
                    {
                        var key = Encoding.Default.GetBytes(VernamKey);
                        var blockSize = key.Length;
                        while (true)
                        {
                            var bytes = reader.ReadBytes(blockSize);
                            if (bytes.Length == 0)
                                break;

                            var result = GetCipher(key, bytes);
                            writer.Write(result);
                        }
                    }
                }
            File.Delete(filePatch);
            File.Move(filePatch + "tmp", filePatch);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        private static byte[] GetCipher(byte[] key, byte[] data)
        {
            if (key.Length < data.Length)
            {
                throw new Exception("Small key");
            }

            var cipher = new byte[data.Length];


            for (var i = 0; i < data.Length; i++)
            {
                cipher[i] = (byte)(key[i] ^ data[i]);
            }

            return cipher;
        }
    }
}
