namespace viviapi.WebUI.Managements.Settled
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Settled;
    using viviapi.BLL.User;
    using viviapi.Model;
    using viviapi.Model.Settled;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public class IncreaseAmtEdit : ManagePageBase
    {
        public IncreaseAmtInfo _ItemInfo = null;
        protected Button btnAdd;
        protected CustomValidator CustomValidator1;
        protected HtmlForm form1;
        protected Label lblbalance;
        protected RadioButtonList rbl_optype;
        protected TextBox txtdesc;
        protected TextBox txtincreaseAmt;
        protected TextBox txtuserId;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int result = 0;
            if (!int.TryParse(this.txtuserId.Text.Trim(), out result))
            {
                args.IsValid = false;
            }
            else if (!UserFactory.Exists(int.Parse(this.txtuserId.Text)))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.userId > 0)
            {
                string s = string.Empty;
                UsersAmtInfo model = UsersAmt.GetModel(this.userId);
                if (model == null)
                {
                    s = "用户不存在!";
                }
                else
                {
                    s = ((model.balance.Value - model.Freeze.Value) - model.unpayment.Value).ToString("f2");
                }
                base.Response.Write(s);
                base.Response.End();
            }
            ManageFactory.CheckSecondPwd();
            this.setPower();
            if (!base.IsPostBack)
            {
                this.ShowInfo();
            }
        }

        private void Save()
        {
            if (base.IsValid)
            {
                int result = 0;
                if (!int.TryParse(this.txtuserId.Text, out result))
                {
                    base.AlertAndRedirect("请输入正确的用户ID");
                }
                else
                {
                    decimal num2 = 0M;
                    if (!decimal.TryParse(this.txtincreaseAmt.Text, out num2))
                    {
                        base.AlertAndRedirect("请输入正确的金额");
                    }
                    else
                    {
                        string text = this.txtdesc.Text;
                        this.model.userId = new int?(result);
                        this.model.increaseAmt = new decimal?(num2);
                        this.model.addtime = new DateTime?(DateTime.Now);
                        this.model.mangeId = new int?(base.ManageId);
                        this.model.mangeName = base.currentManage.username;
                        this.model.status = 1;
                        this.model.optype = (optypeenum)int.Parse(this.rbl_optype.SelectedValue);
                        this.model.desc = text;
                        if (!this.isUpdate)
                        {
                            if (IncreaseAmt.Add(this.model) > 0)
                            {
                                base.AlertAndRedirect("操作成功！", "IncreaseAmts.aspx");
                            }
                            else
                            {
                                base.AlertAndRedirect("操作失败！");
                            }
                        }
                    }
                }
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
            if (this.isUpdate && (this.model != null))
            {
                this.txtuserId.Text = this.model.userId.ToString();
                this.txtincreaseAmt.Text = this.model.increaseAmt.ToString();
                this.txtdesc.Text = this.model.desc;
            }
        }

        public bool isUpdate
        {
            get
            {
                return (this.ItemInfoId > 0);
            }
        }

        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public IncreaseAmtInfo model
        {
            get
            {
                if (this._ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        this._ItemInfo = IncreaseAmt.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        this._ItemInfo = new IncreaseAmtInfo();
                    }
                }
                return this._ItemInfo;
            }
        }

        public int userId
        {
            get
            {
                return WebBase.GetQueryStringInt32("user", 0);
            }
        }
    }
}

