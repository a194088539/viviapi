namespace viviapi.WebUI.webservice
{
    using System.Web;
    using System.Web.SessionState;
    using viviapi.BLL.User;

    public class checkuser : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string s = "";
            string str2 = context.Request["username"];
            string str3 = context.Request["captcha"];
            if (!string.IsNullOrEmpty(str2))
            {
                if (string.IsNullOrEmpty(str3))
                {
                    s = "{result:false, text:'验证码不能为空!', ok:true}";
                }
                else if (HttpContext.Current.Session["CCode"].ToString() != str3.ToUpper())
                {
                    s = "{result:false, text:'验证码不正确！', ok:true}";
                }
                else if (string.IsNullOrEmpty(str2))
                {
                    s = "{result:false, text:'用户名不能为空！', ok:true}";
                }
                if (UserFactory.GetModelByName(str2).ID <= 0)
                {
                    s = "{result:false, text:'用户不存在！', ok:true}";
                }
                else
                {
                    HttpContext.Current.Session["findpwduser"] = str2;
                    s = "{result:true, text:'验证成功',  time:1, url:'/selectForm.aspx'}";
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
    }
}

