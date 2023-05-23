using System.Security.Cryptography;
using System.Text;

namespace viviapi.ETAPI.Mobaopay
{
    public sealed class MobaopaySignUtil
    {
        private static readonly MobaopaySignUtil instance = new MobaopaySignUtil();

        public static MobaopaySignUtil Instance
        {
            get
            {
                return MobaopaySignUtil.instance;
            }
        }

        private MobaopaySignUtil()
        {
        }

        public string sign(string sourceData)
        {
            return MobaopaySignUtil.GetbyteToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(sourceData + MobaopayConfig.Mbp_key)));
        }

        private static string GetbyteToString(byte[] data)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < data.Length; ++index)
                stringBuilder.Append(data[index].ToString("x2"));
            return ((object)stringBuilder).ToString();
        }

        public bool verifyData(string signData, string srcData)
        {
            return this.sign(srcData).ToUpper().Equals(signData.ToUpper());
        }
    }
}
