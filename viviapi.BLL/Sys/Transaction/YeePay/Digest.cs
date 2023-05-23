using System.Text;

namespace viviapi.BLL.Sys.Transaction.YeePay
{
    public class Digest
    {
        public static string HmacSign(string aValue, string aKey)
        {
            byte[] data1 = new byte[64];
            byte[] data2 = new byte[64];
            byte[] bytes1 = Encoding.UTF8.GetBytes(aKey);
            byte[] bytes2 = Encoding.UTF8.GetBytes(aValue);
            for (int length = bytes1.Length; length < 64; ++length)
                data1[length] = (byte)54;
            for (int length = bytes1.Length; length < 64; ++length)
                data2[length] = (byte)92;
            for (int index = 0; index < bytes1.Length; ++index)
            {
                data1[index] = (byte)((uint)bytes1[index] ^ 54U);
                data2[index] = (byte)((uint)bytes1[index] ^ 92U);
            }
            HmacMD5 hmacMd5 = new HmacMD5();
            hmacMd5.update(data1, (uint)data1.Length);
            hmacMd5.update(bytes2, (uint)bytes2.Length);
            byte[] data3 = hmacMd5.finalize();
            hmacMd5.init();
            hmacMd5.update(data2, (uint)data2.Length);
            hmacMd5.update(data3, 16U);
            return Digest.toHex(hmacMd5.finalize());
        }

        public static string toHex(byte[] input)
        {
            if (input == null)
                return (string)null;
            StringBuilder stringBuilder = new StringBuilder(input.Length * 2);
            for (int index = 0; index < input.Length; ++index)
            {
                int num = (int)input[index] & (int)byte.MaxValue;
                if (num < 16)
                    stringBuilder.Append("0");
                stringBuilder.Append(num.ToString("x"));
            }
            return stringBuilder.ToString();
        }
    }
}
