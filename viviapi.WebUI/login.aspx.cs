namespace viviapi.WebUI
{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.User;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;
    using viviLib.Security;
    using viviLib.Web;

    public class login : PageBase
    {
        protected HtmlForm form1;
        protected ImageButton ImageButton2;
        protected TextBox password;
        protected HtmlInputText username;

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            string str = base.Request["imycode"];
            string str2 = base.Request["username"];
            string str3 = base.Request["password"];
            string str4 = base.Request["ckbsavepass"];
            string str5 = string.Empty;
            if (string.IsNullOrEmpty(str))
            {
                str5 = str5 + "请输入验证码!";
            }
            else if (string.IsNullOrEmpty(str2))
            {
                str5 = "请输入商户名!";
            }
            else if (string.IsNullOrEmpty(str3))
            {
                str5 = "请输入商户密码!";
            }
            else if (this.Session["CCode"] == null)
            {
                str5 = "验证码失效!";
            }
            else if (this.Session["CCode"].ToString().ToUpper() != str.ToUpper())
            {
                str5 = "验证码不正确!";
            }
            else
            {
                HttpCookie cookie;
                UserInfo userinfo = new UserInfo();
                if (SysConfig.isUserloginByEmail == "1")
                {
                    userinfo.Email = str2;
                    userinfo.UserName = str2;
                }
                else
                {
                    userinfo.UserName = str2;
                }
                if (base.Request.Cookies["yklm_user"] != null)
                {
                    userinfo.Password = base.Request.Cookies["yklm_user"]["userpass"].ToString();
                }
                else
                {
                    userinfo.Password = Cryptography.MD5(str3);
                }
                userinfo.LastLoginIp = ServerVariables.TrueIP;
                userinfo.LastLoginTime = DateTime.Now;
                userinfo.LastLoginAddress = WebUtility.GetIPAddress(userinfo.LastLoginIp);
                userinfo.LastLoginRemark = WebUtility.GetIPAddressInfo(userinfo.LastLoginIp);
                str5 = UserFactory.SignIn(userinfo);
                if (((userinfo.ID > 0) && SysConfig.RegistrationActivationByEmail) && (userinfo.IsEmailPass == 0))
                {
                    str5 = "您的账号尚未激活，请进入你注册时使用的邮箱激活账号。";
                }
                if (str4 != null)
                {
                    cookie = new HttpCookie("yklm_user");
                    cookie.Expires.AddMonths(3);
                    cookie.Values.Add("username", userinfo.UserName);
                    cookie.Values.Add("userpass", userinfo.Password);
                    base.Response.AppendCookie(cookie);
                }
                else
                {
                    cookie = new HttpCookie("yklm_user");
                    cookie.Expires = DateTime.Now.AddMonths(-24);
                    base.Response.Cookies.Add(cookie);
                }
            }
            if (str5 != "登录成功")
            {
                string script = "<SCRIPT LANGUAGE='javascript'>alert('" + str5 + "');</SCRIPT>";
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "AlertAndRedirect", script);
            }
            else
            {
                base.Response.Redirect("/Userlogin/account/index.aspx", false);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && (base.Request.Cookies["yklm_user"] != null))
            {
                this.username.Value = base.Request.Cookies["yklm_user"]["username"].ToString();
                this.password.Attributes["value"] = "**********";
            }
        }
    }
}

