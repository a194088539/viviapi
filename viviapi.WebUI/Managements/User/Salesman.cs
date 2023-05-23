namespace viviapi.WebUI.Managements.User
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Settled;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public class Salesman : ManagePageBase
    {
        protected Button btnSearch;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected Repeater rptmanage;
        protected HtmlInputHidden selectedUsers;
        protected TextBox txtmanageId;
        protected string wzfmoney = string.Empty;
        protected string yzfmoney = string.Empty;

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected string GetParm(object userid, object balance, object Freeze, object unpayment)
        {
            try
            {
                decimal num;
                decimal num2;
                decimal num3;
                if (balance == DBNull.Value)
                {
                    num = 0M;
                }
                else
                {
                    num = Convert.ToDecimal(balance);
                }
                if (Freeze == DBNull.Value)
                {
                    num2 = 0M;
                }
                else
                {
                    num2 = Convert.ToDecimal(Freeze);
                }
                if (unpayment == DBNull.Value)
                {
                    num3 = 0M;
                }
                else
                {
                    num3 = Convert.ToDecimal(unpayment);
                }
                return string.Format("{0}${1}${2}${3}", new object[] { userid, num, num2, num3 });
            }
            catch
            {
                return string.Format("{0}${1}${2}${3}", new object[] { "0.00", "0.00", "0.00", "0.00" });
            }
        }

        private void LoadData()
        {
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(this.txtmanageId.Text.Trim()))
            {
                int result = 0;
                if (int.TryParse(this.txtmanageId.Text.Trim(), out result))
                {
                    where = where + string.Format(" and id = {0}", result);
                }
            }
            DataTable table = ManageFactory.GetList(where).Tables[0];
            table.Columns.Add("Commissiontypeview");
            foreach (DataRow row in table.Rows)
            {
                if (row["Commissiontype"] != DBNull.Value)
                {
                    row["Commissiontypeview"] = (row["Commissiontype"].ToString() == "2") ? "按支付金额%" : "按条固定提成";
                }
            }
            this.rptmanage.DataSource = table;
            this.rptmanage.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.LoadData();
            }
        }

        protected void Pager1_PageChanging(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void rptUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Settled")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                Manage model = ManageFactory.GetModel(id);
                if (model != null)
                {
                    if (!model.balance.HasValue)
                    {
                        base.AlertAndRedirect("结算金额大于余额 操作有误");
                    }
                    else
                    {
                        TextBox box = e.Item.FindControl("txtpayAmt") as TextBox;
                        decimal num2 = decimal.Parse(box.Text.Trim());
                        if (num2 <= 0M)
                        {
                            base.AlertAndRedirect("请输入正确的金额");
                        }
                        else if (num2 > model.balance.Value)
                        {
                            base.AlertAndRedirect("结算金额大于余额 操作有误");
                        }
                        else if (ManageTrade.Add(id, 0, 3, "", DateTime.Now, 0M - num2, "提现") > 0)
                        {
                            base.AlertAndRedirect("结算成功", "Salesman.aspx");
                        }
                        else
                        {
                            base.AlertAndRedirect("结算失败，请重试!");
                        }
                    }
                }
                else
                {
                    base.AlertAndRedirect("参数错误!");
                }
            }
        }

        protected void rptUsersItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                int id = 0;
                id = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id"));
                Literal literal = (Literal)e.Item.FindControl("litUserCount");
                Literal literal2 = (Literal)e.Item.FindControl("litMonthIncome");
                Literal literal3 = (Literal)e.Item.FindControl("litDayIncome");
                Literal literal4 = (Literal)e.Item.FindControl("litMonthSetted");
                TextBox box = (TextBox)e.Item.FindControl("txtpayAmt");
                literal.Text = ManageFactory.GetManageUsers(id).ToString();
                decimal num2 = 0M;
                decimal num3 = 0M;
                DateTime firstDayOfMonth = base.FirstDayOfMonth;
                DateTime now = DateTime.Now;
                num2 = decimal.Round(ManageTrade.GetManageIncome(id, firstDayOfMonth, now), 2);
                num3 = decimal.Round(ManageTrade.GetSettledAmt(id, firstDayOfMonth, now), 2);
                literal2.Text = num2.ToString();
                literal4.Text = num3.ToString();
                firstDayOfMonth = base.ToDayFirstTime;
                now = DateTime.Now;
                literal3.Text = decimal.Round(ManageTrade.GetManageIncome(id, firstDayOfMonth, now), 2).ToString();
                box.Attributes["onkeypress"] = "if (event.keyCode < 45 || event.keyCode > 57) event.returnValue = false;";
                object obj2 = DataBinder.Eval(e.Item.DataItem, "balance");
                if (obj2 != DBNull.Value)
                {
                    box.Text = decimal.Round(Convert.ToDecimal(obj2), 2).ToString();
                }
                else
                {
                    box.Text = "0.00";
                }
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

        public string cmd
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", "");
            }
        }

        public string orderBy
        {
            get
            {
                return WebBase.GetQueryStringString("orderby", "balance");
            }
        }

        public string orderByType
        {
            get
            {
                return WebBase.GetQueryStringString("type", "asc");
            }
        }

        public int proid
        {
            get
            {
                return WebBase.GetQueryStringInt32("proid", 0);
            }
        }

        public int UserID
        {
            get
            {
                return WebBase.GetQueryStringInt32("ID", 0);
            }
        }

        public string UserStatus
        {
            get
            {
                return WebBase.GetQueryStringString("UserStatus", "");
            }
        }
    }
}

