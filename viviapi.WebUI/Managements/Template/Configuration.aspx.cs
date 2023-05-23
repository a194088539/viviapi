namespace viviapi.WebUI.Managements.Template
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;

    public class Configuration : Page
    {
        protected Button btn_Update;
        protected HtmlForm form1;
        protected TextBox txtAuthenticate;
        protected TextBox txtFindPwd;
        protected TextBox txtModify;
        protected TextBox txtRegister;
        protected TextBox txttoCash;

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SysConfig.Update(0x35, this.txtRegister.Text);
            SysConfig.Update(0x37, this.txtModify.Text);
            SysConfig.Update(0x36, this.txtAuthenticate.Text);
            SysConfig.Update(0x38, this.txtFindPwd.Text);
            SysConfig.Update(60, this.txttoCash.Text);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtRegister.Text = SysConfig.sms_temp_Register;
                this.txtModify.Text = SysConfig.sms_temp_Modify;
                this.txtAuthenticate.Text = SysConfig.sms_temp_Authenticate;
                this.txtFindPwd.Text = SysConfig.sms_temp_FindPwd;
                this.txttoCash.Text = SysConfig.sms_temp_tocash;
            }
        }
    }
}

