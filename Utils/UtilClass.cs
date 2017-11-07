using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    public static class UtilClass
    {
        public static string XSSConvert(string oriHTML)
        {
            string convertHTML = oriHTML.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")
                                .Replace("\"", "&quot;").Replace("'", "&#x27;").Replace("/", "&#x2f;");
            return convertHTML;
        }
        public static string XSSDeconvert(string oriHTML)
        {
            string convertHTML = oriHTML.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">")
                                .Replace("&quot;", "\"").Replace("&#x27;", "'").Replace("&#x2f;", "/");
            return convertHTML;
        }
        public static string HashStr(string src)
        {
            var sha2 = SHA256.Create();
            byte[] hashByte = sha2.ComputeHash(Encoding.Unicode.GetBytes(src));
            string passHash = "";
            for (int i = 0; i < hashByte.Length; i++)
            {
                passHash += hashByte[i].ToString("x2");
            }
            return passHash;
        }
    }
}