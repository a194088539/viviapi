namespace viviapi.WebUI.Managements
{
    using System;
    using System.IO;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Tools;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib;

    public class DataBackup : ManagePageBase
    {
        protected Button bt_sub;
        protected Button btnClear;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected Label Label1;
        protected Label lbmsg;
        protected TextBox txtfilepath;
        protected TextBox txtfilname;

        protected void bt_sub_Click(object sender, EventArgs e)
        {
            string text = this.txtfilepath.Text;
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            if (db.Backup(text + this.txtfilname.Text))
            {
                this.lbmsg.Text = "备份成功！";
            }
            else
            {
                this.lbmsg.Text = "备份失败！";
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack && !base.IsPostBack)
            {
                this.txtfilname.Text = XRequest.GetHost() + DateTime.Now.ToString("yyyyMMddHHmmssff") + ".bak";
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

