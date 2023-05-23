namespace viviapi.WebUI.Merchant
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.User;
    using viviapi.WebComponents.Web;

    public class safeques : UserPageBase
    {
        protected Button btnSave;
        protected HtmlTableRow p_oldans;
        protected HtmlTableRow p_oldques;
        protected HtmlInputText txtnewans;
        protected HtmlInputText txtnewques;
        protected HtmlInputText txtoldans;
        protected HtmlInputText txtoldques;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = "";
            string str2 = this.txtnewques.Value;
            string str3 = this.txtnewans.Value;
            string str4 = this.txtoldans.Value;
            if (!this.isnew && (str4 != base.currentUser.answer))
            {
                str = "问题答案不对";
            }
            if (string.IsNullOrEmpty(str2))
            {
                str = "新问题为空";
            }
            else if (string.IsNullOrEmpty(str3))
            {
                str = "新问题为空";
            }
            if (string.IsNullOrEmpty(str))
            {
                base.currentUser.question = str2;
                base.currentUser.answer = str3;
                if (UserFactory.Update(base.currentUser, null))
                {
                    str = "更新成功";
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
            if (string.IsNullOrEmpty(base.currentUser.question))
            {
                this.p_oldans.Visible = false;
                this.p_oldques.Visible = false;
            }
            else
            {
                this.p_oldans.Visible = true;
                this.p_oldques.Visible = true;
                this.txtoldques.Value = base.currentUser.question;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitForm();
            }
        }

        public bool isnew
        {
            get
            {
                return string.IsNullOrEmpty(base.currentUser.question);
            }
        }
    }
}

