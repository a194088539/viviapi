namespace viviapi.WebUI.Userlogin.Ajax
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;
    using viviapi.BLL.User;
    using viviapi.Cache;
    using viviapi.Model.User;
    using viviapi.WebComponents;
    using viviLib;
    using viviLib.Security;

    public class SendVerifyCode : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string s = string.Empty;
            if (this.currentUser == null)
            {
                s = "登录信息失效，请重新登录";
            }
            else
            {
                string str2 = XRequest.GetString("phone");
                string objId = "PHONE_VALID_" + str2;
                string o = new Random().Next(0x2710, 0x1869f).ToString();
                WebCache.GetCacheService().AddObject(objId, o);
                string email = UserFactory.CurrentMember.Email;
                EmailCheckInfo model = new EmailCheckInfo();
                model.userid = this.currentUser.ID;
                model.status = EmailCheckStatus.提交中;
                model.addtime = new DateTime?(DateTime.Now);
                model.checktime = new DateTime?(DateTime.Now);
                model.email = email;
                model.typeid = EmailCheckType.验证码;
                model.Expired = DateTime.Now.AddDays(7.0);
                int num = new EmailCheck().Add(model);
                if (num > 0)
                {
                    string str7 = HttpUtility.UrlEncode(Cryptography.RijndaelEncrypt(string.Format("id={0}&", num)));
                    StringBuilder builder = new StringBuilder();
                    builder.AppendFormat("<p>亲爱的{0}:<p>", email);
                    builder.AppendFormat("<p style=\"font-size:14px\">您本次验证码为：</p><p><a href=\"{0}\" style=\"color:#003300\">{0}</a></p>", o);
                    builder.Append("<p style=\"color:rgb(255, 0, 0);font-size:12px\">如果不是本人操作，请及时修改帐户密码");
                    builder.Append("<p><p>————————————————————————————————");
                    EmailHelper helper = new EmailHelper(string.Empty, email, email + "验证码", builder.ToString(), true, Encoding.GetEncoding("gbk"));
                    if (helper.Send())
                    {
                        s = "true";
                    }
                    else
                    {
                        s = "false";
                    }
                }
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(s);
        }

        public UserInfo currentUser
        {
            get
            {
                return UserFactory.CurrentMember;
            }
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

