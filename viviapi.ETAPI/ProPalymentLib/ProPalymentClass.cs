using System.Collections.Generic;

namespace ProPalymentLib
{
    public class ProPalymentClass
    {
        private IProPalyment vnbPalyment;

        public ProPalymentClass(string MerCertPath, string MerCertPwd, string GatewayPublicKeyPath, string PostAdd)
        {
            this.vnbPalyment = (IProPalyment)new ProPalyment(MerCertPath, MerCertPwd, GatewayPublicKeyPath, PostAdd);
        }

        public Dictionary<string, string> SignMessageAndSendData2Gateway(Dictionary<string, string> dictionary)
        {
            return this.vnbPalyment.VNB_SignMessageAndSendData2Gateway(dictionary);
        }

        public Dictionary<string, string> VNB_ParseXML(string xmlbase64, string singmsg)
        {
            return this.vnbPalyment.VNB_ParseXML(xmlbase64, singmsg);
        }
    }
}
