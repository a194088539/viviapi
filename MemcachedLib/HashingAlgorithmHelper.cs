using System.Text;

namespace MemcachedLib
{
    public class HashingAlgorithmHelper
    {
        public static int OriginalHashingAlgorithm(string key)
        {
            int num1 = 0;
            foreach (int num2 in key.ToCharArray())
                num1 = num1 * 33 + num2;
            return num1;
        }

        public static int NewHashingAlgorithm(string key)
        {
            CRCTool crcTool = new CRCTool();
            crcTool.Init(CRCTool.CRCCode.CRC32);
            return (int)crcTool.crctablefast(Encoding.UTF8.GetBytes(key)) >> 16 & (int)short.MaxValue;
        }
    }
}
