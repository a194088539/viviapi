using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Withdraw;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Data;
using viviLib.Web;
using Wuqi.Webdiyer;

namespace viviapi.WebUI.Managements
{
    public class settledAgentSummarys : ManagePageBase
    {
        protected settledAgentSummary _bll = new settledAgentSummary();
        protected settledAgent stlAgtBLL = new settledAgent();
        protected HtmlHead Head1;
        protected HtmlForm form1;
        protected TextBox txtUserId;
        protected TextBox txtLotno;
        protected TextBox StimeBox;
        protected TextBox EtimeBox;
        protected DropDownList ddlaudit_status;
        protected DropDownList ddlstatus;
        protected Button btnSearch;
        protected Button btnPass;
        protected Button btnAllPass;
        protected Button btnallfail;
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
            this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
            this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");
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
            List<SearchParam> searchParams = new List<SearchParam>();
            int result1 = 0;
            if (!string.IsNullOrEmpty(this.txtUserId.Text.Trim()) && int.TryParse(this.txtUserId.Text.Trim(), out result1))
                searchParams.Add(new SearchParam("userid", (object)result1));
            if (!string.IsNullOrEmpty(this.txtLotno.Text.Trim()))
                searchParams.Add(new SearchParam("lotno", (object)this.txtLotno.Text.Trim()));
            if (!string.IsNullOrEmpty(this.ddlstatus.Text.Trim()) && int.TryParse(this.ddlstatus.Text.Trim(), out result1))
                searchParams.Add(new SearchParam("status", (object)result1));
            DateTime result2 = DateTime.MinValue;
            if (!string.IsNullOrEmpty(this.StimeBox.Text.Trim()) && DateTime.TryParse(this.StimeBox.Text.Trim(), out result2) && result2 > DateTime.MinValue)
                searchParams.Add(new SearchParam("saddtime", (object)this.StimeBox.Text.Trim()));
            if (!string.IsNullOrEmpty(this.EtimeBox.Text.Trim()) && DateTime.TryParse(this.EtimeBox.Text.Trim(), out result2) && result2 > DateTime.MinValue)
                searchParams.Add(new SearchParam("eaddtime", (object)result2.AddDays(1.0)));
            DataSet dataSet = this._bll.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty, false);
            this.Pager1.RecordCount = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
            this.rptApply.DataSource = (object)dataSet.Tables[1];
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
            HtmlTableRow htmlTableRow = e.Item.FindControl("tr_detail") as HtmlTableRow;
            (e.Item.FindControl("litimg") as Literal).Text = string.Format("<img src=\"../style/images/folder_close.gif\" style=\"cursor: hand\" onclick=\"collapse(this, '{0}')\" alt=\"\" />", (object)htmlTableRow.ClientID);
            string str = DataBinder.Eval(e.Item.DataItem, "lotno").ToString();
            DataBinder.Eval(e.Item.DataItem, "status").ToString();
            Repeater repeater = (Repeater)e.Item.FindControl("rptList");
            DataSet list = this.stlAgtBLL.GetList("lotno=" + str);
            repeater.DataSource = (object)list;
            repeater.DataBind();
        }

        protected void rptApply_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
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
            }
            this.BindData();
        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                ;
        }
    }
}
