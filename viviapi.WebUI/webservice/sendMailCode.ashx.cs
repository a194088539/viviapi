namespace viviapi.WebUI.webservice
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;
    using viviapi.BLL.User;
    using viviapi.Cache;
    using viviapi.Model.User;
    using viviapi.WebComponents;
    using viviapi.WebComponents.Template;

    public class sendMailCode : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        private UserInfo _user = null;

        public void ProcessRequest(HttpContext context)
        {
            string s = "";
            string to = context.Request["email"];
            if (this.userInfo != null)
            {
                if (this.userInfo.Email != to)
                {
                    s = "{\"result\":false, \"text\":\"填写的邮箱与实际注册邮箱不符！\", \"time\":1.5}";
                }
                else if (this.userInfo.IsEmailPass == 0)
                {
                    s = "{\"result\":false, \"text\":\"邮箱没有认证！\", \"time\":1.5}";
                }
                else
                {
                    string emailCheckTemp = Helper.GetEmailCheckTemp();
                    string objId = "PHONE_VALID_" + to;
                    string o = (string)WebCache.GetCacheService().RetrieveObject(objId);
                    if (o == null)
                    {
                        o = new Random().Next(0x2710, 0x1869f).ToString();
                        WebCache.GetCacheService().AddObject(objId, o);
                    }
                    StringBuilder builder = new StringBuilder();
                    builder.AppendFormat("<p>亲爱的{0}:<p>", this.userInfo.UserName);
                    builder.AppendFormat("<p style=\"font-size:14px\">您正常通过邮箱找回密码</p>", new object[0]);
                    builder.AppendFormat("<p style=\"font-size:14px\">您的验证码为：<font style=\"font-size:14px;font-weight:bold;color:blue\">{0}</font>，打死也不能告诉别人！</p>", o);
                    EmailHelper helper = new EmailHelper(string.Empty, to, to + "验证码", builder.ToString(), true, Encoding.GetEncoding("gbk"));
                    if (helper.Send())
                    {
                        s = "{\"result\":true, \"text\":\"邮箱验证码发送成功！\", \"time\":1.5}";
                    }
                    else
                    {
                        s = "{\"result\":false, \"text\":\"邮箱验证码发送失败,请重新发送或联系客服！\", \"time\":1.5}";
                    }
                }
            }
            else
            {
                s = "{\"result\":false, \"text\":\"数据不存在！\", \"time\":1.5}";
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

