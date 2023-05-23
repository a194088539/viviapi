namespace viviapi.WebUI.Managements
{
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents;
    using viviapi.WebComponents.Web;

    public class emailconfig : ManagePageBase
    {
        protected Button btn_Update;
        protected Button btnSend;
        protected CheckBox ckb_ssl;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected TextBox txtContent;
        protected TextBox txtEmailServerAddress;
        protected TextBox txtEmailServerAddressPort;
        protected TextBox txtMailDisplayName;
        protected TextBox txtmailfrom;
        protected TextBox txtmailto;
        protected TextBox txtServerUserName;
        protected TextBox txtServerUserPass;

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            EmailHelper helper = new EmailHelper(string.Empty, this.txtmailto.Text.Trim(), this.txtmailto.Text.Trim() + "邮件测试", this.txtContent.Text.Trim(), true, Encoding.GetEncoding("gbk"));
            if (helper.Send())
            {
                msg = "发送成功";
            }
            else
            {
                msg = "发送失败";
            }
            base.AlertAndRedirect(msg);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SysConfig.Update(0x3d, this.txtEmailServerAddress.Text);
            SysConfig.Update(0x3e, this.txtEmailServerAddressPort.Text);
            SysConfig.Update(0x3f, this.txtServerUserName.Text);
            if (!string.IsNullOrEmpty(this.txtServerUserPass.Text))
            {
                SysConfig.Update(0x40, this.txtServerUserPass.Text);
            }
            SysConfig.Update(0x42, this.txtmailfrom.Text);
            SysConfig.Update(0x44, this.txtMailDisplayName.Text);
            if (this.ckb_ssl.Checked)
            {
                SysConfig.Update(0x43, "1");
            }
            else
            {
                SysConfig.Update(0x43, "0");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.txtEmailServerAddress.Text = SysConfig.MailDomain;
                this.txtEmailServerAddressPort.Text = SysConfig.MailDomainPort.ToString();
                this.txtServerUserName.Text = SysConfig.MailServerUserName;
                this.txtServerUserPass.Text = SysConfig.MailServerPassWord;
                this.txtmailfrom.Text = SysConfig.MailFrom;
                this.txtMailDisplayName.Text = SysConfig.MailDisplayName;
                this.ckb_ssl.Checked = SysConfig.MailIsSsl == "1";
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.System))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }
    }
}

