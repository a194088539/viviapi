namespace viviapi.WebUI.webservice
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using viviapi.BLL;
    using viviapi.BLL.Tools;
    using viviapi.BLL.User;
    using viviapi.Cache;
    using viviapi.Model.User;

    public class sendMobileCode : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        private UserInfo _user = null;

        public void ProcessRequest(HttpContext context)
        {
            string s = "";
            string str2 = context.Request["mobile"];
            if (this.userInfo != null)
            {
                if (this.userInfo.IsPhonePass == 0)
                {
                    s = "{\"result\":false, \"text\":\"手机号码没有通过认证！\", \"time\":1.5}";
                }
                else
                {
                    string objId = "PHONE_VALID_" + this.userInfo.Tel;
                    string o = (string)WebCache.GetCacheService().RetrieveObject(objId);
                    if (o == null)
                    {
                        o = new Random().Next(10000, 99999).ToString();
                        WebCache.GetCacheService().AddObject(objId, o);
                    }
                    string msg = SysConfig.sms_temp_Authenticate;
                    msg = SysConfig.sms_temp_Modify.Replace("{@username}", this.userInfo.UserName).Replace("{@sitename}", WebInfoFactory.CurrentWebInfo.Name).Replace("{@authcode}", o);
                    if (string.IsNullOrEmpty(SMS.SendSmsWithCheck(this.userInfo.Tel, msg, "0")))
                    {
                        s = "{\"result\":true, \"text\":\"短信验证码发送成功！\", \"time\":1.5}";
                    }
                    else
                    {
                        s = "{\"result\":false, \"text\":\"短信验证码发送失败,请重新发送或联系客服！\", \"time\":1.5}";
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

