using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Data;
using viviLib.Web;
using Wuqi.Webdiyer;

namespace viviapi.WebUI.Managements
{
    public class Audits : ManagePageBase
    {
        protected HtmlHead Head1;
        protected HtmlForm form1;
        protected TextBox txtUserId;
        protected TextBox txtItemInfoId;
        protected DropDownList ddlbankName;
        protected TextBox txtAccount;
        protected TextBox txtpayeeName;
        protected DropDownList ddlmode;
        protected DropDownList ddlSupplier;
        protected Button btnSearch;
        protected Button btnPass;
        protected Button btnAllPass;
        protected Button btnallfail;
        protected Button btnExport;
        protected Repeater rptApply;
        protected AspNetPager Pager1;

        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("ID", 0);
            }
        }

        public int ItemInfoStatus
        {
            get
            {
                return WebBase.GetQueryStringInt32("status", 0);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (this.IsPostBack)
                return;
            this.ddlmode.Items.Add(new ListItem("--提现方式--", ""));
            foreach (int num in Enum.GetValues(typeof(SettledmodeEnum)))
                this.ddlmode.Items.Add(new ListItem(Enum.GetName(typeof(SettledmodeEnum), (object)num), num.ToString()));
            DataTable dataTable = SupplierFactory.GetList("isdistribution=1").Tables[0];
            this.ddlSupplier.Items.Add(new ListItem("--付款接口--", ""));
            this.ddlSupplier.Items.Add(new ListItem("不走接口", "0"));
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
                this.ddlSupplier.Items.Add(new ListItem(dataRow["name"].ToString(), dataRow["code"].ToString()));
            this.BindData();
        }

        private void setPower()
        {
            if (ManageFactory.CheckCurrentPermission(false, ManageRole.Financial))
                return;
            this.Response.Write("Sorry,No authority!");
            this.Response.End();
        }

        private void BindData()
        {
            DataSet data = this.GetData();
            this.Pager1.RecordCount = Convert.ToInt32(data.Tables[0].Rows[0][0]);
            this.rptApply.DataSource = (object)data.Tables[1];
            this.rptApply.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void btnPass_Click(object sender, EventArgs e)
        {
            string ids = this.Request.Form["ischecked"];
            DataTable withdrawListByApi = (DataTable)null;
            string batchNo = Guid.NewGuid().ToString("N");
            if (!string.IsNullOrEmpty(ids))
            {
                if (SettledFactory.BatchPass(ids, batchNo, out withdrawListByApi))
                {
                    if (withdrawListByApi != null && withdrawListByApi.Rows.Count > 0)
                    {
                        foreach (SettledInfo itemInfo in SettledFactory.DataTableToList(withdrawListByApi))
                            viviapi.ETAPI.Withdraw.InitDistribution(itemInfo);
                    }
                    this.AlertAndRedirect("审核成功!");
                    this.BindData();
                }
                else
                    this.AlertAndRedirect("审核失败!");
            }
            else
                this.AlertAndRedirect("请选择要审核的申请!");
        }

        protected void Pager1_PageChanging(object src, PageChangingEventArgs e)
        {
            this.BindData();
        }

        protected void btnAllPass_Click(object sender, EventArgs e)
        {
            string batchNo = Guid.NewGuid().ToString("N");
            if (SettledFactory.AllPass(batchNo))
            {
                DataTable listWithdrawByApi = SettledFactory.GetListWithdrawByApi(batchNo);
                if (listWithdrawByApi != null && listWithdrawByApi.Rows.Count > 0)
                {
                    foreach (SettledInfo itemInfo in SettledFactory.DataTableToList(listWithdrawByApi))
                        viviapi.ETAPI.Withdraw.InitDistribution(itemInfo);
                }
                this.AlertAndRedirect("审核成功!");
                this.BindData();
            }
            else
                this.AlertAndRedirect("审核失败!");
        }

        protected void btnallfail_Click(object sender, EventArgs e)
        {
            if (SettledFactory.Allfails())
            {
                this.AlertAndRedirect("操作成功!");
                this.BindData();
            }
            else
                this.AlertAndRedirect("操作失败!");
        }

        protected string GetTranApiName(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return "不走接口";
            return Convert.ToInt32(obj) == 100 ? "财付通" : "";
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void rptApply_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;
            DataBinder.Eval(e.Item.DataItem, "status").ToString();
        }

        protected void rptApply_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string s = e.CommandArgument.ToString();
            if (!string.IsNullOrEmpty(s))
            {
                int status = -1;
                if (e.CommandName == "Pass")
                    status = 2;
                else if (e.CommandName == "Refuse")
                    status = 4;
                if (status != -1 && SettledFactory.Audit(int.Parse(s), status) && status == 2)
                {
                    SettledInfo model = SettledFactory.GetModel(int.Parse(s));
                    if (model.status == SettledStatus.付款接口支付中)
                        viviapi.ETAPI.Withdraw.InitDistribution(model);
                }
            }
            this.BindData();
        }

        private DataSet GetData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("status", (object)1));
            int result = 0;
            if (!string.IsNullOrEmpty(this.txtItemInfoId.Text.Trim()) && int.TryParse(this.txtItemInfoId.Text.Trim(), out result))
                searchParams.Add(new SearchParam("id", (object)result));
            if (!string.IsNullOrEmpty(this.txtUserId.Text.Trim()) && int.TryParse(this.txtUserId.Text.Trim(), out result))
                searchParams.Add(new SearchParam("userid", (object)result));
            if (!string.IsNullOrEmpty(this.ddlSupplier.SelectedValue))
                searchParams.Add(new SearchParam("tranapi", (object)int.Parse(this.ddlSupplier.SelectedValue)));
            if (!string.IsNullOrEmpty(this.ddlbankName.SelectedValue))
                searchParams.Add(new SearchParam("payeebank", (object)this.ddlbankName.SelectedValue));
            if (!string.IsNullOrEmpty(this.txtAccount.Text.Trim()))
                searchParams.Add(new SearchParam("account", (object)this.txtAccount.Text.Trim()));
            if (!string.IsNullOrEmpty(this.txtpayeeName.Text.Trim()))
                searchParams.Add(new SearchParam("payeename", (object)this.txtpayeeName.Text.Trim()));
            return SettledFactory.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet data = this.GetData();
                if (data == null)
                    return;
                DataTable dataTable = data.Tables[1];
                dataTable.Columns.Add("sName", typeof(string));
                foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
                {
                    dataRow["sName"] = (object)Enum.GetName(typeof(SettledStatus), dataRow["status"]);
                    dataRow["PayeeBank"] = (object)SettledFactory.GetSettleBankName(dataRow["PayeeBank"].ToString());
                }
                dataTable.AcceptChanges();
                dataTable.TableName = "Rpt";
                string file = this.Server.MapPath("~/common/template/xls/settle.xls");
                WorkbookDesigner workbookDesigner = new WorkbookDesigner();
                workbookDesigner.Workbook = new Workbook(file);
                workbookDesigner.SetDataSource(dataTable);
                workbookDesigner.Process();
                workbookDesigner.Workbook.Save(this.Response, DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", ContentDisposition.Attachment, workbookDesigner.Workbook.SaveOptions);
            }
            catch (Exception ex)
            {
                this.AlertAndRedirect(ex.Message);
            }
        }
    }
}
