namespace viviapi.WebUI.Merchant.WS
{
    using Newtonsoft.Json;
    using System.Web;
    using viviapi.BLL.User;
    using viviapi.Model.User;
    using viviLib.Web;

    public class GetUserInfo : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string s = "fail";
            try
            {
                string formString = WebBase.GetFormString("token", string.Empty);
                if (!string.IsNullOrEmpty(formString))
                {
                    int userIdByToken = UserFactory.GetUserIdByToken(formString);
                    if (userIdByToken > 0)
                    {
                        UserInfo model = UserFactory.GetModel(userIdByToken);
                        if (model != null)
                        {
                            s = "success" + JsonConvert.SerializeObject(model, Formatting.Indented);
                        }
                    }
                }
            }
            catch
            {
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

