using System;
using System.Web.UI;
using viviapi.BLL.User;
using viviLib.Security;

namespace viviapi.WebUI.LongBao.merchant
{
    public class recharge_return : Page
    {
        protected string successAmt = "0.00";

        protected void Page_Load(object sender, EventArgs e)
        {
            bool flag = false;
            string apiKey = UserFactory.GetModel(800).APIKey;
            string str1 = this.Request["orderid"];
            string str2 = this.Request["opstate"];
            string str3 = this.Request["ovalue"];
            string str4 = this.Request["sign"];
            string str5 = this.Request["sysorderid"];
            string str6 = this.Request["systime"];
            string str7 = this.Request["attach"];
            string str8 = this.Request["msg"];
            string strToEncrypt = string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", (object)str1, (object)str2, (object)str3, (object)apiKey);
            if (str4 == Cryptography.MD5(strToEncrypt))
            {
                this.successAmt = str3;
                if (str2.Equals("0") || str2.Equals("-3"))
                    flag = true;
                else if (str2.Equals("-1") || str2.Equals("-2") || (str2.Equals("-4") || !str2.Equals("-5")))
                    ;
            }
            if (flag)
                ;
        }
    }
}
