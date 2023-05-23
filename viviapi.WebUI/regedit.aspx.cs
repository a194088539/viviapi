using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.User;
using viviapi.Model.User;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.WebUI
{
    public class regedit : PageBase
    {
        protected HtmlForm form1;
        protected HtmlInputText newusername;
        protected HtmlInputPassword password1;
        protected HtmlInputPassword password2;
        protected HtmlInputText newfullname;
        protected HtmlInputText newqq;
        protected HtmlInputText newemail;
        protected Literal litError;

        public int m_Id
        {
            get
            {
                return WebBase.GetQueryStringInt32("s", 0);
            }
        }

        public int agent_Id
        {
            get
            {
                return WebBase.GetQueryStringInt32("agent", 0);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (viviapi.BLL.SysConfig.IsOpenRegistration)
                return;
            this.AlertAndRedirect("接口暂不开放注册", "index.aspx");
        }

        private string GetParamValue(string paramName)
        {
            return WebBase.GetFormString(paramName, string.Empty);
        }

        private void ShowError(string errMsg)
        {
            this.litError.Visible = true;
            this.litError.Text = string.Format("<br>&nbsp;&nbsp;<span class=\"txt_ERROR\">{0}</span><br>", (object)errMsg);
        }

        private string GetCheckEmailUrl(string parms)
        {
            return this.webInfo.Domain + "Userlogin/Ajax/mailCheckReceive.ashx?parms=" + parms;
        }

        private void SendMail(int UserId, string email)
        {
            string str = string.Empty;
            int num = new EmailCheck().Add(new EmailCheckInfo()
            {
                userid = UserId,
                status = EmailCheckStatus.提交中,
                addtime = new DateTime?(DateTime.Now),
                checktime = new DateTime?(DateTime.Now),
                email = email,
                typeid = EmailCheckType.注册,
                Expired = DateTime.Now.AddDays(7.0)
            });
            if (num <= 0)
                return;
            string domain = this.webInfo.Domain;
            string name = this.webInfo.Name;
            string kfqq = this.webInfo.Kfqq;
            string phone = this.webInfo.Phone;
            string parms = HttpUtility.UrlEncode(Cryptography.RijndaelEncrypt(string.Format("id={0}&", (object)num)));
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("<p>亲爱的{0}:<p>", (object)email);
            stringBuilder.AppendFormat("<p style=\"font-size:14px\">感谢您注册{0}，您需要在一周之内激活您的账号。 您只需点击下面的确认链接，即可完成帐号激活。</p><p>如果不是您本人的操作，可能是有用户误输入您的Email地址，您可以忽略此邮件或与{0}客服联系。</p>", (object)this.SiteName);
            stringBuilder.AppendFormat("<p><a href=\"{0}\" style=\"color:#003300\">{0}</a></p>", (object)this.GetCheckEmailUrl(parms));
            stringBuilder.Append("<p style=\"color:#999;font-size:12px\">如果无法点击该URL链接地址，请将它复制并粘帖到浏览器的地址输入框，然后单击回车即可。");
            stringBuilder.Append("<p><p>————————————————————————————————");
            stringBuilder.AppendFormat("<p style=\"font-size:14px;line-height:150%\">{1} {0} 如有疑问加我们的客服QQ {2} 或者来电咨询 {3}", (object)domain, (object)name, (object)kfqq, (object)phone);
            this.AlertAndRedirect(!new EmailHelper(string.Empty, email, email + "账号激活", stringBuilder.ToString(), true, Encoding.GetEncoding("gbk")).Send() ? "邮件发送失败，请联系管理员" : "注册成功,请登录邮箱激活账号!", "index.aspx");
        }

        protected void rbluserclass_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            if (this.Session["CCode"] == null)
                this.ShowError("验证码失效!");
            else if (!this.GetParamValue("txtvcode").ToLower().Equals(this.Session["CCode"].ToString().ToLower()))
            {
                this.ShowError("验证码错误!");
            }
            else
            {
                int num = 0;
                string str1 = this.GetParamValue("newusername").Trim();
                string strToEncrypt = this.GetParamValue("password1").Trim();
                string str2 = this.GetParamValue("password2").Trim();
                string email = this.GetParamValue("newemail").Trim();
                string str3 = this.GetParamValue("newsitename").Trim();
                string str4 = this.GetParamValue("newsiteurl").Trim();
                string str5 = "";
                string str6 = string.Empty;
                string str7 = this.GetParamValue("newfullname").Trim();
                string str8 = this.GetParamValue("newqq").Trim();
                string str9 = string.Empty;
                string str10 = string.Empty;
                string str11 = this.GetParamValue("fBank").Trim();
                this.GetParamValue("fAccountName").Trim();
                this.GetParamValue("fAccount").Trim();
                this.GetParamValue("fProvince").Trim();
                this.GetParamValue("fCity").Trim();
                this.GetParamValue("fSubBranch").Trim();
                if (string.IsNullOrEmpty(str1))
                    errMsg = "用户名不能为空!";
                else if (!new Regex("^(?!_)(?!.*?_$)[a-zA-Z0-9_]+$").Match(str1).Success)
                    errMsg = "用户名格式不正确!";
                else if (UserFactory.Exists(str1))
                    errMsg = "用户名已注册!";
                if (string.IsNullOrEmpty(errMsg))
                {
                    if (string.IsNullOrEmpty(strToEncrypt))
                        errMsg = "密码不能为空!";
                    else if (strToEncrypt.Length < 6 || strToEncrypt.Length > 32)
                        errMsg = "密码长度不正确!";
                    else if (!strToEncrypt.Equals(str2))
                        errMsg = "密码与重复密码输入不一致!";
                }
                if (string.IsNullOrEmpty(errMsg))
                {
                    if (string.IsNullOrEmpty(email))
                        errMsg = "安全邮箱不能为空!";
                    else if (!viviLib.Text.Validate.IsEmail(email))
                        errMsg = "安全邮箱格式不正确!";
                }
                if (string.IsNullOrEmpty(errMsg))
                {
                    if (string.IsNullOrEmpty(str7))
                        errMsg = "真实姓名不能不空!";
                    else if (string.IsNullOrEmpty(str8))
                        errMsg = "联系QQ不能不空!";
                }
                if (!string.IsNullOrEmpty(errMsg))
                    ;
                if (!string.IsNullOrEmpty(errMsg))
                {
                    this.ShowError(errMsg);
                }
                else
                {
                    UserInfo _userinfo = new UserInfo();
                    _userinfo.UserName = str1;
                    _userinfo.Password = Cryptography.MD5(strToEncrypt);
                    _userinfo.Email = email;
                    _userinfo.QQ = str8;
                    _userinfo.Tel = str5;
                    _userinfo.SiteName = str3;
                    _userinfo.SiteUrl = str4;
                    _userinfo.IdCard = str6;
                    _userinfo.CPSDrate = viviapi.BLL.SysConfig.DefaultCPSDrate;
                    _userinfo.PMode = 1;
                    _userinfo.PayeeBank = str11;
                    _userinfo.LinkMan = str7;
                    _userinfo.full_name = str7;
                    _userinfo.Status = viviapi.BLL.SysConfig.IsAudit ? 1 : 2;
                    _userinfo.LastLoginIp = ServerVariables.TrueIP;
                    _userinfo.LastLoginTime = DateTime.Now;
                    _userinfo.RegTime = DateTime.Now;
                    _userinfo.Settles = viviapi.BLL.SysConfig.DefaultSettledMode;
                    _userinfo.CPSDrate = viviapi.BLL.SysConfig.DefaultCPSDrate;
                    _userinfo.AgentId = 0;
                    _userinfo.APIAccount = 0L;
                    _userinfo.APIKey = Guid.NewGuid().ToString("N");
                    _userinfo.question = str9;
                    _userinfo.answer = str10;
                    _userinfo.classid = num;
                    if (this.m_Id > 0)
                        _userinfo.manageId = new int?(this.m_Id);
                    int UserId = UserFactory.Add(_userinfo);
                    if (UserId > 0)
                    {
                        if (this.agent_Id > 1)
                            PromotionUserFactory.Insert(new PromotionUserInfo()
                            {
                                PID = this.agent_Id,
                                Prices = new Decimal(5, 0, 0, false, (byte)1),
                                RegId = UserId,
                                PromTime = DateTime.Now,
                                PromStatus = 1
                            });
                        if (viviapi.BLL.SysConfig.RegistrationActivationByEmail)
                            this.SendMail(UserId, email);
                        else if (!viviapi.BLL.SysConfig.IsAudit)
                            this.AlertAndRedirect("注册成功,无须审核请登陆后直接使用！", "/Userlogin/account/index.aspx");
                        else
                            this.AlertAndRedirect("注册成功,请等待管理员审核！", "login.aspx");
                    }
                    else
                        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "AlertAndRedirect", "<SCRIPT LANGUAGE='javascript'>alert('注册失败！');</SCRIPT>");
                }
            }
        }
    }
}
