using com.todaynic.Utility;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;

namespace cn.eibei.xml
{
    public class XmlClient
    {
        private MemoryStream m_MemoryStream;
        protected XmlWriter m_XMLWriter;
        private string m_SendXML;
        private string m_ReceiveXML;
        private string m_ServerName;
        private int m_ServerPort;

        public string SendXML
        {
            get
            {
                return this.m_SendXML;
            }
        }

        public string ReceiveXML
        {
            get
            {
                return this.m_ReceiveXML;
            }
        }

        public string ServerName
        {
            get
            {
                return this.m_ServerName;
            }
            set
            {
                this.m_ServerName = value;
            }
        }

        public int ServerPort
        {
            get
            {
                return this.m_ServerPort;
            }
            set
            {
                this.m_ServerPort = value;
            }
        }

        public XmlClient(string ServerName, int ServerPort)
        {
            this.m_ServerName = ServerName;
            this.m_ServerPort = ServerPort;
            this.initialize();
        }

        private void initialize()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.Default;
            settings.Indent = true;
            settings.IndentChars = "  ";
            settings.OmitXmlDeclaration = false;
            settings.NewLineHandling = NewLineHandling.None;
            settings.CheckCharacters = true;
            this.m_MemoryStream = new MemoryStream();
            this.m_XMLWriter = XmlWriter.Create((Stream)this.m_MemoryStream, settings);
        }

        protected virtual void reset()
        {
            if (this.m_XMLWriter.WriteState != WriteState.Closed)
                return;
            this.initialize();
        }

        protected SCPReply send()
        {
            IPEndPoint ipEndPoint = new IPEndPoint(Dns.GetHostAddresses(this.m_ServerName)[0], this.m_ServerPort);
            Socket socket = new Socket(ipEndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                this.m_XMLWriter.Flush();
                this.m_XMLWriter.Close();
                this.m_MemoryStream.Flush();
                byte[] numArray1 = this.m_MemoryStream.ToArray();
                this.m_MemoryStream.Close();
                this.m_SendXML = Encoding.Default.GetString(numArray1);
                socket.Connect((EndPoint)ipEndPoint);
                socket.Send(numArray1, 0, numArray1.Length, SocketFlags.None);
                byte[] numArray2 = new byte[1024];
                StringBuilder stringBuilder = new StringBuilder();
                do
                {
                    int count = socket.Receive(numArray2);
                    if (count > 0)
                        stringBuilder.Append(Encoding.Default.GetString(numArray2, 0, count));
                    else
                        break;
                }
                while (!stringBuilder.ToString().Trim().EndsWith("</scp>"));
                this.m_ReceiveXML = stringBuilder.ToString();
                return new SCPReply(this.m_ReceiveXML);
            }
            finally
            {
                socket.Close();
            }
        }

        protected void writeSecurityMessage(string ID, string Password, UserType userType)
        {
            this.m_XMLWriter.WriteStartElement("security");
            this.m_XMLWriter.WriteElementString(userType.ToString(), ID);
            string str = DateTime.Now.ToFileTime().ToString();
            this.m_XMLWriter.WriteElementString("cltrid", str);
            this.m_XMLWriter.WriteElementString("login", Utility.getMd5Hash(str + "-" + Password));
            this.m_XMLWriter.WriteEndElement();
        }
    }
}
