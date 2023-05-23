using System;
using System.Collections.Generic;

namespace com.todaynic.ScpClient
{
    public class ProductClient : XmlClient
    {
        private string m_VCPID;
        private string m_VCPPassword;

        public ProductClient(string HostName, int HostPort, string VcpID, string VcpPwd)
          : base(HostName, HostPort)
        {
            this.m_VCPID = VcpID;
            this.m_VCPPassword = VcpPwd;
        }

        public Dictionary<string, string> getProductList(int Category)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "product:getProductList");
            this.m_XMLWriter.WriteElementString("category", Category.ToString());
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            List<string> textListValue = reply.getTextListValue("/scp/response/resdata/product");
            for (int index = 0; index < textListValue.Count; ++index)
            {
                string[] strArray = textListValue[index].Split(new char[1]
                {
          ':'
                }, StringSplitOptions.None);
                dictionary.Add(strArray[0], strArray[strArray.Length - 1]);
            }
            return dictionary;
        }

        public Dictionary<string, string> getProductPrice(string IDProduct, string year, string quota)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "product:getProductPrice");
            this.m_XMLWriter.WriteElementString("productid", IDProduct);
            this.m_XMLWriter.WriteElementString("year", year);
            this.m_XMLWriter.WriteElementString("quota", quota);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return reply.getDictionaryValue("/scp/response/resdata");
        }

        private void writeSCPEnd()
        {
            this.m_XMLWriter.WriteEndDocument();
        }

        private void writeSCPStart()
        {
            this.m_XMLWriter.WriteStartElement("scp", "urn:scp:params:xml:ns:scp-3.0");
        }
    }
}
