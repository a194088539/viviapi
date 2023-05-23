using System;
using System.Xml;

namespace viviapi.ETAPI.Mobaopay
{
    public class MobaopayRefundEntity
    {
        private string respCode;
        private string respDesc;
        private string signMsg;

        public string RespCode
        {
            get
            {
                return this.respCode;
            }
        }

        public string RespDesc
        {
            get
            {
                return this.respDesc;
            }
        }

        public string SignMsg
        {
            get
            {
                return this.signMsg;
            }
        }

        public void Parse(string resp)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(resp);
            XmlNode xmlNode1 = xmlDocument.SelectSingleNode("/moboAccount/respData/respCode");
            if (null == xmlNode1)
                throw new Exception("响应信息格式错误：不存在'respCode'节点。");
            this.respCode = xmlNode1.InnerText;
            XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/moboAccount/respData/respDesc");
            if (null == xmlNode2)
                throw new Exception("响应信息格式错误：不存在'respDesc'节点");
            this.respDesc = xmlNode2.InnerText;
            XmlNode xmlNode3 = xmlDocument.SelectSingleNode("/moboAccount/signMsg");
            if (null == xmlNode3)
                throw new Exception("响应信息格式错误：不存在'signMsg'节点");
            this.signMsg = xmlNode3.InnerText;
        }
    }
}
