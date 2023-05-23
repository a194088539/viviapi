namespace viviapi.WebUI.webservice
{
    using System.Web;
    using System.Web.SessionState;
    using viviapi.BLL.User;
    using viviapi.Model.User;
    using viviLib.Security;

    public class setForm : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        private UserInfo _user = null;

        public void ProcessRequest(HttpContext context)
        {
            string s = "";
            string str4 = context.Request["password"];
            string str5 = context.Request["password2"];
            if (this.username == "")
            {
                s = "{\"result\":\"no\",\"ico\":\"error\",\"msg\":\"验证超时！\",\"url\":\"/getPassword.html\"}";
            }
            else if (this.userInfo == null)
            {
                s = "{\"result\":\"no\",\"ico\":\"error\",\"msg\":\"验证超时！\",\"url\":\"/getPassword.html\"}";
            }
            if (this.renzheng == "")
            {
                s = "{\"result\":\"no\",\"ico\":\"error\",\"msg\":\"验证超时！\",\"url\":\"/getPassword.html\"}";
            }
            else if (HttpContext.Current.Session["findpwduserok"].ToString() != Cryptography.MD5("yanzhengtongguook", "GB2312"))
            {
                s = "{\"result\":\"no\",\"ico\":\"error\",\"msg\":\"验证超时！\",\"url\":\"/getPassword.html\"}";
            }
            else if (string.IsNullOrEmpty(str4))
            {
                s = "{\"result\":\"no\",\"ico\":\"error\",\"msg\":\"密码不能为空！\",\"url\":\"/getPassword.html\"}";
            }
            else if (string.IsNullOrEmpty(str5))
            {
                s = "{\"result\":\"no\",\"ico\":\"error\",\"msg\":\"两次密码不能为空！\",\"url\":\"/getPassword.html\"}";
            }
            else if (str4 != str5)
            {
                s = "{\"result\":\"no\",\"ico\":\"error\",\"msg\":\"两次密码不一致！请重新输入\",\"url\":\"/getPassword.html\"}";
            }
            else
            {
                this.userInfo.Password = str4;
                if (UserFactory.Update(this.userInfo, null))
                {
                    HttpContext.Current.Session["findpwduserok"] = null;
                    HttpContext.Current.Session["findpwduser"] = null;
                    s = "{\"result\":\"ok\",\"ico\":\"success\",\"msg\":\"修改成功！\", \"url\":\"/index.aspx\"}";
                }
                else
                {
                    s = "{\"result\":\"no\",\"ico\":\"error\",\"msg\":\"修改失败！\",\"url\":\"/index.aspx\"}";
                }
            }
            HttpContext.Current.Response.ContentType = "text/html";
            context.Response.Write(s);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string renzheng
        {
            get
            {
                if (HttpContext.Current.Session["findpwduserok"] != null)
                {
                    return HttpContext.Current.Session["findpwduserok"].ToString();
                }
                return string.Empty;
            }
        }

        public UserInfo userInfo
        {
            get
            {
                if (!(string.IsNullOrEmpty(this.username) || (this._user != null)))
                {
                    this._user = UserFactory.GetModelByName(this.username);
                }
                return this._user;
            }
        }

        public string username
        {
            get
            {
                if (HttpContext.Current.Session["findpwduser"] != null)
                {
                    return HttpContext.Current.Session["findpwduser"].ToString();
                }
                return string.Empty;
            }
        }
    }
}

