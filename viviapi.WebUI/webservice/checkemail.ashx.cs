namespace viviapi.WebUI.webservice
{
    using System.Web;
    using System.Web.SessionState;
    using viviapi.Cache;
    using viviLib.Security;

    public class checkemail : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string s = "";
            string str2 = context.Request["email"];
            string str3 = context.Request["verifycode"];
            if (string.IsNullOrEmpty(str3))
            {
                s = "{\"result\":\"no\",\"ico\":\"error\",\"msg\":\"验证码不能为空！\"}";
            }
            else
            {
                string objId = "PHONE_VALID_" + str2;
                string str5 = (string)WebCache.GetCacheService().RetrieveObject(objId);
                if (str5 != str3)
                {
                    s = "{\"result\":\"no\",\"ico\":\"error\",\"msg\":\"验证码不正确！\"}";
                }
                else
                {
                    HttpContext.Current.Session["findpwduserok"] = Cryptography.MD5("yanzhengtongguook", "GB2312");
                    s = "{\"result\":\"ok\",\"ico\":\"success\",\"msg\":\"验证成功！\",\"url\":\"/setForm.aspx\"}";
                }
            }
            context.Response.ContentType = "application/json";
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

