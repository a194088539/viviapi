using System.Collections.Generic;

namespace com.todaynic.ScpClient
{
    public class EmailClient : XmlClient
    {
        private string m_VCPID;
        private string m_VCPPassword;

        public EmailClient(string HostName, int HostPort, string VcpID, string VcpPwd)
          : base(HostName, HostPort)
        {
            this.m_VCPID = VcpID;
            this.m_VCPPassword = VcpPwd;
        }

        public Dictionary<string, string> createEmail(EmailInfo emailInfo)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "email:createEmail");
            this.m_XMLWriter.WriteElementString("email", "productid", (string)null, emailInfo.ProductID);
            this.m_XMLWriter.WriteElementString("email", "domain", (string)null, emailInfo.Domain);
            this.m_XMLWriter.WriteElementString("email", "period", (string)null, emailInfo.Period);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return reply.getDictionaryValue("/scp/response/resdata");
        }

        public EmailInfo getEmailInfo(string EmailID)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "email:infoEmail");
            this.m_XMLWriter.WriteElementString("email", "id", (string)null, EmailID);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return new EmailInfo()
            {
                Status = reply.getTextValue("/scp/response/resdata/active"),
                AttachVHostID = reply.getTextValue("/scp/response/resdata/vhost"),
                Space = reply.getTextValue("/scp/response/resdata/space"),
                MailServer = reply.getTextValue("/scp/response/resdata/mailserver"),
                Password = reply.getTextValue("/scp/response/resdata/pwd"),
                Domain = reply.getTextValue("/scp/response/resdata/domain"),
                DtCreate = reply.getTextValue("/scp/response/resdata/datecreate"),
                DtExpired = reply.getTextValue("/scp/response/resdata/dateexpired")
            };
        }

        public bool renewEmail(string EmailID, string Period, string dtExpired)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "email:renewEmail");
            this.m_XMLWriter.WriteElementString("email", "id", (string)null, EmailID);
            this.m_XMLWriter.WriteElementString("email", "dateexpired", (string)null, dtExpired);
            this.m_XMLWriter.WriteElementString("email", "period", (string)null, Period);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        private void writeSCPEnd()
        {
            this.m_XMLWriter.WriteEndDocument();
        }

        private void writeSCPStart()
        {
            this.m_XMLWriter.WriteStartElement("scp", "urn:scp:params:xml:ns:scp-3.0");
            this.m_XMLWriter.WriteAttributeString("xmlns", "email", (string)null, "urn:todaynic.com:email");
        }
    }
}
