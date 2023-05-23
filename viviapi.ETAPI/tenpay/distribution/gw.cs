using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;
using tenpay;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviapi.ETAPI.tenpay.distribution
{
    public class gw : ETAPIBase
    {
        private static int suppId = 100;
        private string sp_id = string.Empty;
        private string sp_key = string.Empty;
        private string op_user = string.Empty;
        private string op_passwd = string.Empty;
        private string certFile = string.Empty;
        private string certPasswd = string.Empty;

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/ebatong/distribution_notify.aspx";
            }
        }

        public gw()
          : base(gw.suppId)
        {
            if (this._suppInfo == null)
                return;
            this.sp_id = this._suppInfo.puserid1;
            this.sp_key = this._suppInfo.puserkey1;
            this.op_user = this._suppInfo.puserid2;
            this.op_passwd = this._suppInfo.puserkey2;
            this.certFile = this._suppInfo.puserid3;
            this.certPasswd = this._suppInfo.puserkey3;
        }

        public string Req2(string trade_no, string bank_code, string bank_site_name, string bank_account_name, string bank_account_no, Decimal amount, string remark)
        {
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
            clientRequestHandler.setParameter("package_id", trade_no);
            clientRequestHandler.setParameter("total_num", "1");
            clientRequestHandler.setParameter("total_amt", (amount * new Decimal(100)).ToString("f0"));
            clientRequestHandler.setParameter("client_ip", ServerVariables.TrueIP);
            clientRequestHandler.setParameter("version", "2");
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("<record><serial>{0}</serial><rec_acc>{1}</rec_acc><rec_name>{2}</rec_name><cur_type>1</cur_type><pay_amt>{3:0}</pay_amt><desc>{4}</desc></record>", (object)1, (object)bank_account_no, (object)bank_account_name, (object)(amount * new Decimal(100)), (object)1);
            clientRequestHandler.setParameter("record_set", stringBuilder.ToString());
            tenpayHttpClient.setCertInfo(this.certFile, this.certPasswd);
            tenpayHttpClient.setReqContent(clientRequestHandler.getRequestURL());
            tenpayHttpClient.setTimeOut(10);
            string str = string.Empty;
            if (tenpayHttpClient.call())
                str = tenpayHttpClient.getResContent();
            return str;
        }

        public bool DoTrans(viviapi.Model.distribution info)
        {
            try
            {
                this.UpdateToDB(this.Req2(info.trade_no, info.bankCode, info.bankBranch, info.bankAccountName, info.bankAccount, info.amount, string.Empty), info.trade_no);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return false;
        }

        public void UpdateToDB(string rescontent, string package_id)
        {
            DirectTransClientResponseHandler clientResponseHandler = new DirectTransClientResponseHandler();
            clientResponseHandler.setContent(rescontent);
            string total1 = string.Empty;
            List<handler_result> list1 = new List<handler_result>();
            string total2 = string.Empty;
            List<handler_result> list2 = new List<handler_result>();
            string str = string.Empty;
            List<handler_result> list3 = new List<handler_result>();
            string parameter1 = clientResponseHandler.getParameter("retcode");
            clientResponseHandler.getParameter("retmsg");
            int num = 1;
            int result1 = 0;
            int result2 = 0;
            if (!(parameter1 == "0") && !(parameter1 == "00"))
                return;
            num = 2;
            string parameter2 = clientResponseHandler.getParameter("success_set");
            if (!string.IsNullOrEmpty(parameter2))
            {
                list1 = this.GetResult(1, parameter2, out total1);
                int.TryParse(total1, out result1);
            }
            foreach (handler_result handlerResult in list1)
                Withdraw.Complete(gw.suppId, package_id, false, 3, handlerResult.pay_amt.ToString(), handlerResult.trans_id, handlerResult.desc);
            string parameter3 = clientResponseHandler.getParameter("fail_set");
            if (!string.IsNullOrEmpty(parameter3))
            {
                list2 = this.GetResult(2, parameter3, out total2);
                int.TryParse(total2, out result2);
            }
            foreach (handler_result handlerResult in list2)
                Withdraw.Complete(gw.suppId, package_id, true, 4, handlerResult.pay_amt.ToString(), handlerResult.trans_id, handlerResult.desc);
            if (string.IsNullOrEmpty(clientResponseHandler.getParameter("uncertain_set")))
                ;
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
    }
}
