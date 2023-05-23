namespace viviapi.WebUI.business.User
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

    public class UserImageCheck : BusinessPageBase
    {
        public usersIdImageInfo _ItemInfo = null;
        public UserInfo _userInfo = null;
        protected Button btnFail;
        protected Button btnOK;
        protected HtmlForm form1;
        protected Label lblid;
        protected TextBox txtidCard;
        protected TextBox txtRealName;
        protected TextBox txtuserName;
        protected TextBox txtWhy;

        protected void btnFail_Click(object sender, EventArgs e)
        {
            this.Save("fail");
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            this.Save("ok");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (this.ItemInfo.status != IdImageStatus.审核中)
            {
                this.btnFail.Enabled = false;
                this.btnOK.Enabled = false;
            }
            if (!base.IsPostBack)
            {
                this.ShowInfo();
            }
        }

        private void Save(string cmd)
        {
            usersIdImageInfo model = new usersIdImageInfo();
            model.id = this.ItemInfoId;
            if (cmd == "ok")
            {
                model.status = IdImageStatus.审核成功;
            }
            if (cmd == "fail")
            {
                model.status = IdImageStatus.审核失败;
            }
            model.why = this.txtWhy.Text.Trim();
            model.checktime = new DateTime?(DateTime.Now);
            model.admin = new int?(base.ManageId);
            model.userId = this.ItemInfo.userId;
            usersIdImage image = new usersIdImage();
            if (image.Check(model))
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
                this.lblid.Text = this.model.ID.ToString();
                this.txtuserName.Text = this.model.UserName;
                this.txtRealName.Text = this.model.PayeeName;
                this.txtidCard.Text = this.model.IdCard;
                this.txtWhy.Text = this.ItemInfo.why;
            }
        }

        public usersIdImageInfo ItemInfo
        {
            get
            {
                if (this._ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        this._ItemInfo = new usersIdImage().GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        this._ItemInfo = new usersIdImageInfo();
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
                        this._userInfo = UserFactory.GetModel(this.ItemInfo.userId.Value);
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

