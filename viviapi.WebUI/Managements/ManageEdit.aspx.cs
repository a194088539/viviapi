using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.WebUI.Managements
{
    public class ManageEdit : ManagePageBase
    {
        public viviapi.Model.Manage _ItemInfo = (viviapi.Model.Manage)null;
        protected HtmlForm form1;
        protected HiddenField hf_isupdate;
        protected TextBox txtusername;
        protected TextBox txtpassword;
        protected TextBox txtsecondpwd;
        protected CheckBox ckb_SuperAdmin;
        protected CheckBox ckb_Agent;
        protected CheckBoxList cbl_roles;
        protected DropDownList ddlStus;
        protected TextBox txtrelname;
        protected Label lbllastloginip;
        protected Label lbllastlogintime;
        protected DropDownList ddlCommissionType;
        protected TextBox txtCommission;
        protected TextBox txtCardCommission;
        protected TextBox lblbalance;
        protected Button btnAdd;

        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
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
                return this.ItemInfoId > 0 && this.Action == "edit";
            }
        }

        public viviapi.Model.Manage ItemInfo
        {
            get
            {
                if (this._ItemInfo == null)
                    this._ItemInfo = !this.isUpdate ? new viviapi.Model.Manage() : ManageFactory.GetModel(this.ItemInfoId);
                return this._ItemInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (this.IsPostBack)
                return;
            this.InitForm();
        }

        private void InitForm()
        {
            foreach (ManageRole role in Enum.GetValues(typeof(ManageRole)))
            {
                if (role != ManageRole.None)
                {
                    ListItem listItem = new ListItem(ManageFactory.GetManageRoleView(role), ((int)role).ToString());
                    if ((this.ItemInfo.role & role) == role)
                        listItem.Selected = true;
                    this.cbl_roles.Items.Add(listItem);
                }
            }
            if (!this.isUpdate)
                return;
            this.hf_isupdate.Value = "1";
            this.txtusername.Text = this.ItemInfo.username;
            this.txtrelname.Text = this.ItemInfo.relname;
            this.ddlCommissionType.SelectedValue = this.ItemInfo.commissiontype.ToString();
            this.ddlStus.SelectedValue = this.ItemInfo.status.ToString();
            this.lbllastloginip.Text = this.ItemInfo.lastLoginIp;
            DateTime? lastLoginTime = this.ItemInfo.lastLoginTime;
            if (lastLoginTime.HasValue)
            {
                Label label = this.lbllastlogintime;
                lastLoginTime = this.ItemInfo.lastLoginTime;
                string str = lastLoginTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                label.Text = str;
            }
            Decimal? nullable;
            if (this.ItemInfo.commission.HasValue)
            {
                TextBox textBox = this.txtCommission;
                nullable = this.ItemInfo.commission;
                string str = nullable.Value.ToString("f4");
                textBox.Text = str;
            }
            nullable = this.ItemInfo.cardcommission;
            if (nullable.HasValue)
            {
                TextBox textBox = this.txtCardCommission;
                nullable = this.ItemInfo.cardcommission;
                string str = nullable.Value.ToString("f4");
                textBox.Text = str;
            }
            nullable = this.ItemInfo.balance;
            if (nullable.HasValue)
            {
                TextBox textBox = this.lblbalance;
                nullable = this.ItemInfo.balance;
                string str = nullable.Value.ToString("f2");
                textBox.Text = str;
            }
            this.ckb_SuperAdmin.Checked = this.ItemInfo.isSuperAdmin > 0;
            this.ckb_Agent.Checked = this.ItemInfo.isAgent > 0;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string str1 = this.txtusername.Text.Trim();
            string strToEncrypt1 = this.txtpassword.Text.Trim();
            string strToEncrypt2 = this.txtsecondpwd.Text.Trim();
            string str2 = this.txtrelname.Text.Trim();
            ManageRole manageRole = ManageRole.None;
            foreach (ListItem listItem in this.cbl_roles.Items)
            {
                if (listItem.Selected)
                {
                    if (manageRole == ManageRole.None)
                        manageRole = (ManageRole)Convert.ToInt32(listItem.Value);
                    else
                        manageRole |= (ManageRole)Convert.ToInt32(listItem.Value);
                }
            }
            this.ItemInfo.username = str1;
            if (!string.IsNullOrEmpty(strToEncrypt1))
                this.ItemInfo.password = Cryptography.MD5(strToEncrypt1);
            if (!string.IsNullOrEmpty(strToEncrypt2))
                this.ItemInfo.secondpwd = Cryptography.MD5(strToEncrypt2);
            this.ItemInfo.relname = str2;
            this.ItemInfo.role = manageRole;
            this.ItemInfo.commissiontype = new int?(int.Parse(this.ddlCommissionType.SelectedValue));
            Decimal result1 = new Decimal(0);
            if (!Decimal.TryParse(this.txtCommission.Text.Trim(), out result1))
            {
                this.AlertAndRedirect("请输入数字");
            }
            else
            {
                Decimal result2 = new Decimal(0);
                if (!Decimal.TryParse(this.txtCardCommission.Text.Trim(), out result2))
                {
                    this.AlertAndRedirect("请输入数字");
                }
                else
                {
                    this.ItemInfo.cardcommission = new Decimal?(result2);
                    this.ItemInfo.commission = new Decimal?(result1);
                    this.ItemInfo.status = new int?(int.Parse(this.ddlStus.SelectedValue));
                    this.ItemInfo.isSuperAdmin = this.ckb_SuperAdmin.Checked ? 1 : 0;
                    this.ItemInfo.isAgent = this.ckb_Agent.Checked ? 1 : 0;
                    bool flag = false;
                    if (this.isUpdate)
                    {
                        if (ManageFactory.Update(this.ItemInfo))
                            flag = true;
                    }
                    else if (ManageFactory.Add(this.ItemInfo) > 0)
                        flag = true;
                    if (flag)
                        this.AlertAndRedirect("操作成功", "Manage.aspx");
                    else
                        this.AlertAndRedirect("操作失败");
                }
            }
        }

        private void setPower()
        {
            if (ManageFactory.CheckCurrentPermission(true, ManageRole.None))
                return;
            this.Response.Write("Sorry,No authority!");
            this.Response.End();
        }
    }
}
