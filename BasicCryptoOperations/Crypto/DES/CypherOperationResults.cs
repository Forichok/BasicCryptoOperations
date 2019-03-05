using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCryptoOperations.Crypto.DES
{
    public enum CypherOperationResults
    {
        OK
        , OperationCancelled
        , FileIsCorrupted
        , FileOccupiedByOtherProcess
        , OtherError
    }
}
