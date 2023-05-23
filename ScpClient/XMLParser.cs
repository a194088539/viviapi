using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace com.todaynic.ScpClient
{
    public class XMLParser
    {
        private string m_XMLData;
        private XmlDocument m_XMLDocumet;
        private XmlNamespaceManager m_XMLNamespaceManager;

        public string XMLData
        {
            get
            {
                return this.m_XMLData;
            }
        }

        public XMLParser(string XMLData)
        {
            this.m_XMLData = XMLData;
            this.m_XMLDocumet = new XmlDocument();
            this.m_XMLDocumet.LoadXml(XMLData);
            this.m_XMLNamespaceManager = new XmlNamespaceManager(this.m_XMLDocumet.NameTable);
            XmlElement documentElement = this.m_XMLDocumet.DocumentElement;
            if (!documentElement.HasAttributes)
                return;
            foreach (XmlAttribute xmlAttribute in (XmlNamedNodeMap)documentElement.Attributes)
            {
                if (!(xmlAttribute.LocalName == "xmlns"))
                    this.m_XMLNamespaceManager.AddNamespace(xmlAttribute.LocalName, xmlAttribute.Value);
            }
        }

        public string getAttributeTextOfNode(string xpath, string attributeName)
        {
            XmlNode xmlNode = this.m_XMLDocumet.DocumentElement.SelectSingleNode(xpath, this.m_XMLNamespaceManager);
            if (xmlNode != null)
                return xmlNode.Attributes[attributeName].InnerText;
            return string.Empty;
        }

        public Dictionary<string, string> getDictionaryOfANode(string xpath)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (XmlNode xmlNode in this.m_XMLDocumet.DocumentElement.SelectSingleNode(xpath, this.m_XMLNamespaceManager).ChildNodes)
            {
                if (!dictionary.ContainsKey(xmlNode.Name))
                    dictionary.Add(xmlNode.Name, xmlNode.InnerXml);
            }
            return dictionary;
        }

        public List<string> getListOfANode(string xpath)
        {
            List<string> list = new List<string>();
            string[] strArray = this.getNodesInnerText(xpath).Split(new string[1]
            {
        Environment.NewLine
            }, StringSplitOptions.None);
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (!string.IsNullOrEmpty(strArray[index]))
                    list.Add(strArray[index]);
            }
            return list;
        }

        public string getNodesInnerText(string xpath)
        {
            XmlNodeList xmlNodeList = this.m_XMLDocumet.DocumentElement.SelectNodes(xpath, this.m_XMLNamespaceManager);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (XmlNode xmlNode in xmlNodeList)
                stringBuilder.Append(xmlNode.InnerText + Environment.NewLine);
            return stringBuilder.ToString();
        }

        public string getNodesInnerXML(string xpath)
        {
            XmlNodeList xmlNodeList = this.m_XMLDocumet.DocumentElement.SelectNodes(xpath, this.m_XMLNamespaceManager);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (XmlNode xmlNode in xmlNodeList)
                stringBuilder.Append(xmlNode.InnerXml + Environment.NewLine);
            return stringBuilder.ToString();
        }

        public string getSingleNodeInnerText(string xpath)
        {
            XmlNode xmlNode = this.m_XMLDocumet.DocumentElement.SelectSingleNode(xpath, this.m_XMLNamespaceManager);
            if (xmlNode != null)
                return xmlNode.InnerText;
            return string.Empty;
        }

        public string getSingleNodeInnerXML(string xpath)
        {
            XmlNode xmlNode = this.m_XMLDocumet.DocumentElement.SelectSingleNode(xpath, this.m_XMLNamespaceManager);
            if (xmlNode != null)
                return xmlNode.InnerXml;
            return string.Empty;
        }
    }
}
