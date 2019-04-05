using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BasicCryptoOperations.Crypto.DES
{
    public sealed class DESExec 
    {
        #region CipherExecutorBase class abstract methods implementation

        public CypherOperationResults Encrypt(string pathIn, byte[] key)
        {
            var pathOut = pathIn + ".tmp";

            {
                var des = new DES
                {
                    Key = key
                };
                using (var reader = new BinaryReader(new FileStream(pathIn, FileMode.Open, FileAccess.Read),
                    Encoding.Default))
                {
                    if (reader.BaseStream.Length == 0)
                    {
                        return 0;
                    }

                    var countOfBytesToDeleteAtDecryption = (int) (8 - reader.BaseStream.Length % 8) % 8;
                    var countOfBytesToDeleteAtDecryptionBytes =
                        BitConverter.GetBytes((ulong) countOfBytesToDeleteAtDecryption);
                    using (var writer = new BinaryWriter(new FileStream(pathOut, FileMode.Create, FileAccess.Write),
                        Encoding.Default))
                    {
                        writer.Write(des.Encrypt(countOfBytesToDeleteAtDecryptionBytes));
                        while (true)
                        {
                            var readBytes = reader.ReadBytes(128);
                            if (readBytes.Length == 0)
                            {
                                break;
                            }

                            if (readBytes.Length != 128)
                            {
                                Array.Resize(ref readBytes, readBytes.Length + countOfBytesToDeleteAtDecryption);
                            }

                            var blocks = Enumerable.Range(0, readBytes.Length / 8)
                                .Select((i) => readBytes.Skip(i * 8).Take(8).ToArray()).ToArray();
                            var encryptedBlocks = new byte[blocks.Length][];
                            Parallel.For(0, blocks.Length, (i) => { encryptedBlocks[i] = des.Encrypt(blocks[i]); });
                            foreach (var encryptedBlock in encryptedBlocks)
                            {
                                writer.Write(encryptedBlock);
                            }


                        }
                    }
                }

                    File.Delete(pathIn);
                    File.Move(pathOut, pathIn);
                    File.Delete(pathOut);
                

                return CypherOperationResults.OK;

            }
        }

        public CypherOperationResults Decrypt(string pathIn, byte[] key)
        {
                var pathOut = pathIn + ".tmp";

                var des = new DES()
                {
                    Key = key
                };
                using (var reader = new BinaryReader(new FileStream(pathIn, FileMode.Open, FileAccess.Read), Encoding.Default))
                {
                    if (reader.BaseStream.Length % 8 != 0 || reader.BaseStream.Length == 8)
                    {
                        return CypherOperationResults.FileIsCorrupted;
                    }
                    using (var writer = new BinaryWriter(new FileStream(pathOut, FileMode.Create, FileAccess.Write), Encoding.Default))
                    {
                        if (reader.BaseStream.Length == 0)
                        {
                            return CypherOperationResults.OK;
                        }
                        var countOfBytesToDelete = BitConverter.ToUInt64(des.Decrypt(reader.ReadBytes(8)), 0);
                        if (countOfBytesToDelete >= 8)
                        {
                            countOfBytesToDelete = 0;
                        }
                        while (true)
                        {
                            var readBytes = reader.ReadBytes(1024);
                            if (readBytes.Length == 0)
                            {
                                break;
                            }

                            var blocks = Enumerable.Range(0, readBytes.Length / 8).Select((i) => readBytes.Skip(i * 8).Take(8).ToArray()).ToArray();
                            var decryptedBlocks = new byte[blocks.Length][];
                            Parallel.For(0, blocks.Length, (i) =>
                            {
                                decryptedBlocks[i] = des.Decrypt(blocks[i]);
                            });
                            if (reader.BaseStream.Position == reader.BaseStream.Length && countOfBytesToDelete != 0)
                            {
                                Array.Resize(ref decryptedBlocks[decryptedBlocks.Length - 1], (int)(8 - countOfBytesToDelete));
                            }
                            foreach (var decryptedBlock in decryptedBlocks)
                            {
                                writer.Write(decryptedBlock);
                            }
                            
                        }
                    }
                }
                
                    File.Delete(pathIn);
                    File.Move(pathOut, pathIn);
                    File.Delete(pathOut);
                
                return CypherOperationResults.OK;
            
        }

        #endregion

    }

}
