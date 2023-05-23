namespace viviapi.WebUI.Managements
{
    using Aspose.Cells;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class Historys : ManagePageBase
    {
        protected Button btnExport;
        protected Button btnSearch;
        protected DropDownList ddlbankName;
        protected DropDownList ddlmode;
        protected DropDownList ddlStatusList;
        protected DropDownList ddlSupplier;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected AspNetPager Pager1;
        protected Repeater rptList;
        protected string TotalMoney = "0.00元";
        protected TextBox txtAccount;
        protected TextBox txtEtimeBox;
        protected TextBox txtItemInfoId;
        protected TextBox txtpayeeName;
        protected TextBox txtStimeBox;
        protected TextBox txtUserId;

        private void BindData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            if (!string.IsNullOrEmpty(this.ddlStatusList.SelectedValue))
            {
                searchParams.Add(new SearchParam("status", int.Parse(this.ddlStatusList.SelectedValue)));
            }
            int result = 0;
            if (!string.IsNullOrEmpty(this.txtItemInfoId.Text.Trim()) && int.TryParse(this.txtItemInfoId.Text.Trim(), out result))
            {
                searchParams.Add(new SearchParam("id", result));
            }
            if (!string.IsNullOrEmpty(this.txtUserId.Text.Trim()) && int.TryParse(this.txtUserId.Text.Trim(), out result))
            {
                searchParams.Add(new SearchParam("userid", result));
            }
            if (!string.IsNullOrEmpty(this.ddlSupplier.SelectedValue))
            {
                searchParams.Add(new SearchParam("tranapi", int.Parse(this.ddlSupplier.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.ddlbankName.SelectedValue))
            {
                searchParams.Add(new SearchParam("payeebank", this.ddlbankName.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtAccount.Text.Trim()))
            {
                searchParams.Add(new SearchParam("account", this.txtAccount.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtpayeeName.Text.Trim()))
            {
                searchParams.Add(new SearchParam("payeename", this.txtpayeeName.Text.Trim()));
            }
            DataSet set = SettledFactory.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            DataTable table = set.Tables[1];
            if (table != null)
            {
                table.Columns.Add("StatusText");
                foreach (DataRow row in table.Rows)
                {
                    switch (((SettledStatus)row["Status"]))
                    {
                        case SettledStatus.审核中:
                            row["StatusText"] = "<font color='#66CC00'>审核中</font>";
                            break;

                        case SettledStatus.支付中:
                            row["StatusText"] = "<a href=\"Pay.aspx?id=" + row["ID"].ToString() + "\">进行支付</a>";
                            break;

                        case SettledStatus.无效:
                            row["StatusText"] = "<font color='red'>无效申请</font>";
                            break;

                        case SettledStatus.已支付:
                            row["StatusText"] = "<font color='blue'>已支付</font>";
                            break;
                    }
                }
            }
            this.rptList.DataSource = table;
            this.rptList.DataBind();
            this.TotalMoney = Convert.ToString(set.Tables[2].Rows[0][0]);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet data = this.GetData();
                if (data != null)
                {
                    DataTable dataTable = data.Tables[1];
                    dataTable.Columns.Add("sName", typeof(string));
                    foreach (DataRow row in dataTable.Rows)
                    {
                        row["sName"] = Enum.GetName(typeof(SettledStatus), row["status"]);
                        row["PayeeBank"] = SettledFactory.GetSettleBankName(row["PayeeBank"].ToString());
                    }
                    dataTable.AcceptChanges();
                    dataTable.TableName = "Rpt";
                    string file = base.Server.MapPath("~/common/template/xls/settle.xls");
                    WorkbookDesigner designer = new WorkbookDesigner();
                    designer.Workbook = new Workbook(file);
                    designer.SetDataSource(dataTable);
                    designer.Process();
                    designer.Workbook.Save(base.Response, DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", ContentDisposition.Attachment, designer.Workbook.SaveOptions);
                }
            }
            catch (Exception exception)
            {
                base.AlertAndRedirect(exception.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        private DataSet GetData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            if (!string.IsNullOrEmpty(this.ddlStatusList.SelectedValue))
            {
                searchParams.Add(new SearchParam("status", int.Parse(this.ddlStatusList.SelectedValue)));
            }
            int result = 0;
            if (!string.IsNullOrEmpty(this.txtItemInfoId.Text.Trim()) && int.TryParse(this.txtItemInfoId.Text.Trim(), out result))
            {
                searchParams.Add(new SearchParam("id", result));
            }
            if (!string.IsNullOrEmpty(this.txtUserId.Text.Trim()) && int.TryParse(this.txtUserId.Text.Trim(), out result))
            {
                searchParams.Add(new SearchParam("userid", result));
            }
            if (!string.IsNullOrEmpty(this.ddlSupplier.SelectedValue))
            {
                searchParams.Add(new SearchParam("tranapi", int.Parse(this.ddlSupplier.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.ddlbankName.SelectedValue))
            {
                searchParams.Add(new SearchParam("payeebank", this.ddlbankName.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtAccount.Text.Trim()))
            {
                searchParams.Add(new SearchParam("account", this.txtAccount.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtpayeeName.Text.Trim()))
            {
                searchParams.Add(new SearchParam("payeename", this.txtpayeeName.Text.Trim()));
            }
            return SettledFactory.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.ddlmode.Items.Add(new ListItem("--提现方式--", ""));
                foreach (int num in Enum.GetValues(typeof(SettledmodeEnum)))
                {
                    this.ddlmode.Items.Add(new ListItem(Enum.GetName(typeof(SettledmodeEnum), num), num.ToString()));
                }
                DataTable table = SupplierFactory.GetList("isdistribution=1").Tables[0];
                this.ddlSupplier.Items.Add(new ListItem("--付款接口--", ""));
                this.ddlSupplier.Items.Add(new ListItem("不走接口", "0"));
                foreach (DataRow row in table.Rows)
                {
                    this.ddlSupplier.Items.Add(new ListItem(row["name"].ToString(), row["code"].ToString()));
                }
                this.txtStimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.txtEtimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.txtStimeBox.Text = DateTime.Now.ToString("yyyy-MM-01");
                this.txtEtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.ddlStatusList.Items.Add(new ListItem("--状态--", ""));
                foreach (int num in Enum.GetValues(typeof(SettledStatus)))
                {
                    this.ddlStatusList.Items.Add(new ListItem(Enum.GetName(typeof(SettledStatus), num), num.ToString()));
                }
                this.ddlStatusList.SelectedValue = 8.ToString();
                this.BindData();
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData();
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Financial))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        public string action
        {
            get
            {
                return WebBase.GetQueryStringString("action", "");
            }
        }

        public int userId
        {
            get
            {
                return WebBase.GetQueryStringInt32("userid", 0);
            }
        }
    }
}

