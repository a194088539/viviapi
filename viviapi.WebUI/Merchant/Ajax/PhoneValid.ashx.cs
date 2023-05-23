namespace viviapi.WebUI.Merchant.Ajax
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using viviapi.BLL;
    using viviapi.BLL.Tools;
    using viviapi.BLL.User;
    using viviapi.Cache;
    using viviLib;
    using viviLib.Text;

    public class PhoneValid : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string s = string.Empty;
            if (UserFactory.CurrentMember == null)
            {
                s = "登录信息失效，请重新登录";
            }
            else
            {
                string str2 = XRequest.GetString("phone");
                if (!string.IsNullOrEmpty(str2))
                {
                    if (str2 == UserFactory.CurrentMember.Tel)
                    {
                        s = "不能为原手机号码一样";
                    }
                    else if (Validate.IsMobileNum(str2))
                    {
                        bool flag = UserFactory.CurrentMember.IsPhonePass == 1;
                        string objId = "PHONE_VALID_" + str2;
                        string o = (string)WebCache.GetCacheService().RetrieveObject(objId);
                        if (o == null)
                        {
                            o = new Random().Next(0x2710, 0x1869f).ToString();
                            WebCache.GetCacheService().AddObject(objId, o);
                        }
                        string msg = SysConfig.sms_temp_Authenticate;
                        if (flag)
                        {
                            msg = SysConfig.sms_temp_Modify;
                        }
                        msg = msg.Replace("{@username}", UserFactory.CurrentMember.UserName).Replace("{@sitename}", WebInfoFactory.CurrentWebInfo.Name).Replace("{@authcode}", o);
                        string str6 = SMS.SendSmsWithCheck(str2, msg, "");
                        if (string.IsNullOrEmpty(str6))
                        {
                            str6 = "true";
                        }
                        else
                        {
                            s = str6;
                        }
                    }
                    else
                    {
                        s = "请输入正确的手机号码";
                    }
                }
                else
                {
                    s = "请输入手机号码";
                }
                context.Response.ContentType = "text/plain";
                context.Response.Write(s);
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

