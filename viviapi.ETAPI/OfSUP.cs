using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using viviLib.ExceptionHandling;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.ETAPI
{
    public class OfSUP : ETAPIBase
    {
        private static int suppId = 82;
        private string ApiUrl = "http://supply.api.17sup.com/";

        public OfSUP()
          : base(OfSUP.suppId)
        {
        }

        public OfSUPSupplyResult Supply(string reqid)
        {
            OfSUPSupplyResult ofSupSupplyResult = new OfSUPSupplyResult();
            ofSupSupplyResult.status = "-1";
            ofSupSupplyResult.msg = "未知错误";
            try
            {
                ofSupSupplyResult = this.ConversionToSupplyResult(WebClientHelper.GetString(string.Format("{0}/supply.do?partner={1}&tplid={2}&sign={3}&reqid={4}", (object)this.ApiUrl, (object)this.suppAccount, (object)this.suppUserName, (object)Cryptography.MD5(string.Format("{0}{1}{2}", (object)this.suppAccount, (object)this.suppUserName, (object)this.suppKey)).ToUpper(), (object)reqid), (NameValueCollection)null, "GET", Encoding.GetEncoding("gb2312")));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return ofSupSupplyResult;
        }

        public OfSUPSupplyResult ConversionToSupplyResult(string retXml)
        {
            OfSUPSupplyResult ofSupSupplyResult = new OfSUPSupplyResult();
            if (!string.IsNullOrEmpty(retXml))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(retXml);
                ofSupSupplyResult.status = xmlDocument.GetElementsByTagName("status")[0].InnerText;
                ofSupSupplyResult.msg = xmlDocument.GetElementsByTagName("msg")[0].InnerText;
                StringBuilder stringBuilder = new StringBuilder();
                List<OfSUPGetOrderdataList> list = new List<OfSUPGetOrderdataList>();
                foreach (XmlNode xmlNode1 in xmlDocument.GetElementsByTagName("data"))
                {
                    OfSUPGetOrderdataList getOrderdataList = new OfSUPGetOrderdataList();
                    foreach (XmlNode xmlNode2 in xmlNode1.ChildNodes)
                    {
                        if (xmlNode2.Name == "reqId")
                            getOrderdataList.reqId = xmlNode2.InnerText;
                        else if (xmlNode2.Name == "fields")
                            getOrderdataList.fields = xmlNode2.InnerText;
                        else if (xmlNode2.Name == "dataList")
                        {
                            getOrderdataList.dataList = xmlNode2.OuterXml;
                            if (!string.IsNullOrEmpty(xmlNode2.OuterXml))
                            {
                                xmlDocument.GetElementsByTagName(xmlNode2.OuterXml);
                                foreach (XmlNode xmlNode3 in xmlNode1.ChildNodes)
                                {
                                    if (xmlNode3.Name == "order_id")
                                    {
                                        if (stringBuilder.Length == 0)
                                            stringBuilder.AppendFormat("{0}", (object)xmlNode3.InnerText);
                                        else
                                            stringBuilder.AppendFormat(",{0}", (object)xmlNode3.InnerText);
                                    }
                                }
                            }
                        }
                    }
                    list.Add(getOrderdataList);
                }
                ofSupSupplyResult.orderids = stringBuilder.ToString();
                ofSupSupplyResult.data = list;
            }
            return ofSupSupplyResult;
        }

        public void CheckOrder(string reqid, string orderids)
        {
        }
    }
}
