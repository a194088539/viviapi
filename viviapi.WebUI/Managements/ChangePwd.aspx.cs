namespace viviapi.WebUI.Managements
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.WebComponents.Web;
    using viviLib.Security;

    public class ChangePwd : ManagePageBase
    {
        protected Button btnUpdate;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected Label lblMessage;
        protected HtmlInputPassword newsedpwd;
        protected HtmlInputPassword newsedpwd2;
        protected HtmlInputPassword old_password;
        protected HtmlInputPassword oldsedpwd;
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
                else if (Cryptography.MD5(str) != base.currentManage.password)
                {
                    this.lblMessage.Text = "旧密码输入错误！请重试。";
                }
                else
                {
                    str2 = Cryptography.MD5(str2);
                    base.currentManage.password = str2;
                    str = this.oldsedpwd.Value;
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (Cryptography.MD5(str) != base.currentManage.secondpwd)
                        {
                            this.lblMessage.Text = "旧二级密码输入错误！请重试。";
                            return;
                        }
                        str2 = this.newsedpwd.Value;
                        if (string.IsNullOrEmpty(str2))
                        {
                            this.lblMessage.Text = "请输入新二级密码";
                            return;
                        }
                        if (str2 != this.newsedpwd2.Value)
                        {
                            this.lblMessage.Text = "二次二级密码不一致";
                            return;
                        }
                        str2 = Cryptography.MD5(str2);
                        base.currentManage.secondpwd = str2;
                    }
                    if (ManageFactory.Update(base.currentManage))
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
            this.setPower();
        }

        private void setPower()
        {
        }
    }
}

