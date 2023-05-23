namespace viviapi.WebUI.agent
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.User;
    using viviapi.WebComponents.Web;
    using viviLib.Security;

    public class ChangePwd : AgentPageBase
    {
        protected Button btnUpdate;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected Label lblMessage;
        protected HtmlInputPassword old_password;
        protected HtmlInputPassword pas;
        protected HtmlInputPassword re_password;

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string str = this.old_password.Value;
            if (string.IsNullOrEmpty(str))
            {
                this.lblMessage.Text = "请输入旧密码";
            }
            else
            {
                string str2 = this.pas.Value;
                if (string.IsNullOrEmpty(str2))
                {
                    this.lblMessage.Text = "请输入新密码";
                }
                else if (str2 != this.re_password.Value)
                {
                    this.lblMessage.Text = "二次密码不一致";
                }
                else if (Cryptography.MD5(str) != base.currentUser.Password)
                {
                    this.lblMessage.Text = "旧密码输入错误！请重试。";
                }
                else
                {
                    str2 = Cryptography.MD5(str2);
                    UserFactory.CurrentMember.Password = Cryptography.MD5(str2);
                    if (UserFactory.Update(UserFactory.CurrentMember, null))
                    {
                        this.lblMessage.Text = "修改成功！";
                    }
                    else
                    {
                        this.lblMessage.Text = "修改失败！请重试。";
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

