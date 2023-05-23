using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Order;
using viviapi.BLL.Payment;
using viviapi.BLL.User;
using viviapi.ETAPI.huiyuan;
using viviapi.Model.Order;
using viviapi.Model.sys;
using viviapi.Model.User;
using viviapi.SysConfig;
using viviLib.Logging;
using viviLib.Text;

namespace viviapi.gateway
{
    public class smsTest : Page
    {
        private string[] split = new string[1]
        {
      "eka"
        };
        protected HtmlForm form1;
        protected TextBox TextBox1;
        protected TextBox TextBox2;
        protected TextBox TextBox3;
        protected Button Button1;
        protected Label lblmsg;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable failOrders = Dal.GetFailOrders();
            if (failOrders == null)
                return;
            foreach (DataRow dataRow in (InternalDataCollectionBase)failOrders.Rows)
            {
                string orderid = dataRow["orderid"].ToString();
                Card card = new Card();
                string str = card.Query(orderid);
                LogHelper.Write(str);
                if (!string.IsNullOrEmpty(str))
                    LogHelper.Write(card.Finish(str).ToString());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.lblmsg.Text = this.GetUserResult(this.TextBox1.Text.Trim(), this.TextBox2.Text.Trim(), this.TextBox3.Text.Trim()).userid.ToString();
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
            orderSmsInfo.supplierRate = SupplierPayRateFactory.GetRate(800, 90);
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
                                orderSmsInfo.payRate = PayRateFactory.GetUserPayRate(result2, 90);
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
