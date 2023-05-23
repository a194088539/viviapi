namespace viviapi.WebUI.Merchant
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.User;
    using viviapi.WebComponents.Web;
    using viviLib.Security;

    public class cashpass : UserPageBase
    {
        protected Button btnSave;
        protected HtmlInputPassword txtcashpass;
        protected HtmlInputPassword txtloginpwd;
        protected HtmlInputText txtmail;
        protected HtmlInputPassword txtrecashpass;
        protected HtmlInputText txtuserid;
        protected HtmlInputText txtusername;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            string strToEncrypt = this.txtloginpwd.Value;
            string str3 = this.txtmail.Value;
            string str4 = this.txtcashpass.Value;
            string str5 = this.txtrecashpass.Value;
            if (Cryptography.MD5(strToEncrypt) != base.currentUser.Password)
            {
                str = "登录密码不正确";
            }
            if (str3 != UserFactory.CurrentMember.Email)
            {
                str = "安全邮箱不正确";
            }
            if (str4 != str5)
            {
                str = "两次密码输入不一致";
            }
            if (string.IsNullOrEmpty(str))
            {
                base.currentUser.Password2 = Cryptography.MD5(str4);
                if (base.currentUser.Password2 == base.currentUser.Password)
                {
                    str = "登录密码与提现密码不能相同";
                }
                else if (UserFactory.Update(UserFactory.CurrentMember, null))
                {
                    str = "设置成功!";
                }
                else
                {
                    str = "更新失败";
                }
            }
            base.AlertAndRedirect(str);
        }

        private void InitForm()
        {
            this.txtuserid.Attributes["readonly"] = "true";
            this.txtusername.Attributes["readonly"] = "true";
            this.txtuserid.Value = base.currentUser.ID.ToString();
            this.txtusername.Value = base.currentUser.UserName;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitForm();
            }
        }
    }
}

