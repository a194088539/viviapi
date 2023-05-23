namespace viviapi.WebUI.business
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.User;
    using viviapi.Model;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public class QuestionEdit : BusinessPageBase
    {
        public QuestionInfo _ItemInfo = null;
        protected Button btnAdd;
        protected CheckBox chkrelease;
        protected HtmlForm form1;
        protected HiddenField hf_isupdate;
        protected TextBox txtquestion;
        protected TextBox txtsort;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Question question = new Question();
            string text = this.txtquestion.Text;
            bool flag = this.chkrelease.Checked;
            int num = int.Parse(this.txtsort.Text);
            this.ItemInfo.question = text;
            this.ItemInfo.release = flag;
            this.ItemInfo.sort = num;
            bool flag2 = false;
            if (this.isUpdate)
            {
                if (question.Update(this.ItemInfo))
                {
                    flag2 = true;
                }
            }
            else if (question.Add(this.ItemInfo) > 0)
            {
                flag2 = true;
            }
            if (flag2)
            {
                base.AlertAndRedirect("操作成功", "Questions.aspx");
            }
            else
            {
                base.AlertAndRedirect("操作失败");
            }
        }

        private void InitForm()
        {
            this.txtquestion.Text = this.ItemInfo.question;
            this.chkrelease.Checked = this.ItemInfo.release;
            this.txtsort.Text = this.ItemInfo.sort.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.InitForm();
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

        public string Action
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", "");
            }
        }

        public bool isUpdate
        {
            get
            {
                return ((this.ItemInfoId > 0) && (this.Action == "edit"));
            }
        }

        public QuestionInfo ItemInfo
        {
            get
            {
                if (this._ItemInfo == null)
                {
                    if (this.isUpdate)
                    {
                        this._ItemInfo = new Question().GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        this._ItemInfo = new QuestionInfo();
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

