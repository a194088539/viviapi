namespace viviapi.gateway
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public class payinterface : Page
    {
        protected HtmlForm form1;

        private void Eka365pay(string orderid, string callBackurl)
        {
            string str5;
            string str = ConfigurationManager.AppSettings["userid"];
            string str2 = ConfigurationManager.AppSettings["userkey"];
            if (base.Request.Form["bankCardType"] == "00")
            {
                string str3 = base.Request.Form["bankCode"];
                string str4 = base.Request.Form["totalAmount"];
                str5 = string.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}", new object[] { str, str3, str4, orderid, callBackurl });
                string url = string.Format("http://" + base.Request.ServerVariables["HTTP_HOST"] + "/chargebank.aspx?{0}&sign={1}", str5, FormsAuthentication.HashPasswordForStoringInConfigFile(str5 + str2, "MD5").ToLower());
                base.Response.Redirect(url);
            }
            else if (base.Request.Form["bankCardType"] == "01")
            {
                string str7 = base.Request.Form["cardNo"];
                string str8 = base.Request.Form["cardPwd"];
                string str9 = base.Request.Form["channel"];
                ///todo:这里是否有问题
                string str10 = base.Request.Form["facevalue"];
                string str11 = "0";
                string str12 = "test";
                str5 = string.Format("type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}", new object[] { str9, str, str7, str8, str10, str11, orderid, callBackurl });
                string url = string.Format(string.Concat(new object[] { "http://", base.Request.ServerVariables["HTTP_HOST"], "/card/index.aspx?{0}&attach={1}&sign={2}" }), str5, str12, FormsAuthentication.HashPasswordForStoringInConfigFile(str5 + str2, "MD5").ToLower());
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string str13 = reader.ReadToEnd();
                    reader.Dispose();
                    reader.Close();
                    response.Close();
                    if (str13.Trim() == "opstate=0")
                    {
                        base.Response.Write("支付成功.");
                    }
                    if (str13.Trim() == "opstate=-1")
                    {
                        base.Response.Write("请求参数无效。");
                    }
                    if (str13.Trim() == "opstate=-2")
                    {
                        base.Response.Write("签名错误。");
                    }
                    if (str13.Trim() == "opstate=-3")
                    {
                        base.Response.Write("提交的卡密为重复提交，系统不进行消耗并进入下行流程。");
                    }
                    if (str13.Trim() == "opstate=-4")
                    {
                        base.Response.Write("提交的卡密不符合趣宝网定义的卡号密码面值规则。");
                    }
                    if (str13.Trim() == "opstate=-999")
                    {
                        base.Response.Write("接口维护中。");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string orderid = Guid.NewGuid().ToString().Substring(0, 20).Replace("-", "");
            string callBackurl = "http://" + base.Request.ServerVariables["HTTP_HOST"] + "/notify/interface/Bank_Notify.aspx";
            this.Eka365pay(orderid, callBackurl);
        }
    }
}

