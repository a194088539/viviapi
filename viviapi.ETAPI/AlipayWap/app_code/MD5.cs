using System.Security.Cryptography;
using System.Text;

namespace viviapi.ETAPI.AlipayWap
{
    public sealed class AlipayMD5
    {
        public static string Sign(string prestr, string key, string _input_charset)
        {
            StringBuilder stringBuilder = new StringBuilder(32);
            prestr = prestr + key;
            foreach (byte num in new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(prestr)))
                stringBuilder.Append(num.ToString("x").PadLeft(2, '0'));
            return ((object)stringBuilder).ToString();
        }

        public static bool Verify(string prestr, string sign, string key, string _input_charset)
        {
            return AlipayMD5.Sign(prestr, key, _input_charset) == sign;
        }
    }
}
