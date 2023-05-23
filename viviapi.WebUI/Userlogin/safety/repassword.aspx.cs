namespace viviapi.WebUI.Userlogin.safety
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.User;
    using viviapi.WebComponents.Web;
    using viviLib.Security;

    public class repassword : UserPageBase
    {
        protected Button btnSave;
        protected HtmlForm form1;
        protected HtmlInputPassword txtnewpassword;
        protected HtmlInputPassword txtoldpassword;
        protected HtmlInputPassword txtrepassword;
        protected HtmlInputText txtuserid;
        protected HtmlInputText txtusername;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            string strToEncrypt = this.txtoldpassword.Value.Trim();
            string str3 = this.txtnewpassword.Value.Trim();
            string str4 = this.txtrepassword.Value.Trim();
            if (Cryptography.MD5(strToEncrypt) != base.currentUser.Password)
            {
                str = "旧密码不正确";
            }
            else if (str3 != str4)
            {
                str = "两次密码不一致";
            }
            else if (str3 == strToEncrypt)
            {
                str = "新密码不能为新一样";
            }
            if (string.IsNullOrEmpty(str))
            {
                base.currentUser.Password = Cryptography.MD5(str3);
                if (UserFactory.Update(base.currentUser, null))
                {
                    str = "true";
                }
                else
                {
                    str = "更新失败";
                }
            }
            if (str.Equals("true"))
            {
                base.AlertAndRedirect("修改成功");
            }
            else
            {
                base.AlertAndRedirect(str);
            }
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

