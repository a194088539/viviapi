using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;
using tenpay;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviapi.ETAPI.tenpay
{
    public class batch_trans : ETAPIBase
    {
        private static int suppId = 106;
        private string sp_id = string.Empty;
        private string sp_key = string.Empty;
        private string op_user = string.Empty;
        private string op_passwd = string.Empty;
        private string certFile = string.Empty;
        private string certPasswd = string.Empty;
        private viviapi.BLL.Financial.tenpay_batch_trans_head headBLL = new viviapi.BLL.Financial.tenpay_batch_trans_head();
        private viviapi.BLL.Financial.tenpay_batch_trans_detail detailBLL = new viviapi.BLL.Financial.tenpay_batch_trans_detail();

        public batch_trans()
          : base(batch_trans.suppId)
        {
        }

        private void InitParam()
        {
            this.sp_id = this.suppAccount;
            this.sp_key = this.suppKey;
            this.op_user = this._suppInfo.puserid1;
            this.op_passwd = this._suppInfo.puserkey1;
            this.certFile = this._suppInfo.puserid2;
            this.certPasswd = this._suppInfo.puserkey2;
        }

        public void DoTrans(viviapi.Model.Financial.tenpay_batch_trans_head _info)
        {
            try
            {
                this.InitParam();
                if (this._suppInfo == null || (_info == null || _info.items == null || _info.items.Count <= 0))
                    return;
                DirectTransClientRequestHandler clientRequestHandler = new DirectTransClientRequestHandler(HttpContext.Current);
                TenpayHttpClient tenpayHttpClient = new TenpayHttpClient();
                DirectTransClientResponseHandler clientResponseHandler = new DirectTransClientResponseHandler();
                clientRequestHandler.setKey(this.sp_key);
                clientRequestHandler.setParameter("op_code", "1000");
                clientRequestHandler.setParameter("op_name", "batch_transfer");
                clientRequestHandler.setParameter("op_user", this.op_user);
                clientRequestHandler.setParameter("op_passwd", this.op_passwd);
                clientRequestHandler.setParameter("op_time", DateTime.Now.ToString("yyyyMMddHHmmssff"));
                clientRequestHandler.setParameter("sp_id", this.sp_id);
                clientRequestHandler.setParameter("package_id", _info.package_id);
                clientRequestHandler.setParameter("total_num", _info.total_num.ToString());
                clientRequestHandler.setParameter("total_amt", (_info.total_amt * new Decimal(100)).ToString("f0"));
                clientRequestHandler.setParameter("client_ip", ServerVariables.TrueIP);
                clientRequestHandler.setParameter("version", "2");
                StringBuilder stringBuilder = new StringBuilder();
                foreach (viviapi.Model.Financial.tenpay_batch_trans_detail batchTransDetail in _info.items)
                    stringBuilder.AppendFormat("<record><serial>{0}</serial><rec_acc>{1}</rec_acc><rec_name>{2}</rec_name><cur_type>1</cur_type><pay_amt>{3:0}</pay_amt><desc>{4}</desc></record>", (object)batchTransDetail.serial, (object)batchTransDetail.rec_acc, (object)batchTransDetail.rec_name, (object)(batchTransDetail.pay_amt * new Decimal(100)), (object)batchTransDetail.serial);
                clientRequestHandler.setParameter("record_set", stringBuilder.ToString());
                tenpayHttpClient.setCertInfo(this.certFile, this.certPasswd);
                tenpayHttpClient.setReqContent(clientRequestHandler.getRequestURL());
                tenpayHttpClient.setTimeOut(10);
                if (tenpayHttpClient.call())
                    this.UpdateToDB(tenpayHttpClient.getResContent(), _info.package_id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public List<handler_result> GetResult(int type, string retxml, out string total)
        {
            string str = "suc";
            if (type == 2)
                str = "fail";
            else if (type == 3)
                str = "uncertain";
            string name1 = str + "_rec";
            string name2 = str + "_total";
            total = string.Empty;
            List<handler_result> list = new List<handler_result>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(retxml);
            total = xmlDocument.GetElementsByTagName(name2)[0].InnerText;
            foreach (XmlNode xmlNode1 in xmlDocument.GetElementsByTagName(name1))
            {
                handler_result handlerResult = new handler_result();
                handlerResult.type = type;
                foreach (XmlNode xmlNode2 in xmlNode1.ChildNodes)
                {
                    if (xmlNode2.Name == "serial")
                        handlerResult.serial = int.Parse(xmlNode2.InnerText);
                    else if (xmlNode2.Name == "rec_acc")
                        handlerResult.rec_acc = xmlNode2.InnerText;
                    else if (xmlNode2.Name == "rec_name")
                        handlerResult.rec_name = xmlNode2.InnerText;
                    else if (xmlNode2.Name == "cur_type")
                        handlerResult.cur_type = xmlNode2.InnerText;
                    else if (xmlNode2.Name == "pay_amt")
                        handlerResult.pay_amt = Decimal.Parse(xmlNode2.InnerText) / new Decimal(100);
                    else if (xmlNode2.Name == "trans_id")
                        handlerResult.trans_id = xmlNode2.InnerText;
                    else if (xmlNode2.Name == "desc")
                        handlerResult.trans_id = xmlNode2.InnerText;
                }
                list.Add(handlerResult);
            }
            return list;
        }

        public void DoQuery(string package_id)
        {
            try
            {
                if (this._suppInfo == null || string.IsNullOrEmpty(package_id))
                    return;
                this.InitParam();
                DirectTransClientRequestHandler clientRequestHandler = new DirectTransClientRequestHandler(HttpContext.Current);
                TenpayHttpClient tenpayHttpClient = new TenpayHttpClient();
                clientRequestHandler.setKey(this.sp_key);
                clientRequestHandler.setParameter("op_code", "1001");
                clientRequestHandler.setParameter("op_name", "batch_transfer_query");
                clientRequestHandler.setParameter("op_user", this.op_user);
                clientRequestHandler.setParameter("op_passwd", this.op_passwd);
                clientRequestHandler.setParameter("op_time", DateTime.Now.ToString("yyyyMMddHHmmssff"));
                clientRequestHandler.setParameter("sp_id", this.sp_id);
                clientRequestHandler.setParameter("package_id", package_id);
                clientRequestHandler.setParameter("client_ip", ServerVariables.TrueIP);
                clientRequestHandler.setParameter("version", "2");
                tenpayHttpClient.setCertInfo(this.certFile, this.certPasswd);
                tenpayHttpClient.setReqContent(clientRequestHandler.getRequestURL());
                tenpayHttpClient.setTimeOut(10);
                if (tenpayHttpClient.call())
                    this.UpdateToDB(tenpayHttpClient.getResContent(), package_id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public void UpdateToDB(string rescontent, string package_id)
        {
            DirectTransClientResponseHandler clientResponseHandler = new DirectTransClientResponseHandler();
            clientResponseHandler.setContent(rescontent);
            string total1 = string.Empty;
            List<handler_result> list1 = new List<handler_result>();
            string total2 = string.Empty;
            List<handler_result> list2 = new List<handler_result>();
            string total3 = string.Empty;
            List<handler_result> list3 = new List<handler_result>();
            string parameter1 = clientResponseHandler.getParameter("retcode");
            string parameter2 = clientResponseHandler.getParameter("retmsg");
            int status = 1;
            int result1 = 0;
            int result2 = 0;
            int result3 = 0;
            if (parameter1 == "0" || parameter1 == "00")
            {
                status = 2;
                string parameter3 = clientResponseHandler.getParameter("success_set");
                if (!string.IsNullOrEmpty(parameter3))
                {
                    list1 = this.GetResult(1, parameter3, out total1);
                    int.TryParse(total1, out result1);
                }
                foreach (handler_result handlerResult in list1)
                    this.detailBLL.Complete(package_id, handlerResult.serial, 2, handlerResult.pay_amt, handlerResult.desc, handlerResult.trans_id);
                string parameter4 = clientResponseHandler.getParameter("fail_set");
                if (!string.IsNullOrEmpty(parameter4))
                {
                    list2 = this.GetResult(2, parameter4, out total2);
                    int.TryParse(total2, out result3);
                }
                foreach (handler_result handlerResult in list2)
                    this.detailBLL.Complete(package_id, handlerResult.serial, 4, handlerResult.pay_amt, handlerResult.desc, handlerResult.trans_id);
                string parameter5 = clientResponseHandler.getParameter("uncertain_set");
                if (!string.IsNullOrEmpty(parameter5))
                {
                    list3 = this.GetResult(3, parameter5, out total3);
                    int.TryParse(total3, out result2);
                }
            }
            this.headBLL.Complete(package_id, status, parameter1, parameter2, rescontent, result1, result2, result3);
        }
    }
}
