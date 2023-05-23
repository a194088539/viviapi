namespace viviapi.WebUI.Userlogin.Ajax
{
    using Newtonsoft.Json;
    using System;
    using System.Web;
    using System.Web.SessionState;
    using viviapi.BLL.User;
    using viviapi.Model.User;
    using viviLib;

    public class GetUserInfo_new : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        public string Mark(string num)
        {
            if (string.IsNullOrEmpty(num))
            {
                return string.Empty;
            }
            return ("*" + num.Substring(1));
        }

        public void ProcessRequest(HttpContext context)
        {
            string s = "";
            GetUserInfoResult result = new GetUserInfoResult();
            result.result = 0;
            result.username = string.Empty;
            result.name = string.Empty;
            result.msg = "未知错误";
            try
            {
                if (UserFactory.CurrentMember == null)
                {
                    result.msg = "登录信息失效，请重新登录";
                }
                else
                {
                    result.msg = "此账户不存在";
                    string str2 = XRequest.GetString("username");
                    if (!string.IsNullOrEmpty(str2))
                    {
                        UserInfo modelByName = UserFactory.GetModelByName(str2);
                        if (modelByName != null)
                        {
                            result.result = modelByName.ID;
                            result.username = str2;
                            result.name = this.Mark(modelByName.full_name);
                        }
                    }
                }
                s = JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            catch (Exception exception)
            {
                result.msg = exception.Message;
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

