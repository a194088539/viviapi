using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.SessionState;
using viviapi.BLL.User;
using viviapi.Model.User;
using viviLib;

namespace viviapi.WebUI.LongBao.merchant.Ajax
{
    public class GetUserInfo : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string Mark(string num)
        {
            if (string.IsNullOrEmpty(num))
                return string.Empty;
            return "*" + num.Substring(1);
        }

        public void ProcessRequest(HttpContext context)
        {
            string s = "";
            GetUserInfoResult getUserInfoResult = new GetUserInfoResult();
            getUserInfoResult.result = 0;
            getUserInfoResult.username = string.Empty;
            getUserInfoResult.name = string.Empty;
            getUserInfoResult.msg = "未知错误";
            try
            {
                if (UserFactory.CurrentMember == null)
                {
                    getUserInfoResult.msg = "登录信息失效，请重新登录";
                }
                else
                {
                    getUserInfoResult.msg = "此账户不存在";
                    string @string = XRequest.GetString("username");
                    if (!string.IsNullOrEmpty(@string))
                    {
                        UserInfo modelByName = UserFactory.GetModelByName(@string);
                        if (modelByName != null)
                        {
                            getUserInfoResult.result = modelByName.ID;
                            getUserInfoResult.username = @string;
                            getUserInfoResult.name = this.Mark(modelByName.full_name);
                        }
                    }
                }
                s = JsonConvert.SerializeObject((object)getUserInfoResult, Formatting.Indented);
            }
            catch (Exception ex)
            {
                getUserInfoResult.msg = ex.Message;
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(s);
        }
    }
}
