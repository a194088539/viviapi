using System.Collections.Generic;

namespace com.todaynic.ScpClient
{
    public class SCPReply
    {
        private XMLParser m_XMLParser;

        public ScpStatus Status
        {
            get
            {
                return this.getResultCode() == "2000" ? ScpStatus.Successfully : ScpStatus.Error;
            }
        }

        public SCPReply(string ReceiveXML)
        {
            this.m_XMLParser = new XMLParser(ReceiveXML);
        }

        public Dictionary<string, string> getDictionaryValue(string xPath)
        {
            return this.m_XMLParser.getDictionaryOfANode(xPath);
        }

        public string getResultCode()
        {
            return this.m_XMLParser.getAttributeTextOfNode("/scp/response/result", "code");
        }

        public string getResultMessage()
        {
            return this.m_XMLParser.getSingleNodeInnerText("/scp/response/result/msg");
        }

        public List<string> getTextListValue(string xPath)
        {
            return this.m_XMLParser.getListOfANode(xPath);
        }

        public string getTextValue(string xPath)
        {
            return this.m_XMLParser.getSingleNodeInnerText(xPath);
        }
    }
}
