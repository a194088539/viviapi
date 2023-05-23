using System.Collections.Generic;

namespace cn.eibei.xml
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

        public string getResultCode()
        {
            return this.m_XMLParser.getAttributeTextOfNode("/scp/response/result", "code");
        }

        public string getResultMessage()
        {
            return this.m_XMLParser.getNodesInnerText("/scp/response/result/msg");
        }

        public string getTextValue(string xPath)
        {
            return this.m_XMLParser.getSingleNodeInnerText(xPath);
        }

        public Dictionary<string, string> getDictionaryValue(string xPath)
        {
            return this.m_XMLParser.getDictionaryOfANode(xPath);
        }

        public List<string> getTextListValue(string xPath)
        {
            return this.m_XMLParser.getInnerTextList(xPath);
        }
    }
}
