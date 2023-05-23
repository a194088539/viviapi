using System.Collections.Generic;

namespace ProPalymentLib
{
    public interface IProPalyment
    {
        Dictionary<string, string> VNB_ParseXML(string xmlbase64, string singmsg);

        Dictionary<string, string> VNB_SignMessageAndSendData2Gateway(Dictionary<string, string> dictionary);
    }
}
