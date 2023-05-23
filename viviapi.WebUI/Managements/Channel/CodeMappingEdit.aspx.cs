namespace viviapi.WebUI.Managements
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Channel;
    using viviapi.Model;
    using viviapi.Model.Channel;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public class CodeMappingEdit : ManagePageBase
    {
        public CodeMappingInfo _ItemInfo = null;
        protected Button btnCont;
        protected Button btnSave;
        protected DropDownList ddlpmode;
        protected DropDownList ddlsupp;
        protected HtmlForm form1;
        protected TextBox txtsuppCode;

        protected void btnCont_Click(object sender, EventArgs e)
        {
            this.Save("CodeMappingEdit.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.Save("CodeMappinglList.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.ddlpmode.Items.Add(new ListItem("---全部通道---", ""));
                DataTable table = viviapi.BLL.Channel.Channel.GetList(0x66).Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    this.ddlpmode.Items.Add(new ListItem(row["modeName"].ToString(), row["code"].ToString()));
                }
                string where = "isbank=1";
                DataTable table2 = SupplierFactory.GetList(where).Tables[0];
                this.ddlsupp.Items.Add(new ListItem("--请选择--", ""));
                foreach (DataRow row in table2.Rows)
                {
                    this.ddlsupp.Items.Add(new ListItem(row["name"].ToString(), row["code"].ToString()));
                }
                this.ShowInfo();
            }
        }

        private void Save(string url)
        {
            string selectedValue = this.ddlpmode.SelectedValue;
            int num = int.Parse(this.ddlsupp.SelectedValue);
            string str2 = this.txtsuppCode.Text.Trim();
            this.model.pmodeCode = selectedValue;
            this.model.suppId = num;
            this.model.suppCode = str2;
            if (!this.isUpdate)
            {
                if (CodeMappingFactory.Add(this.model) > 0)
                {
                    base.AlertAndRedirect("保存成功！", url);
                }
                else
                {
                    base.AlertAndRedirect("保存失败！");
                }
            }
            else if (CodeMappingFactory.Update(this.model))
            {
                base.AlertAndRedirect("更新成功！", url);
            }
            else
            {
                base.AlertAndRedirect("更新失败！");
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Interfaces))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        private void ShowInfo()
        {
            if (this.isUpdate && (this.model != null))
            {
                this.ddlpmode.SelectedValue = this.model.pmodeCode;
                this.ddlsupp.SelectedValue = this.model.suppId.ToString();
                this.txtsuppCode.Text = this.model.suppCode;
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

        public CodeMappingInfo model
        {
            get
            {
                if (this._ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        this._ItemInfo = CodeMappingFactory.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        this._ItemInfo = new CodeMappingInfo();
                    }
                }
                return this._ItemInfo;
            }
        }
    }
}

