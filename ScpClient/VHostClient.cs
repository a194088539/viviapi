using System.Collections.Generic;

namespace com.todaynic.ScpClient
{
    public class VHostClient : XmlClient
    {
        private string m_VCPID;
        private string m_VCPPassword;

        public VHostClient(string HostName, int HostPort, string VcpID, string VcpPwd)
          : base(HostName, HostPort)
        {
            this.m_VCPID = VcpID;
            this.m_VCPPassword = VcpPwd;
        }

        public bool addBindDomain(string VHostID, string Domain)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:addBindDomain");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteElementString("vhost", "domain", (string)null, Domain);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public bool bindDomain(string VHostID, string Domain, string TomcatBase)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:bindDomain");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteElementString("vhost", "domain", (string)null, Domain);
            this.m_XMLWriter.WriteElementString("vhost", "tomcatbase", (string)null, TomcatBase);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public bool changeFTPPassword(string VHostID, string OldPassword, string NewPassword)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:changeFTPPassword");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteElementString("vhost", "oldpwd", (string)null, OldPassword);
            this.m_XMLWriter.WriteElementString("vhost", "newpwd", (string)null, NewPassword);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public bool changeVHostConfig(string VHostID, string NewProductID)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:changeVHostConfig");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteElementString("vhost", "productid", (string)null, NewProductID);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public bool createVDir(string VHostID, string VDirName)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:createVDir");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteElementString("vhost", "vdirname", (string)null, VDirName);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public Dictionary<string, string> createVHost(VHostInfo vhostInfo)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:createVHost");
            this.m_XMLWriter.WriteElementString("vhost", "productid", (string)null, vhostInfo.ProductID);
            this.m_XMLWriter.WriteElementString("vhost", "domain", (string)null, vhostInfo.Domain);
            this.m_XMLWriter.WriteElementString("vhost", "username", (string)null, vhostInfo.UserName);
            this.m_XMLWriter.WriteElementString("vhost", "password", (string)null, vhostInfo.Password);
            this.m_XMLWriter.WriteElementString("vhost", "period", (string)null, vhostInfo.Period);
            this.m_XMLWriter.WriteElementString("vhost", "vhostquota", (string)null, vhostInfo.Quota);
            this.m_XMLWriter.WriteElementString("vhost", "dbquota", (string)null, vhostInfo.DBQuota);
            this.m_XMLWriter.WriteElementString("vhost", "emailquota", (string)null, vhostInfo.EmailQuota);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return reply.getDictionaryValue("/scp/response/resdata/vhostinfo");
        }

        public bool deleteVDir(string VHostID, string VDirName)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:deleteVDir");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteElementString("vhost", "vdirname", (string)null, VDirName);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public bool fixVHost(string VHostID)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:fixVHost");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public Dictionary<string, string> getDefaultDoc(string VHostID)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:getDefaultDocList");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return reply.getDictionaryValue("/scp/response/resdata/DefaultDoc");
        }

        public Dictionary<string, string> getIISLogList(string VHostID)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:getIISLogList");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return reply.getDictionaryValue("/scp/response/resdata/loglist");
        }

        public Dictionary<string, string> getRemoteVHostInfo(string VHostID)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:getRemoteVHostInfo");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return reply.getDictionaryValue("/scp/response/resdata/vhostinfo");
        }

        public Dictionary<string, string> getVDirList(string VHostID)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:getVDirList");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return reply.getDictionaryValue("/scp/response/resdata/vdirlist");
        }

        public Dictionary<string, string> getVHostInfo(string VHostID)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:getVHostInfo");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return reply.getDictionaryValue("/scp/response/resdata/vhostinfo");
        }

        public bool initializeFTPPassword(string VHostID, string Password)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:initializeFTPPassword");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteElementString("vhost", "password", (string)null, Password);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public bool moveIISLogToFTPDir(string VHostID, string LogFileName)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:moveIISLogToFtpDir");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteElementString("vhost", "iislogfilename", (string)null, LogFileName);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public bool removeBindDomain(string VHostID, string Domain)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:removeBindDomain");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteElementString("vhost", "domain", (string)null, Domain);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public bool renewVHost(string VHostID, string Period, string dtExpired)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:renewVHost");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteElementString("vhost", "period", (string)null, Period);
            this.m_XMLWriter.WriteElementString("vhost", "curexpired", (string)null, dtExpired);
            this.m_XMLWriter.WriteEndElement();
            this.writeSecurityMessage(this.m_VCPID, this.m_VCPPassword, UserType.vcpuser);
            this.writeSCPEnd();
            SCPReply reply = this.send();
            if (reply.Status != ScpStatus.Successfully)
                throw new SCPException(reply);
            return true;
        }

        public bool setDefaultDoc(string VHostID, string DefDoc)
        {
            this.writeSCPStart();
            this.m_XMLWriter.WriteStartElement("command");
            this.m_XMLWriter.WriteElementString("action", "vhost:setDefaultDoc");
            this.m_XMLWriter.WriteElementString("vhost", "vhostid", (string)null, VHostID);
            this.m_XMLWriter.WriteElementString("vhost", "defaultdoc", (string)null, DefDoc);
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
            this.m_XMLWriter.WriteAttributeString("xmlns", "vhost", (string)null, "urn:todaynic.com:vhost");
        }
    }
}
