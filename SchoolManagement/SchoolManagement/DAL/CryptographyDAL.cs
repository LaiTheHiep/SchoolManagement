using System.Security.Cryptography;
using System.Text;

namespace SchoolManagement.DAL
{
    public static class CryptographyDAL
    {
        public static string EncryptMD5(string text)
        {
            string encrypt = null;
            byte[] btext = ASCIIEncoding.ASCII.GetBytes(text);    // text => byte array(btext)

            byte[] md5 = new MD5CryptoServiceProvider().ComputeHash(btext); // encrypt md5
            foreach (byte item in md5)
            {
                var _item = item % 17; // item / 17 => div = _item
                encrypt += _item;    // covert => string
            }

            return encrypt;
        }
    }
}