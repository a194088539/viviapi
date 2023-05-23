namespace viviapi.WebUI.Userlogin.Ajax
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using viviapi.BLL.User;
    using viviapi.Model.User;
    using viviapi.WebUI;
    using viviLib.Security;
    using viviLib.Web;

    public class Login_new : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string formString = WebBase.GetFormString("username", string.Empty);
            string userPwd = WebBase.GetFormString("password", string.Empty);
            string code = WebBase.GetFormString("imycode", string.Empty);
            string s = this.SignIn(context, formString, userPwd, code);
            context.Response.ContentType = "text/plain";
            context.Response.Write(s);
        }

        protected string SignIn(HttpContext context, string userName, string userPwd, string code)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(code))
            {
                return "请输入验证码!";
            }
            if (string.IsNullOrEmpty(userName))
            {
                return "请输入商户名!";
            }
            if (string.IsNullOrEmpty(userPwd))
            {
                return "请输入商户密码!";
            }
            if (context.Session["CCode"] == null)
            {
                return "验证码失效!";
            }
            if (context.Session["CCode"].ToString().ToUpper() != code.ToUpper())
            {
                return "验证码不正确!";
            }
            UserInfo userinfo = new UserInfo();
            userinfo.UserName = userName;
            userinfo.Password = Cryptography.MD5(userPwd);
            userinfo.LastLoginIp = ServerVariables.TrueIP;
            userinfo.LastLoginTime = DateTime.Now;
            userinfo.LastLoginAddress = WebUtility.GetIPAddress(userinfo.LastLoginIp);
            userinfo.LastLoginRemark = WebUtility.GetIPAddressInfo(userinfo.LastLoginIp);
            str = UserFactory.SignIn(userinfo);
            if (userinfo.ID > 0)
            {
                str = "ok";
            }
            return str;
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

