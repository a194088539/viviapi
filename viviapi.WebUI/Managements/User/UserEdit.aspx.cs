namespace viviapi.WebUI.Managements.User
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.User;
    using viviapi.Model;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;
    using viviapi.WebUI;
    using viviLib.Web;

    public class UserEdit : ManagePageBase
    {
        public feedbackInfo _ItemInfo = null;
        public UserInfo _userInfo = null;
        protected Button btnOK;
        protected HtmlForm form1;
        protected HtmlInputText txtaddtime;
        protected TextBox txtcont;
        protected TextBox txtreply;
        protected TextBox txtreplyer;
        protected HtmlInputText txtreplytime;
        protected TextBox txtstatus;
        protected TextBox txttitle;
        protected TextBox txttypeid;
        protected TextBox txtuserid;

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
            this.ItemInfo.reply = this.txtreply.Text;
            this.ItemInfo.status = feedbackstatus.已回复;
            this.ItemInfo.replytime = new DateTime?(DateTime.Now);
            this.ItemInfo.replyer = new int?(base.ManageId);
            feedback feedback = new feedback();
            if (feedback.Update(this.ItemInfo))
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
            if (this.model != null)
            {
                this.txtuserid.Text = this.ItemInfo.userid.ToString();
                this.txttypeid.Text = Enum.GetName(typeof(feedbacktype), (int)this.ItemInfo.typeid);
                this.txttitle.Text = this.ItemInfo.title;
                this.txtcont.Text = this.ItemInfo.cont;
                this.txtstatus.Text = Enum.GetName(typeof(feedbackstatus), (int)this.ItemInfo.status);
                this.txtaddtime.Value = this.ItemInfo.addtime.ToString("yyyy-MM-dd HH:mm:ss");
                this.txtreply.Text = this.ItemInfo.reply;
                this.txtreplyer.Text = this.ItemInfo.replyer.ToString();
                this.txtreplytime.Value = this.ItemInfo.replytime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                this.txtuserid.Enabled = false;
                this.txttypeid.Enabled = false;
                this.txttitle.Enabled = false;
                this.txtcont.Enabled = false;
            }
        }

        public feedbackInfo ItemInfo
        {
            get
            {
                if (this._ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        this._ItemInfo = new feedback().GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        this._ItemInfo = new feedbackInfo();
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

        public UserInfo model
        {
            get
            {
                if ((this._userInfo == null) && (this.ItemInfo != null))
                {
                    if (this.ItemInfoId > 0)
                    {
                        this._userInfo = UserFactory.GetModel(this.ItemInfo.userid);
                    }
                    else
                    {
                        this._userInfo = new UserInfo();
                    }
                }
                return this._userInfo;
            }
        }
    }
}

