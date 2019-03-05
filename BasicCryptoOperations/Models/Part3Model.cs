using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicCryptoOperations.Crypto;

namespace BasicCryptoOperations.Models
{
    class Part3Model
    {
        public void Encrypt(String path)
        {
            MyCrypto.Encrypt(path);
        }

        public void Decrypt(String path)
        {
            MyCrypto.Decrypt(path);
        }
    }
}
