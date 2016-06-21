using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TIC.PuppyLove.Helpers
{
    /// <summary>
    /// Handle hashing for application.  Ref url:
    /// https://dotnetcodr.com/2013/10/28/hashing-algorithms-and-their-practical-usage-in-net-part-1/
    /// </summary>
    public class CryptoHelper
    {
        public byte[] CreateHashSha512 (string userEnteredPw)
        {
            //userEnteredPw = userEnteredPw.Trim();
            SHA512Managed sha512 = new SHA512Managed();

            return sha512.ComputeHash(Encoding.UTF8.GetBytes(userEnteredPw));
        }

        public bool IsPasswordCorrect (string userEnteredPw, byte[] dbPwHash)
        {
            byte[] userEnteredPwHash = CreateHashSha512(userEnteredPw);

            return (BitConverter.ToString(userEnteredPwHash) == BitConverter.ToString(dbPwHash));
        }
    }
}
