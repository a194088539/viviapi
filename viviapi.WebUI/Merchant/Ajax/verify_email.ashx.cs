namespace viviapi.WebUI.Merchant.Ajax
{
    using System;
    using System.Web;
    using viviapi.BLL.User;
    using viviapi.Model.User;
    using viviLib.Security;
    using viviLib.Web;

    public class verify_email : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string s = "";
            try
            {
                string[] strArray = Cryptography.RijndaelDecrypt(this.parms).Split(new char[] { '&' });
                if (strArray.Length == 2)
                {
                    int result = 0;
                    if (int.TryParse(strArray[0].Split(new char[] { '=' })[1], out result))
                    {
                        EmailCheck check = new EmailCheck();
                        EmailCheckInfo model = check.GetModel(result);
                        if (((model == null) || (model.status != EmailCheckStatus.提交中)) || (model.Expired < DateTime.Now))
                        {
                            s = "无效的信息或此链接已失效";
                        }
                        else
                        {
                            model.checktime = new DateTime?(DateTime.Now);
                            model.status = EmailCheckStatus.已审核;
                            if (check.Update(model))
                            {
                                s = "激活成功 请登录";
                            }
                        }
                    }
                }
            }
            catch
            {
                s = "提交无效的参数";
            }
            string str3 = string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nalert({0});\r\nlocation.href='/index.aspx';\r\n//--></SCRIPT>\r\n", AntiXss.JavaScriptEncode(s));
            HttpContext.Current.Response.Write(str3);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string parms
        {
            get
            {
                return WebBase.GetQueryStringString("parms", "");
            }
        }
    }
}

