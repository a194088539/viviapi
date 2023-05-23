namespace viviapi.WebUI.Managements.Withdraw
{
    using Aspose.Cells;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.ETAPI;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class AgentDists : ManagePageBase
    {
        protected Button btnExport;
        protected Button btnSearch;
        protected DropDownList ddl_issure;
        protected DropDownList ddlaudit_status;
        protected DropDownList ddlbankCode;
        protected DropDownList ddlis_cancel;
        protected DropDownList ddlmode;
        protected DropDownList ddlnotifystatus;
        protected DropDownList ddlpayment_status;
        protected DropDownList ddlSupplier;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected AspNetPager Pager1;
        protected Repeater rptList;
        protected viviapi.BLL.Withdraw.settledAgent stlAgtBLL = new viviapi.BLL.Withdraw.settledAgent();
        protected string total_amount = "0.00元";
        protected string total_charge = "0.00元";
        protected string total_paymoney = "0.00元";
        protected TextBox txtAccount;
        protected TextBox txtbankAccountName;
        protected TextBox txtEtimeBox;
        protected TextBox txtLotno;
        protected TextBox txtout_trade_no;
        protected TextBox txtStimeBox;
        protected TextBox txttrade_no;
        protected TextBox txtUserId;

        private void BindData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            int result = 0;
            if (!string.IsNullOrEmpty(this.txtUserId.Text.Trim()) && int.TryParse(this.txtUserId.Text.Trim(), out result))
            {
                searchParams.Add(new SearchParam("userid", result));
            }
            if (!string.IsNullOrEmpty(this.txttrade_no.Text.Trim()))
            {
                searchParams.Add(new SearchParam("trade_no", this.txttrade_no.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtout_trade_no.Text.Trim()))
            {
                searchParams.Add(new SearchParam("out_trade_no", this.txtout_trade_no.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtLotno.Text))
            {
                searchParams.Add(new SearchParam("lotno", this.txtLotno.Text));
            }
            if (!string.IsNullOrEmpty(this.ddlbankCode.SelectedValue))
            {
                searchParams.Add(new SearchParam("bankCode", this.ddlbankCode.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtbankAccountName.Text))
            {
                searchParams.Add(new SearchParam("bankAccountName", this.txtbankAccountName.Text));
            }
            if (!string.IsNullOrEmpty(this.ddlSupplier.SelectedValue))
            {
                searchParams.Add(new SearchParam("tranapi", int.Parse(this.ddlSupplier.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.ddlmode.SelectedValue))
            {
                searchParams.Add(new SearchParam("mode", int.Parse(this.ddlmode.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.ddlaudit_status.SelectedValue))
            {
                searchParams.Add(new SearchParam("audit_status", int.Parse(this.ddlaudit_status.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.ddlpayment_status.SelectedValue))
            {
                searchParams.Add(new SearchParam("payment_status", int.Parse(this.ddlpayment_status.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.ddlis_cancel.SelectedValue))
            {
                bool paramValue = false;
                if (this.ddlis_cancel.SelectedValue == "1")
                {
                    paramValue = true;
                }
                searchParams.Add(new SearchParam("is_cancel", paramValue));
            }
            if (!string.IsNullOrEmpty(this.ddlnotifystatus.SelectedValue))
            {
                searchParams.Add(new SearchParam("notifystatus", int.Parse(this.ddlnotifystatus.SelectedValue)));
            }
            DataSet set = this.stlAgtBLL.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty, 1);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            DataTable table = set.Tables[1];
            this.rptList.DataSource = table;
            this.rptList.DataBind();
            try
            {
                DataRow row = set.Tables[2].Rows[0];
                this.total_amount = string.Format("{0:f2}", row["amount"]);
                this.total_charge = string.Format("{0:f2}", row["charge"]);
                this.total_paymoney = string.Format("{0:f2}", row["totalpay"]);
            }
            catch
            {
            }
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
                        row["sName"] = this.stlAgtBLL.GetPaymentStatusText(row["payment_status"]);
                    }
                    dataTable.AcceptChanges();
                    dataTable.TableName = "Rpt";
                    string file = base.Server.MapPath("~/common/template/xls/SettledAgent.xls");
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
            int result = 0;
            if (!string.IsNullOrEmpty(this.txtUserId.Text.Trim()) && int.TryParse(this.txtUserId.Text.Trim(), out result))
            {
                searchParams.Add(new SearchParam("userid", result));
            }
            if (!string.IsNullOrEmpty(this.txttrade_no.Text.Trim()))
            {
                searchParams.Add(new SearchParam("trade_no", this.txttrade_no.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtout_trade_no.Text.Trim()))
            {
                searchParams.Add(new SearchParam("out_trade_no", this.txtout_trade_no.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtLotno.Text))
            {
                searchParams.Add(new SearchParam("lotno", this.txtLotno.Text));
            }
            if (!string.IsNullOrEmpty(this.ddlbankCode.SelectedValue))
            {
                searchParams.Add(new SearchParam("bankCode", this.ddlbankCode.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtbankAccountName.Text))
            {
                searchParams.Add(new SearchParam("bankAccountName", this.txtbankAccountName.Text));
            }
            if (!string.IsNullOrEmpty(this.ddlSupplier.SelectedValue))
            {
                searchParams.Add(new SearchParam("tranapi", int.Parse(this.ddlSupplier.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.ddlmode.SelectedValue))
            {
                searchParams.Add(new SearchParam("mode", int.Parse(this.ddlmode.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.ddlaudit_status.SelectedValue))
            {
                searchParams.Add(new SearchParam("audit_status", int.Parse(this.ddlaudit_status.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.ddlpayment_status.SelectedValue))
            {
                searchParams.Add(new SearchParam("payment_status", int.Parse(this.ddlpayment_status.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.ddlis_cancel.SelectedValue))
            {
                bool paramValue = false;
                if (this.ddlis_cancel.SelectedValue == "1")
                {
                    paramValue = true;
                }
                searchParams.Add(new SearchParam("is_cancel", paramValue));
            }
            if (!string.IsNullOrEmpty(this.ddlnotifystatus.SelectedValue))
            {
                searchParams.Add(new SearchParam("notifystatus", int.Parse(this.ddlnotifystatus.SelectedValue)));
            }
            return this.stlAgtBLL.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty, 1);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                if (this.audit_status > -1)
                {
                    this.ddlaudit_status.SelectedValue = this.audit_status.ToString();
                }
                if (this.payment_status > -1)
                {
                    this.ddlpayment_status.SelectedValue = this.payment_status.ToString();
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
                this.BindData();
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string msg = string.Empty;
            string str2 = string.Empty;
            if (e.CommandArgument != null)
            {
                str2 = e.CommandArgument.ToString();
            }
            if (!string.IsNullOrEmpty(str2))
            {
                if (e.CommandName == "Cancel")
                {
                    msg = this.stlAgtBLL.doCancel(str2);
                }
                else
                {
                    viviapi.Model.Withdraw.settledAgent model;
                    if (e.CommandName == "Audit")
                    {
                        msg = this.stlAgtBLL.doAudit(str2, base.currentManage.id, base.currentManage.username);
                        if (msg == "审核成功")
                        {
                            model = this.stlAgtBLL.GetModel(str2);
                            if (model != null)
                            {
                                Withdraw.InitDistribution2(model);
                            }
                        }
                    }
                    else if (e.CommandName == "Refuse")
                    {
                        msg = this.stlAgtBLL.doRefuse(str2, base.currentManage.id, base.currentManage.username);
                    }
                    else if (e.CommandName == "paysuccess")
                    {
                        msg = this.stlAgtBLL.PaySuccess(str2);
                    }
                    else if (e.CommandName == "payfail")
                    {
                        msg = this.stlAgtBLL.PayFail(str2);
                    }
                    else if (e.CommandName == "Reissue")
                    {
                        this.stlAgtBLL.DoNotify(str2);
                        msg = "请求已提交";
                    }
                    else if (e.CommandName == "ResendToApi")
                    {
                        model = this.stlAgtBLL.GetModel(str2);
                        if (model != null)
                        {
                            Withdraw.InitDistribution2(model);
                        }
                        msg = "请求已提交";
                    }
                }
            }
            base.AlertAndRedirect(msg);
            this.BindData();
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                int num = Convert.ToByte(DataBinder.Eval(e.Item.DataItem, "issure"));
                int num2 = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "audit_status"));
                int num3 = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "payment_status"));
                bool flag = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "is_cancel"));
                if ((num == 2) && !flag)
                {
                    Button button = (Button)e.Item.FindControl("btnCancel");
                    Button button2 = (Button)e.Item.FindControl("btnAudits");
                    Button button3 = (Button)e.Item.FindControl("btnRefuse");
                    Button button4 = (Button)e.Item.FindControl("btnpaysuccess");
                    Button button5 = (Button)e.Item.FindControl("btnpayfail");
                    button.Visible = num2 == 1;
                    button2.Visible = num2 == 1;
                    button3.Visible = num2 == 1;
                    if ((num2 == 2) && (num3 == 1))
                    {
                        if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "tranApi")) > 0)
                        {
                            if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "suppstatus")) == 0)
                            {
                                Button button6 = (Button)e.Item.FindControl("btnResendToApi");
                                button6.Visible = true;
                            }
                        }
                        else
                        {
                            button4.Visible = true;
                            button5.Visible = true;
                        }
                    }
                }
                Button button7 = (Button)e.Item.FindControl("btnReissue");
                byte num6 = Convert.ToByte(DataBinder.Eval(e.Item.DataItem, "mode"));
                button7.Visible = num6 == 1;
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Financial))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        public int audit_status
        {
            get
            {
                return WebBase.GetQueryStringInt32("audit_status", -1);
            }
        }

        public int payment_status
        {
            get
            {
                return WebBase.GetQueryStringInt32("payment_status", -1);
            }
        }
    }
}

