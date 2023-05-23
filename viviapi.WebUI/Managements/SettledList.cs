namespace viviapi.WebUI.Managements
{
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

    public class SettledList : ManagePageBase
    {
        protected Button btnAllSettle;
        protected Button btnExport;
        protected Button btnSearch;
        protected DropDownList ddlAcctype;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected TextBox KeyWords;
        protected AspNetPager Pager1;
        protected Repeater rptList;
        protected DropDownList SeachType;
        protected DropDownList StatusList;
        protected TextBox txtPassWord;

        private void BindData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            if (!string.IsNullOrEmpty(this.StatusList.SelectedValue))
            {
                searchParams.Add(new SearchParam("status", int.Parse(this.StatusList.SelectedValue)));
            }
            if (this.ddlAcctype.SelectedValue != "0")
            {
                searchParams.Add(new SearchParam("settmode", int.Parse(this.ddlAcctype.SelectedValue)));
            }
            string s = this.KeyWords.Text.Trim();
            int result = 0;
            int.TryParse(s, out result);
            if (result > 0)
            {
                string selectedValue = this.SeachType.SelectedValue;
                if (string.IsNullOrEmpty(selectedValue))
                {
                    searchParams.Add(new SearchParam("all", result));
                }
                else if (selectedValue.ToLower() == "id")
                {
                    searchParams.Add(new SearchParam("id", result));
                }
                else if (selectedValue.ToLower() == "userid")
                {
                    searchParams.Add(new SearchParam("userid", result));
                }
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
                            row["StatusText"] = "<a href=\"Settled.aspx?action=modi&ID=" + row["ID"].ToString() + "\">修改</a>&nbsp;&nbsp;<a href=\"Settled.aspx?action=pay&ID=" + row["ID"].ToString() + "\">进行支付</a>";
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.StatusList.Items.Add(new ListItem("—状态—", ""));
                foreach (int num in Enum.GetValues(typeof(SettledStatus)))
                {
                    this.StatusList.Items.Add(new ListItem(Enum.GetName(typeof(SettledStatus), num), num.ToString()));
                }
                this.StatusList.SelectedValue = 2.ToString();
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

