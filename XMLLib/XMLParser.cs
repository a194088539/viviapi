using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace cn.eibei.xml
{
    public class XMLParser
    {
        private XmlDocument m_XMLDocumet;
        private XmlNamespaceManager m_XMLNamespaceManager;
        private string m_XMLData;

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

        public string getNodesInnerXML(string xpath)
        {
            XmlNodeList xmlNodeList = this.m_XMLDocumet.DocumentElement.SelectNodes(xpath, this.m_XMLNamespaceManager);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (XmlNode xmlNode in xmlNodeList)
                stringBuilder.Append(xmlNode.InnerXml + Environment.NewLine);
            return stringBuilder.ToString();
        }

        public string getSingleNodeInnerXML(string xpath)
        {
            XmlNode xmlNode = this.m_XMLDocumet.DocumentElement.SelectSingleNode(xpath, this.m_XMLNamespaceManager);
            if (xmlNode != null)
                return xmlNode.InnerXml;
            return string.Empty;
        }

        public string getNodesInnerText(string xpath)
        {
            XmlNodeList xmlNodeList = this.m_XMLDocumet.DocumentElement.SelectNodes(xpath, this.m_XMLNamespaceManager);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (XmlNode xmlNode in xmlNodeList)
                stringBuilder.Append(xmlNode.InnerText + Environment.NewLine);
            return stringBuilder.ToString();
        }

        public string getSingleNodeInnerText(string xpath)
        {
            XmlNode xmlNode = this.m_XMLDocumet.DocumentElement.SelectSingleNode(xpath, this.m_XMLNamespaceManager);
            if (xmlNode != null)
                return xmlNode.InnerText;
            return string.Empty;
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

        public List<string> getInnerTextList(string xpath)
        {
            List<string> list = new List<string>();
            foreach (XmlNode xmlNode in this.m_XMLDocumet.DocumentElement.SelectNodes(xpath, this.m_XMLNamespaceManager))
                list.Add(xmlNode.InnerText);
            return list;
        }
    }
}
