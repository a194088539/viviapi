namespace viviapi.WebUI.Managements
{
    using Aspose.Cells;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using Wuqi.Webdiyer;

    public class Pays : ManagePageBase
    {
        protected Button btnAllSettle;
        protected Button btnExport;
        protected Button btnSearch;
        protected DropDownList ddlbankName;
        protected DropDownList ddlmode;
        protected DropDownList ddlSupplier;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected AspNetPager Pager1;
        protected RadioButtonList rbl_export_mode;
        protected Repeater rptList;
        protected TextBox txtAccount;
        protected TextBox txtItemInfoId;
        protected TextBox txtPassWord;
        protected TextBox txtpayeeName;
        protected TextBox txtUserId;

        private void BindData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("status", 2));
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
                            row["StatusText"] = "<a href=\"Pay.aspx?action=modi&ID=" + row["ID"].ToString() + "\">修改</a>&nbsp;&nbsp;<a href=\"Pay.aspx?action=pay&ID=" + row["ID"].ToString() + "\">进行支付</a>";
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
        }

        protected void btnAllSettle_Click(object sender, EventArgs e)
        {
            string str = base.Request.Form["ischecked"];
            if (string.IsNullOrEmpty(this.txtPassWord.Text))
            {
                base.AlertAndRedirect("请输入二级密码");
            }
            else if (!ManageFactory.SecPwdVaild(this.txtPassWord.Text.Trim()))
            {
                base.AlertAndRedirect("二级密码不正确");
            }
            else if (!string.IsNullOrEmpty(str))
            {
                if (SettledFactory.BatchSettle(str))
                {
                    base.AlertAndRedirect("支付成功!");
                    this.BindData();
                }
                else
                {
                    base.AlertAndRedirect("支付失败!");
                }
            }
            else
            {
                base.AlertAndRedirect("请选择要支付的记录!");
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.rbl_export_mode.SelectedValue == "2")
                {
                    string str = base.Request.Form["ischecked"];
                    if (!string.IsNullOrEmpty(str))
                    {
                        StringBuilder builder = new StringBuilder();
                        DataTable table = SettledFactory.Export(str);
                        foreach (DataRow row in table.Rows)
                        {
                            builder.AppendFormat("{0};{1};{2};{3};--;--;{4:f2}", new object[] { row["UserName"], row["PayeeName"], row["Account"], row["PayeeBank"], row["RealAmt"] });
                            builder.Append("\r\n");
                        }
                        string str2 = builder.ToString();
                        StringWriter writer = new StringWriter();
                        writer.Write(str2);
                        writer.WriteLine();
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.Buffer = false;
                        HttpContext.Current.Response.Charset = "GB2312";
                        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                        HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                        HttpContext.Current.Response.Write(writer);
                        HttpContext.Current.Response.End();
                    }
                }
                else
                {
                    DataSet data = this.GetData();
                    if (data != null)
                    {
                        DataTable dataTable = data.Tables[1];
                        dataTable.Columns.Add("sName", typeof(string));
                        foreach (DataRow row2 in dataTable.Rows)
                        {
                            row2["sName"] = Enum.GetName(typeof(SettledStatus), row2["status"]);
                            row2["PayeeBank"] = SettledFactory.GetSettleBankName(row2["PayeeBank"].ToString());
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
            searchParams.Add(new SearchParam("status", 2));
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
                this.BindData();
            }
        }

        protected void Pager1_PageChanging(object src, PageChangingEventArgs e)
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
    }
}

