using System;
using System.Xml;

namespace viviapi.ETAPI.Mobaopay
{
    public class MobaopayQueryEntity
    {
        private string respCode;
        private string respDesc;
        private string accDate;
        private string accNo;
        private string orderNo;
        private string status;
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

        public string AccDate
        {
            get
            {
                return this.accDate;
            }
        }

        public string AccNo
        {
            get
            {
                return this.accNo;
            }
        }

        public string OrderNo
        {
            get
            {
                return this.orderNo;
            }
        }

        public string Status
        {
            get
            {
                return this.status;
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
                throw new Exception("响应信息格式错误：不存在'respDesc'节点。");
            this.respDesc = xmlNode2.InnerText;
            if ("00".Equals(this.respCode))
            {
                XmlNode xmlNode3 = xmlDocument.SelectSingleNode("/moboAccount/respData/accDate");
                if (null == xmlNode3)
                    throw new Exception("响应信息格式错误：不存在'accDate'节点。");
                this.accDate = xmlNode3.InnerText;
                XmlNode xmlNode4 = xmlDocument.SelectSingleNode("/moboAccount/respData/accNo");
                if (null == xmlNode4)
                    throw new Exception("响应信息格式错误：不存在'accNo'节点。");
                this.accNo = xmlNode4.InnerText;
                XmlNode xmlNode5 = xmlDocument.SelectSingleNode("/moboAccount/respData/orderNo");
                if (null == xmlNode5)
                    throw new Exception("响应信息格式错误：不存在'orderNo'节点。");
                this.orderNo = xmlNode5.InnerText;
                XmlNode xmlNode6 = xmlDocument.SelectSingleNode("/moboAccount/respData/Status");
                if (null == xmlNode6)
                    throw new Exception("响应信息格式错误：不存在'status'节点。");
                this.status = xmlNode6.InnerText;
            }
            XmlNode xmlNode7 = xmlDocument.SelectSingleNode("/moboAccount/signMsg");
            if (null == xmlNode7)
                throw new Exception("响应信息格式错误：不存在'signMsg'节点。");
            this.signMsg = xmlNode7.InnerText;
        }
    }
}
