using System.Text;

namespace LuxuryAPIv3.Models.Utils
{
    public class SHA256
    {
        public static string ToSHA256(string s)
        {
            string hash = string.Empty;

            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
                foreach (byte b in hashValue)
                    hash += $"{b:X2}";
            }

            return hash;
        }
    }
}