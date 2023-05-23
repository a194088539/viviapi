using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;

namespace com.todaynic.ScpClient
{
    public class XmlClient
    {
        private MemoryStream m_MemoryStream;
        private string m_ReceiveXML;
        private string m_SendXML;
        private string m_ServerName;
        private int m_ServerPort;
        protected XmlWriter m_XMLWriter;

        private Encoding Encoding
        {
            set
            {
                this.Initialize(value);
                Utility.Encoding = value;
            }
        }

        public string ReceiveXML
        {
            get
            {
                return this.m_ReceiveXML;
            }
        }

        public string SendXML
        {
            get
            {
                return this.m_SendXML;
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
            this.Initialize(Encoding.Default);
        }

        public void Initialize()
        {
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Encoding = Encoding.GetEncoding("gbk"),
                Indent = true,
                IndentChars = "  ",
                OmitXmlDeclaration = false,
                NewLineHandling = NewLineHandling.None,
                CheckCharacters = true
            };
            this.m_MemoryStream = new MemoryStream();
            this.m_XMLWriter = XmlWriter.Create((Stream)this.m_MemoryStream, settings);
        }

        public void Initialize(Encoding Encoding)
        {
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Encoding = Encoding,
                Indent = true,
                IndentChars = "  ",
                OmitXmlDeclaration = false,
                NewLineHandling = NewLineHandling.None,
                CheckCharacters = true
            };
            this.m_MemoryStream = new MemoryStream();
            this.m_XMLWriter = XmlWriter.Create((Stream)this.m_MemoryStream, settings);
        }

        protected SCPReply send()
        {
            IPEndPoint ipEndPoint = new IPEndPoint(Dns.GetHostAddresses(this.m_ServerName)[0], this.m_ServerPort);
            Socket socket = new Socket(ipEndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            SCPReply scpReply;
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
                byte[] buffer = new byte[1024];
                byte[] bytes = new byte[0];
                StringBuilder stringBuilder = new StringBuilder();
                do
                {
                    int num = socket.Receive(buffer);
                    byte[] numArray2 = new byte[bytes.Length];
                    bytes.CopyTo((Array)numArray2, 0);
                    bytes = new byte[bytes.Length + num];
                    numArray2.CopyTo((Array)bytes, 0);
                    for (int index = 0; index < num; ++index)
                        bytes[numArray2.Length + index] = buffer[index];
                }
                while (!Encoding.Default.GetString(bytes, 0, bytes.Length).ToString().Trim().Contains("</scp>"));
                string str = Encoding.Default.GetString(bytes, 0, bytes.Length).ToString().Trim();
                int startIndex = str.IndexOf("<?xml");
                if (startIndex > 0)
                    str = str.Substring(startIndex);
                int num1 = str.IndexOf("</scp>");
                if (num1 > 0)
                    str = str.Substring(0, num1 + "</scp>".Length);
                this.m_ReceiveXML = str;
                Console.WriteLine(this.m_ReceiveXML);
                scpReply = new SCPReply(this.m_ReceiveXML);
            }
            finally
            {
                socket.Close();
            }
            return scpReply;
        }

        private void SetEncoding(string encoding)
        {
            Utility.Encoding = this.m_XMLWriter.Settings.Encoding;
            this.Initialize(Utility.Encoding);
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
