namespace viviapi.WebUI.Managements.Jubao
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviapi.WebUI;
    using viviLib.Web;

    public class ItemModi : ManagePageBase
    {
        public JuBaoInfo _ItemInfo = null;
        protected Button btnOK;
        protected DropDownList ddlstatus;
        protected DropDownList ddltype;
        protected HtmlForm form1;
        protected Label lblid;
        protected TextBox txtaddtime;
        protected TextBox txtcheck;
        protected TextBox txtcheckremark;
        protected TextBox txtchecktime;
        protected TextBox txtemail;
        protected TextBox txtfield1;
        protected TextBox txtname;
        protected TextBox txtpwd;
        protected TextBox txtremark;
        protected TextBox txttel;
        protected TextBox txturl;

        protected void btnOK_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.ShowInfo();
            }
        }

        private void Save()
        {
            this.ItemInfo.status = (JuBaoStatusEnum)Convert.ToInt32(this.ddlstatus.SelectedValue);
            this.ItemInfo.checktime = new DateTime?(DateTime.Now);
            this.ItemInfo.check = new int?(base.ManageId);
            this.ItemInfo.checkremark = this.txtcheckremark.Text;
            JuBao bao = new JuBao();
            if (bao.Update(this.ItemInfo))
            {
                WebUtility.AlertAndClose(this, "操作成功");
            }
            else
            {
                WebUtility.AlertAndClose(this, "操作失败");
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Merchant))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        private void ShowInfo()
        {
            if (this.ItemInfo != null)
            {
                this.txtname.Text = this.ItemInfo.name;
                this.txtemail.Text = this.ItemInfo.email;
                this.txttel.Text = this.ItemInfo.tel;
                this.txturl.Text = this.ItemInfo.url;
                this.ddltype.SelectedValue = this.ItemInfo.type.ToString();
                this.txtremark.Text = this.ItemInfo.remark;
                this.txtaddtime.Text = this.ItemInfo.addtime.ToString();
                this.ddlstatus.SelectedValue = this.ItemInfo.status.ToString();
                this.txtchecktime.Text = this.ItemInfo.checktime.ToString();
                this.txtcheck.Text = this.ItemInfo.check.ToString();
                this.txtcheckremark.Text = this.ItemInfo.checkremark;
                this.txtpwd.Text = this.ItemInfo.pwd;
                this.txtfield1.Text = this.ItemInfo.field1;
            }
        }

        public JuBaoInfo ItemInfo
        {
            get
            {
                if (this._ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        this._ItemInfo = new JuBao().GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        this._ItemInfo = new JuBaoInfo();
                    }
                }
                return this._ItemInfo;
            }
        }

        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }
    }
}

