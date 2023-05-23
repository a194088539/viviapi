namespace viviapi.WebUI.Merchant.WS
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using viviapi.BLL.User;
    using viviapi.Model.User;
    using viviapi.WebUI;
    using viviLib.Security;
    using viviLib.Web;

    public class SignIn : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string s = string.Empty;
            try
            {
                string formString = WebBase.GetFormString("txtusername", string.Empty);
                string str3 = WebBase.GetFormString("txtpassword", string.Empty);
                string ip = WebBase.GetFormString("ip", string.Empty);
                if (string.IsNullOrEmpty(formString))
                {
                    s = "请输入商户名!";
                }
                else if (string.IsNullOrEmpty(str3))
                {
                    s = "请输入商户密码!";
                }
                else
                {
                    UserInfo userinfo = new UserInfo();
                    userinfo.UserName = formString;
                    userinfo.Password = Cryptography.MD5(str3);
                    userinfo.LastLoginIp = ip;
                    userinfo.LastLoginTime = DateTime.Now;
                    userinfo.LastLoginAddress = WebUtility.GetIPAddress(ip);
                    userinfo.LastLoginRemark = "客户端登录";
                    userinfo.loginType = 1;
                    s = UserFactory.SignIn(userinfo);
                    if (userinfo.ID > 0)
                    {
                        s = "success," + HttpContext.Current.Session["{10E6C4EE-54C1-4895-8CDE-202A5B3DD9E9}"].ToString();
                    }
                }
            }
            catch
            {
                s = "login fail";
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(s);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

