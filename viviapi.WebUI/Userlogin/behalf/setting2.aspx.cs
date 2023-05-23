namespace viviapi.WebUI.Userlogin.behalf
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.User;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;

    public class setting2 : UserPageBase
    {
        private usersettingInfo _setting = null;
        protected Button btnupdate;
        protected HtmlForm form1;
        protected RadioButtonList rbl_set;
        protected usersetting setbll = new usersetting();

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (this.setting != null)
            {
                this.setting.isRequireAgentDistAudit = byte.Parse(this.rbl_set.SelectedValue);
                this.setbll.Insert(this.setting);
                base.AlertAndRedirect("操作成功");
            }
        }

        private void InitForm()
        {
            if (this.setting != null)
            {
                this.rbl_set.SelectedValue = this.setting.isRequireAgentDistAudit.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitForm();
            }
        }

        public usersettingInfo setting
        {
            get
            {
                if (this._setting == null)
                {
                    this._setting = this.setbll.GetModel(base.currentUser.ID);
                }
                if (this._setting == null)
                {
                    this._setting = new usersettingInfo();
                }
                return this._setting;
            }
        }
    }
}

