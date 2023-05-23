namespace viviapi.WebUI.Managements
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Security;
    using viviLib.Web;

    public class Manage : ManagePageBase
    {
        public viviapi.Model.Manage _ItemInfo = null;
        protected Button btnAdd;
        protected Button BtnUpdate;
        protected DropDownList ddlCommissionType;
        protected DropDownList ddlStus;
        protected HtmlForm form1;
        protected GridView GridView1;
        protected HtmlHead Head1;
        protected DropDownList LevelList;
        protected TextBox PassWordBox;
        protected TextBox RelNameBox;
        protected TextBox SecPassWordBox;
        protected TextBox txtCardCommission;
        protected TextBox txtCommission;
        protected TextBox UserNameBox;

        private void BindView()
        {
            foreach (int num in Enum.GetValues(typeof(ManageRole)))
            {
                this.LevelList.Items.Add(new ListItem(ManageFactory.GetManageRoleView((ManageRole)num), num.ToString()));
            }
            if (this.isUpdate)
            {
                this.UserNameBox.Text = this.ItemInfo.username;
                this.RelNameBox.Text = this.ItemInfo.relname;
                this.LevelList.SelectedValue = ((int)this.ItemInfo.role).ToString();
                this.ddlCommissionType.SelectedValue = this.ItemInfo.commissiontype.ToString();
                this.ddlStus.SelectedValue = this.ItemInfo.status.ToString();
                if (this.ItemInfo.commission.HasValue)
                {
                    this.txtCommission.Text = this.ItemInfo.commission.Value.ToString("f4");
                }
                if (this.ItemInfo.cardcommission.HasValue)
                {
                    this.txtCardCommission.Text = this.ItemInfo.cardcommission.Value.ToString("f4");
                }
            }
            DataTable table = ManageFactory.GetList(string.Empty).Tables[0];
            table.Columns.Add("LevelText");
            table.Columns.Add("Commissiontypeview");
            table.Columns.Add("statusName");
            foreach (DataRow row in table.Rows)
            {
                switch (((int)row["status"]))
                {
                    case 1:
                        row["statusName"] = "正常";
                        break;

                    case 0:
                        row["statusName"] = "锁定";
                        break;
                }
                row["LevelText"] = ManageFactory.GetManageRoleView((ManageRole)((int)row["role"]));
                if (row["Commissiontype"] != DBNull.Value)
                {
                    row["Commissiontypeview"] = (row["Commissiontype"].ToString() == "2") ? "按支付金额%" : "按条固定提成";
                }
            }
            this.GridView1.DataSource = table;
            this.GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("ManageEdit.aspx");
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            string str = this.UserNameBox.Text.Trim();
            string str2 = this.PassWordBox.Text.Trim();
            string str3 = this.SecPassWordBox.Text.Trim();
            string str4 = this.RelNameBox.Text.Trim();
            int num = int.Parse(this.LevelList.SelectedValue);
            this.ItemInfo.username = str;
            if (!string.IsNullOrEmpty(str2))
            {
                this.ItemInfo.password = Cryptography.MD5(str2);
            }
            if (!string.IsNullOrEmpty(str3))
            {
                this.ItemInfo.secondpwd = Cryptography.MD5(str3);
            }
            this.ItemInfo.relname = str4;
            this.ItemInfo.role = (ManageRole)num;
            this.ItemInfo.commissiontype = new int?(int.Parse(this.ddlCommissionType.SelectedValue));
            decimal result = 0M;
            if (!decimal.TryParse(this.txtCommission.Text.Trim(), out result))
            {
                base.AlertAndRedirect("请输入数字");
            }
            else
            {
                this.ItemInfo.commission = new decimal?(result);
                decimal num3 = 0M;
                if (!decimal.TryParse(this.txtCardCommission.Text.Trim(), out num3))
                {
                    base.AlertAndRedirect("请输入数字");
                }
                else
                {
                    this.ItemInfo.cardcommission = new decimal?(num3);
                    this.ItemInfo.status = new int?(int.Parse(this.ddlStus.SelectedValue));
                    bool flag = false;
                    if (this.isUpdate)
                    {
                        if (ManageFactory.Update(this.ItemInfo))
                        {
                            flag = true;
                        }
                    }
                    else if (ManageFactory.Add(this.ItemInfo) > 0)
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        base.AlertAndRedirect("操作成功", "Manage.aspx");
                    }
                    else
                    {
                        base.AlertAndRedirect("操作失败");
                    }
                }
            }
        }

        private void DoCmd()
        {
            if (this.isDel)
            {
                if (ManageFactory.Delete(this.ItemInfoId))
                {
                    base.AlertAndRedirect("删除成功!", "Manage.aspx");
                }
                else
                {
                    base.AlertAndRedirect("删除失败!", "Manage.aspx");
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            this.BindView();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            this.DoCmd();
            if (!base.IsPostBack)
            {
                this.BindView();
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(true, ManageRole.None))
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

        public bool isDel
        {
            get
            {
                return ((this.ItemInfoId > 0) && (this.Action == "del"));
            }
        }

        public bool isUpdate
        {
            get
            {
                return ((this.ItemInfoId > 0) && (this.Action == "edit"));
            }
        }

        public viviapi.Model.Manage ItemInfo
        {
            get
            {
                if (this._ItemInfo == null)
                {
                    if (this.isUpdate)
                    {
                        this._ItemInfo = ManageFactory.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        this._ItemInfo = new viviapi.Model.Manage();
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

