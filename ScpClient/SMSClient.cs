using System.Collections.Generic;

namespace com.todaynic.ScpClient
{
    public class SMSClient : XmlClient
    {
        private string m_SMSPassword;
        private string m_SMSUser;

        public string SMSUser
        {
            get
            {
                return this.m_SMSUser;
            }
            set
            {
                this.m_SMSUser = value;
            }
        }

        public SMSClient(string HostName, int HostProt, string SMSUser, string SMSPassword)
          : base(HostName, HostProt)
        {
            this.m_SMSUser = SMSUser;
            this.m_SMSPassword = SMSPassword;
        }

        public string getBalance()
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "SMS:infoSMSAccount");
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_SMSUser, this.m_SMSPassword, UserType.smsuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return reply.getTextValue("/scp/response/resdata/smsaccount");
        }

        public Dictionary<string, string> receiveSMS()
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "SMS:readSMS");
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_SMSUser, this.m_SMSPassword, UserType.smsuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            dictionary.Add("mess", reply.getTextValue("/scp/response/resdata/msg"));
            dictionary.Add("id", reply.getTextValue("/scp/response/resdata/id"));
            dictionary.Add("src", reply.getTextValue("/scp/response/resdata/src"));
            dictionary.Add("time", reply.getTextValue("/scp/response/resdata/time"));
            dictionary.Add("message", Utility.getBase64ToString(reply.getTextValue("/scp/response/resdata/message")));
            dictionary.Add("err", reply.getTextValue("/scp/response/resdata/err"));
            return dictionary;
        }

        public bool sendSMS(string MobilePhone, string Msg, string type)
        {
            this.writeSCPStart();
            string str = string.Empty;
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "SMS:sendSMS");
            this.m_XMLWriter.WriteElementString("sms", "mobile", (string)null, MobilePhone);
            this.m_XMLWriter.WriteElementString("sms", "message", (string)null, Utility.getStringToBase64(Msg));
            this.m_XMLWriter.WriteElementString("sms", "datetime", (string)null, "");
            this.m_XMLWriter.WriteElementString("sms", "smstype", (string)null, "");
            this.m_XMLWriter.WriteElementString("sms", "smsabout", (string)null, "");
            this.m_XMLWriter.WriteElementString("sms", "sender", (string)null, str);
            this.m_XMLWriter.WriteElementString("sms", "apitype", (string)null, type);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_SMSUser, this.m_SMSPassword, UserType.smsuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public bool sendSMS(string MobilePhone, string Msg, string sendTime, string type)
        {
            this.writeSCPStart();
            string str = string.Empty;
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "SMS:sendSMS");
            this.m_XMLWriter.WriteElementString("sms", "mobile", (string)null, MobilePhone);
            this.m_XMLWriter.WriteElementString("sms", "message", (string)null, Utility.getBase64ToString(Msg));
            this.m_XMLWriter.WriteElementString("sms", "datetime", (string)null, sendTime);
            this.m_XMLWriter.WriteElementString("sms", "smstype", (string)null, "");
            this.m_XMLWriter.WriteElementString("sms", "smsabout", (string)null, "");
            this.m_XMLWriter.WriteElementString("sms", "sender", (string)null, str);
            this.m_XMLWriter.WriteElementString("sms", "apitype", (string)null, type);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_SMSUser, this.m_SMSPassword, UserType.smsuser);
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
            this.m_XMLWriter.WriteAttributeString("xmlns", "user", (string)null, "urn:mobile:user");
            this.m_XMLWriter.WriteAttributeString("xmlns", "sms", (string)null, "urn:mobile:sms");
        }
    }
}
