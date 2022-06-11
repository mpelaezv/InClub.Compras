using System.Security.Cryptography;
using System.Text;

namespace Usuario.Infrastructure.Security
{
    public class Auth
    {
        public static byte[] hashPass(string ParamPass)
        {
            byte[] hashedDataBytes;
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();
            hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(ParamPass));
            return hashedDataBytes;
        }
    }
}
