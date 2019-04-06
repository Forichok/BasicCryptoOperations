using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCryptoOperations.Crypto.DES
{
    public class DesCFB : Des
    {
        private string _iv;

        private string _lastEncrypted = "";

        public DesCFB(string key, string iv) : base(key)
        {
            _iv = iv;
        }

        public KeyValuePair<long, float> EncryptFile(string fromFile, string toFile)
        {
            FileInfo file = new FileInfo(fromFile);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            using (var reader = new FileStream(fromFile, FileMode.Open))
            {
                using (var writer = new BinaryWriter(File.Open(toFile, FileMode.OpenOrCreate)))
                {
                    Int64 hexIn;
                    String hex = "";

                    var counter = 0;
                    for (int i = 0; (hexIn = reader.ReadByte()) != -1; i++)
                    {
                        counter++;
                        hex += $"{hexIn:X2}";
                        if (counter == 8)
                        {
                            counter = 0;
                            CfbEncryptRound(hex);
                            writer.Write(StringToByteArray(CipherText));
                            hex = "";
                        }
                    }

                    if (counter != 8 && counter != 0)
                    {
                        for (var i = counter; i < 8; i++)
                            hex += $"{(byte)0:X2}";
                        CfbEncryptRound(hex);
                        writer.Write(StringToByteArray(CipherText));
                    }
                }
            }
            watch.Stop();
            return new KeyValuePair<long, float>(file.Length, watch.ElapsedMilliseconds );
        }

        private void CfbEncryptRound(string hex)
        {
            EncryptRound(_iv);

            var output = Convert.ToInt64(hex, 16) ^ Convert.ToInt64(CipherText, 16);

            _cipherText = $"{output:X16}";
            _iv = _cipherText;
        }

        public KeyValuePair<long, float> DecodeFile(string fromFile, string toFile)
        {
            FileInfo file = new FileInfo(fromFile);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            using (var reader = new FileStream(fromFile, FileMode.Open))
            {
                using (var writer = new BinaryWriter(File.Open(toFile, FileMode.OpenOrCreate)))
                {
                    Int64 hexIn;
                    String hex = "";

                    var counter = 0;
                    for (int i = 0; (hexIn = reader.ReadByte()) != -1; i++)
                    {
                        counter++;
                        hex += $"{hexIn:X2}";
                        if (counter == 8)
                        {
                            counter = 0;
                            CfbDecodeRound(hex);
                            writer.Write(StringToByteArray(DecryptText));
                            hex = "";
                        }
                    }

                    if (counter != 8 && counter != 0)
                    {
                        for (var i = counter; i < 8; i++)
                            hex += $"{(byte)0:X2}";
                        CfbDecodeRound(hex);
                        writer.Write(StringToByteArray(DecryptText));
                    }
                }
            }
            watch.Stop();
            return new KeyValuePair<long, float>(file.Length, watch.ElapsedMilliseconds );
        }

        private void CfbDecodeRound(string hex)
        {
            EncryptRound(_iv);

            var output = Convert.ToInt64(hex, 16) ^ Convert.ToInt64(CipherText, 16);

            _decryptedText = $"{output:X16}";
            _iv = hex;
        }
    }
}
