using System;

namespace com.todaynic.ScpClient
{
    public class DomainClient : XmlClient
    {
        private string m_VCPID;
        private string m_VCPPassword;

        public DomainClient(string HostName, int HostPort, string VcpID, string VcpPwd)
          : base(HostName, HostPort)
        {
            this.m_VCPID = VcpID;
            this.m_VCPPassword = VcpPwd;
        }

        public string createContact(string Domain, ContactInfo Contact)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "dot" + this.getTopDomain(Domain) + ":createContact");
            this.writeContactMessage(ContactType.None, Contact);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return reply.getTextValue("/scp/response/resdata/contact:id");
        }

        public DomainInfo createDomain(DomainInfo domainInfo)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            string topDomain = this.getTopDomain(domainInfo.Name);
            if (topDomain.Trim().Length <= 0)
                throw new Exception("domain name error");
            this.m_XMLWriter.WriteElementString("action", "dot" + topDomain + ":createDomain");
            this.m_XMLWriter.WriteElementString("domain", "name", (string)null, domainInfo.Name);
            this.m_XMLWriter.WriteElementString("domain", "period", (string)null, domainInfo.Period);
            this.m_XMLWriter.WriteElementString("domain", "rloginpassword", (string)null, domainInfo.RLoginPassword);
            this.writeContactMessage(ContactType.registrant, domainInfo.RegistrantContactInfo);
            this.writeContactMessage(ContactType.admin, domainInfo.AdminContactInfo);
            this.writeContactMessage(ContactType.tech, domainInfo.TechContactInfo);
            this.writeContactMessage(ContactType.billing, domainInfo.BillingContactInfo);
            this.m_XMLWriter.WriteElementString("domain", "ns", (string)null, domainInfo.NS1);
            this.m_XMLWriter.WriteElementString("domain", "ns", (string)null, domainInfo.NS2);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            DomainInfo domainInfo1 = new DomainInfo()
            {
                Name = reply.getTextValue("/scp/response/resdata/domain:name"),
                ROID = reply.getTextValue("/scp/response/resdata/domain:roid"),
                RLoginPassword = reply.getTextValue("/scp/response/resdata/domain:RLoginPassword"),
                RegistrantContactID = reply.getTextValue("/scp/response/resdata/domain:contact[attribute::type=\"registrant\"]"),
                AdminContactID = reply.getTextValue("/scp/response/resdata/domain:contact[attribute::type=\"admin\"]"),
                TechContactID = reply.getTextValue("/scp/response/resdata/domain:contact[attribute::type=\"tech\"]"),
                BillingContactID = reply.getTextValue("/scp/response/resdata/domain:contact[attribute::type=\"billing\"]"),
                NS1 = reply.getTextValue("/scp/response/resdata/domain:ns1"),
                NS2 = reply.getTextValue("/scp/response/resdata/domain:ns2"),
                DtCreate = reply.getTextValue("/scp/response/resdata/domain:dtCreate"),
                DtUpdate = reply.getTextValue("/scp/response/resdata/domain:dtUpdate"),
                DtExpired = reply.getTextValue("/scp/response/resdata/domain:dtExpired")
            };
            this.resoveDomain(domainInfo1.Name);
            return domainInfo1;
        }

        public ContactInfo getContactInfo(string Domain, string ContactID)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "dot" + this.getTopDomain(Domain) + ":infoContact");
            this.m_XMLWriter.WriteElementString("domain", "name", (string)null, Domain);
            this.m_XMLWriter.WriteElementString("contact", "id", (string)null, ContactID);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return new ContactInfo()
            {
                street = reply.getTextValue("/scp/response/resdata/Street"),
                street1 = reply.getTextValue("/scp/response/resdata/Street1"),
                mobile = reply.getTextValue("/scp/response/resdata/Mobile"),
                org = reply.getTextValue("/scp/response/resdata/Organization"),
                cnorg = reply.getTextValue("/scp/response/resdata/Organization_GB"),
                pc = reply.getTextValue("/scp/response/resdata/PC"),
                sp = reply.getTextValue("/scp/response/resdata/SP"),
                voice = reply.getTextValue("/scp/response/resdata/Voice"),
                cc = reply.getTextValue("/scp/response/resdata/CC"),
                city = reply.getTextValue("/scp/response/resdata/City"),
                fax = reply.getTextValue("/scp/response/resdata/Fax"),
                name = reply.getTextValue("/scp/response/resdata/Name"),
                cnname = reply.getTextValue("/scp/response/resdata/Name_GB"),
                email = reply.getTextValue("/scp/response/resdata/Email")
            };
        }

        public DomainInfo getDomainInfo(string Domain)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "dot" + this.getTopDomain(Domain) + ":infoDomain");
            this.m_XMLWriter.WriteElementString("domain", "name", (string)null, Domain);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return new DomainInfo()
            {
                Status = reply.getTextValue("/scp/response/resdata/domain:status"),
                Name = reply.getTextValue("/scp/response/resdata/domain:name"),
                ROID = reply.getTextValue("/scp/response/resdata/domain:roid"),
                RLoginPassword = reply.getTextValue("/scp/response/resdata/domain:RLoginPassword"),
                RegistrantContactID = reply.getTextValue("/scp/response/resdata/domain:contact[attribute::type=\"registrant\"]"),
                AdminContactID = reply.getTextValue("/scp/response/resdata/domain:contact[attribute::type=\"admin\"]"),
                TechContactID = reply.getTextValue("/scp/response/resdata/domain:contact[attribute::type=\"tech\"]"),
                BillingContactID = reply.getTextValue("/scp/response/resdata/domain:contact[attribute::type=\"billing\"]"),
                NS1 = reply.getTextValue("/scp/response/resdata/domain:ns1"),
                NS2 = reply.getTextValue("/scp/response/resdata/domain:ns2"),
                DtCreate = reply.getTextValue("/scp/response/resdata/domain:dtCreate"),
                DtUpdate = reply.getTextValue("/scp/response/resdata/domain:dtUpdate"),
                DtExpired = reply.getTextValue("/scp/response/resdata/domain:dtExpired")
            };
        }

        private string getTopDomain(string Domain)
        {
            return Domain.Substring(Domain.LastIndexOf('.') + 1);
        }

        public bool isDomainCanBeRegisted(string Domain)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            string str = this.getTopDomain(Domain);
            if (str.StartsWith("xn-"))
                str = "ch";
            this.m_XMLWriter.WriteElementString("action", "dot" + str + ":checkDomain");
            this.m_XMLWriter.WriteElementString("domain", "name", (string)null, Domain);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            bool result;
            if (!bool.TryParse(reply.getTextValue("/scp/response/resdata/domain:name").Split(new char[1]
            {
        ':'
            }, StringSplitOptions.RemoveEmptyEntries)[1], out result))
                throw new SCPException(reply);
            return result;
        }

        public bool modifyDomainNS(string Domain, string newNS1, string newNS2)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "dot" + this.getTopDomain(Domain) + ":modifyDomain");
            this.m_XMLWriter.WriteElementString("domain", "name", (string)null, Domain);
            this.m_XMLWriter.WriteElementString("domain", "newns", (string)null, newNS1);
            this.m_XMLWriter.WriteElementString("domain", "newns", (string)null, newNS2);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public bool registNSServer(string topDomain, string ServerName)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "dot" + topDomain + ":createNS");
            this.m_XMLWriter.WriteElementString("host", "name", (string)null, ServerName);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public DomainInfo renewDomain(string Domain, string Period, string dtExpired)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "dot" + this.getTopDomain(Domain) + ":renewDomain");
            this.m_XMLWriter.WriteElementString("domain", "name", (string)null, Domain);
            this.m_XMLWriter.WriteElementString("domain", "exDate", (string)null, dtExpired);
            this.m_XMLWriter.WriteElementString("domain", "period", (string)null, Period);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return new DomainInfo()
            {
                Name = reply.getTextValue("/scp/response/resdata/domain:name"),
                ROID = reply.getTextValue("/scp/response/resdata/domain:roid"),
                BillingContactID = reply.getTextValue("/scp/response/resdata/domain:contact[attribute::type=\"billing\"]"),
                NS1 = reply.getTextValue("/scp/response/resdata/domain:ns1"),
                NS2 = reply.getTextValue("/scp/response/resdata/domain:ns2"),
                DtCreate = reply.getTextValue("/scp/response/resdata/domain:dtCreate"),
                DtUpdate = reply.getTextValue("/scp/response/resdata/domain:dtUpdate"),
                DtExpired = reply.getTextValue("/scp/response/resdata/domain:dtExpired")
            };
        }

        public bool resoveDomain(string DomainName)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "dot" + this.getTopDomain(DomainName) + ":resoveDomain");
            this.m_XMLWriter.WriteElementString("domain", "name", (string)null, DomainName);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public bool setLoginPassword(string Domain, string loginpassword)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "dot" + this.getTopDomain(Domain) + ":setLoginPassword");
            this.m_XMLWriter.WriteElementString("domain", "name", (string)null, Domain);
            this.m_XMLWriter.WriteElementString("domain", "loginpassword", (string)null, loginpassword);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public bool updateContact(string Domain, string ContactID, ContactType type, ContactInfo Contact)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "dot" + this.getTopDomain(Domain) + ":updateContact");
            this.m_XMLWriter.WriteElementString("domain", "name", (string)null, Domain);
            this.m_XMLWriter.WriteElementString("contact", "id", (string)null, ContactID);
            this.m_XMLWriter.WriteElementString("contact", "type", (string)null, type.ToString());
            this.writeContactMessage(ContactType.None, Contact);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        private void writeContactMessage(ContactType type, ContactInfo contact)
        {
            string prefix = "contact";
            if (type == ContactType.registrant)
            {
                this.m_XMLWriter.WriteStartElement("domain", "registrant", (string)null);
                prefix = "registrant";
            }
            else
            {
                this.m_XMLWriter.WriteStartElement("domain", "contact", (string)null);
                if (type != ContactType.None)
                    this.m_XMLWriter.WriteAttributeString("type", type.ToString());
            }
            this.m_XMLWriter.WriteElementString(prefix, "name", (string)null, contact.name);
            this.m_XMLWriter.WriteElementString(prefix, "cnname", (string)null, contact.cnname);
            this.m_XMLWriter.WriteElementString(prefix, "org", (string)null, contact.org);
            this.m_XMLWriter.WriteElementString(prefix, "cnorg", (string)null, contact.cnorg);
            this.m_XMLWriter.WriteElementString(prefix, "cc", (string)null, contact.cc);
            this.m_XMLWriter.WriteElementString(prefix, "sp", (string)null, contact.sp);
            this.m_XMLWriter.WriteElementString(prefix, "pc", (string)null, contact.pc);
            this.m_XMLWriter.WriteElementString(prefix, "city", (string)null, contact.city);
            this.m_XMLWriter.WriteElementString(prefix, "street", (string)null, contact.street);
            this.m_XMLWriter.WriteElementString(prefix, "street1", (string)null, contact.street1);
            this.m_XMLWriter.WriteElementString(prefix, "voice", (string)null, contact.voice);
            this.m_XMLWriter.WriteElementString(prefix, "fax", (string)null, contact.fax);
            this.m_XMLWriter.WriteElementString(prefix, "email", (string)null, contact.email);
            this.m_XMLWriter.WriteElementString(prefix, "mobile", (string)null, contact.mobile);
            this.m_XMLWriter.WriteEndElement();
        }

        private void writeSCPEnd()
        {
            this.m_XMLWriter.WriteEndDocument();
        }

        private void writeSCPStart()
        {
            this.Initialize();
            this.m_XMLWriter.WriteStartElement("scp", "urn:scp:params:xml:ns:scp-3.0");
            this.m_XMLWriter.WriteAttributeString("xmlns", "domain", (string)null, "urn:todaynic.com:domain");
            this.m_XMLWriter.WriteAttributeString("xmlns", "contact", (string)null, "urn:todaynic.com:contact");
            this.m_XMLWriter.WriteAttributeString("xmlns", "registrant", (string)null, "urn:todayisp.com:registrant");
            this.m_XMLWriter.WriteAttributeString("xmlns", "host", (string)null, "urn:todaynic.com:host");
        }
    }
}
