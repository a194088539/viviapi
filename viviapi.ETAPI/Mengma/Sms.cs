using System;
using System.Collections.Generic;
using System.Web;
using viviapi.BLL;
using viviapi.BLL.Payment;
using viviapi.BLL.User;
using viviapi.Model.Order;
using viviapi.Model.sys;
using viviapi.Model.User;
using viviapi.SysConfig;
using viviLib.Security;
using viviLib.Text;
using viviLib.Web;

namespace viviapi.ETAPI.Mengma
{
    public class Sms : ETAPIBase
    {
        private static int suppId = 800;
        private string[] split = new string[1]
        {
      "EKA"
        };

        public List<SmsComInfo> mengsmscomlist
        {
            get
            {
                List<SmsComInfo> list = new List<SmsComInfo>();
                if (!string.IsNullOrEmpty(PaymentSetting.mengsmsarrCom))
                {
                    string mengsmsarrCom = PaymentSetting.mengsmsarrCom;
                    char[] chArray1 = new char[1]
                    {
            ','
                    };
                    foreach (string str in mengsmsarrCom.Split(chArray1))
                    {
                        char[] chArray2 = new char[1]
                        {
              '|'
                        };
                        string[] strArray = str.Split(chArray2);
                        if (strArray.Length == 3 && !string.IsNullOrEmpty(strArray[0]) && !string.IsNullOrEmpty(strArray[1]) && PageValidate.IsNumber(strArray[2]))
                        {
                            SmsComInfo smsComInfo = new SmsComInfo();
                            smsComInfo.destnumber = strArray[0];
                            smsComInfo.cmd = strArray[1];
                            int result = 0;
                            int.TryParse(strArray[2], out result);
                            smsComInfo.fee = result;
                            list.Add(smsComInfo);
                        }
                    }
                }
                return list;
            }
        }

        public Sms()
          : base(Sms.suppId)
        {
        }

        public void notify()
        {
            string queryStringString1 = WebBase.GetQueryStringString("content", string.Empty);
            string queryStringString2 = WebBase.GetQueryStringString("linkid", string.Empty);
            string queryStringString3 = WebBase.GetQueryStringString("srcnumber", string.Empty);
            WebBase.GetQueryStringString("RecvTime", string.Empty);
            string queryStringString4 = WebBase.GetQueryStringString("destnumber", string.Empty);
            string queryStringString5 = WebBase.GetQueryStringString("fee", string.Empty);
            string queryStringString6 = WebBase.GetQueryStringString("gwid", string.Empty);
            string queryStringString7 = WebBase.GetQueryStringString("passkey", string.Empty);
            string queryStringString8 = WebBase.GetQueryStringString("report", string.Empty);
            if (string.IsNullOrEmpty(queryStringString8) || string.IsNullOrEmpty(queryStringString2) || !(Cryptography.MD5(queryStringString2 + this.suppKey).ToUpper() == queryStringString7.ToUpper()))
                return;
            string str = string.Empty;
            OrderSms orderSms = new OrderSms();
            OrderSmsInfo order = new OrderSmsInfo();
            order.orderid = orderSms.GenerateUniqueOrderId();
            order.userorder = string.Empty;
            order.supplierId = 800;
            order.mobile = queryStringString3;
            order.fee = Decimal.Parse(queryStringString5);
            order.message = queryStringString1;
            order.servicenum = queryStringString4;
            order.addtime = DateTime.Now;
            order.linkid = queryStringString2;
            order.gwid = queryStringString6;
            order.server = RuntimeSetting.ServerId;
            order.status = queryStringString8 == "DELIVRD" ? 2 : 4;
            order.completetime = DateTime.Now;
            OrderSmsInfo userResult = this.GetUserResult(queryStringString1, queryStringString4, queryStringString5);
            if (userResult != null)
            {
                order.userid = userResult.userid;
                order.Cmd = userResult.Cmd;
                order.userMsgContenct = userResult.userMsgContenct;
                order.payRate = userResult.payRate;
                order.payAmt = userResult.payAmt;
                order.supplierRate = userResult.supplierRate;
                order.supplierAmt = userResult.supplierAmt;
                order.notifyurl = userResult.notifyurl;
                order.promRate = new Decimal(0);
                order.promAmt = new Decimal(0);
                order.profits = userResult.supplierAmt - userResult.payAmt;
                order.manageId = userResult.manageId;
            }
            orderSms.Insert(order);
            new viviapi.BLL.OrderSmsNotify().DoNotify(order);
            HttpContext.Current.Response.Write("ok");
        }

        private OrderSmsInfo GetUserResult(string content, string destnumber, string fee)
        {
            OrderSmsInfo orderSmsInfo = new OrderSmsInfo();
            orderSmsInfo.Cmd = string.Empty;
            orderSmsInfo.userid = 0;
            orderSmsInfo.userMsgContenct = string.Empty;
            orderSmsInfo.payRate = new Decimal(0);
            orderSmsInfo.payAmt = new Decimal(0);
            orderSmsInfo.supplierId = 800;
            orderSmsInfo.supplierRate = SupplierPayRateFactory.GetRate(800, 300);
            int result1 = 0;
            int.TryParse(fee, out result1);
            orderSmsInfo.fee = (Decimal)result1;
            orderSmsInfo.notifyurl = string.Empty;
            orderSmsInfo.manageId = new int?(0);
            foreach (SmsComInfo smsComInfo in this.mengsmscomlist)
            {
                if (smsComInfo.destnumber == destnumber && smsComInfo.fee <= result1 && content.IndexOf(smsComInfo.cmd) == 0)
                {
                    string[] strArray = content.ToString().Split(this.split, StringSplitOptions.None);
                    if (strArray.Length == 2)
                    {
                        int result2 = 0;
                        if (int.TryParse(strArray[0].Replace(smsComInfo.cmd, ""), out result2))
                        {
                            orderSmsInfo.userid = result2;
                            UserInfo cacheUserBaseInfo = UserFactory.GetCacheUserBaseInfo(result2);
                            if (cacheUserBaseInfo != null)
                            {
                                orderSmsInfo.notifyurl = cacheUserBaseInfo.smsNotifyUrl;
                                orderSmsInfo.userMsgContenct = strArray[1];
                                orderSmsInfo.fee = Decimal.Parse(fee);
                                orderSmsInfo.payRate = PayRateFactory.GetUserPayRate(result2, 300);
                                orderSmsInfo.payAmt = orderSmsInfo.payRate * orderSmsInfo.fee;
                                orderSmsInfo.supplierAmt = orderSmsInfo.supplierRate * orderSmsInfo.fee;
                                orderSmsInfo.manageId = cacheUserBaseInfo.manageId;
                            }
                        }
                        break;
                    }
                    break;
                }
            }
            return orderSmsInfo;
        }
    }
}
