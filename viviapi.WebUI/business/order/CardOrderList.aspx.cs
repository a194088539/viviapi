namespace viviapi.WebUI.business.Order
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.User;
    using viviapi.Model;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class CardOrderList : BusinessPageBase
    {
        private OrderCard bll = new OrderCard();
        protected Button btn_Search;
        protected DropDownList ddlChannelType;
        protected DropDownList ddlmange;
        protected DropDownList ddlNotifyStatus;
        protected HtmlGenericControl divmoney;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected AspNetPager Pager1;
        protected Repeater rptOrders;
        protected TextBox StimeBox;
        protected string TotalCommission = "0.00";
        protected string TotalProfit = string.Empty;
        protected string TotalPromATM = string.Empty;
        protected string TotalTranATM = string.Empty;
        protected string TotalUserATM = string.Empty;
        protected TextBox txtCardNo;
        protected TextBox txtOrderId;
        protected TextBox txtSuppOrder;
        protected TextBox txtuserid;
        protected TextBox txtUserOrder;

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        public double GetDifftime(int userId, object completeTime)
        {
            DateTime minValue = DateTime.MinValue;
            UserAccessTimeInfo model = UserAccessTime.GetModel(userId);
            if (model == null)
            {
                return 1000.0;
            }
            DateTime? nullable = new DateTime?(model.lastAccesstime);
            if (nullable.HasValue)
            {
                minValue = nullable.Value;
            }
            return Convert.ToDateTime(completeTime).Subtract(minValue).TotalMinutes;
        }

        protected string GetParm(object orderid, object supp, object amt)
        {
            try
            {
                return string.Format("{0}${1}${2}", orderid, supp, amt);
            }
            catch
            {
                return string.Format("{0}${1}${2}", "", "", "0.00");
            }
        }

        protected string getversionName(object version)
        {
            if (version == DBNull.Value)
            {
                return string.Empty;
            }
            if (version.ToString() == "1")
            {
                return "多";
            }
            return "单";
        }

        private void InitForm()
        {
            if (this.UserId > -1)
            {
                this.txtuserid.Text = this.UserId.ToString();
            }
            if (this.ctype > -1)
            {
                this.ddlChannelType.SelectedValue = this.ctype.ToString();
            }
            if (this.NotifyStatus > -1)
            {
                this.ddlNotifyStatus.SelectedValue = this.NotifyStatus.ToString();
            }
            if (!string.IsNullOrEmpty(this.kano))
            {
                this.txtCardNo.Text = this.kano;
            }
            if (!string.IsNullOrEmpty(this.stime))
            {
                this.StimeBox.Text = this.stime;
            }
            if (!string.IsNullOrEmpty(this.etime))
            {
                this.EtimeBox.Text = this.etime;
            }
            if (!string.IsNullOrEmpty(this.sysorderid))
            {
                this.txtOrderId.Text = this.sysorderid;
            }
            if (!string.IsNullOrEmpty(this.userorderid))
            {
                this.txtUserOrder.Text = this.userorderid;
            }
            if (!string.IsNullOrEmpty(this.supporderid))
            {
                this.txtSuppOrder.Text = this.supporderid;
            }
            this.ddlmange.Items.Add("--请选择业务员--");
            DataTable table = ManageFactory.GetList(" status =1").Tables[0];
            foreach (DataRow row in table.Rows)
            {
                this.ddlmange.Items.Add(new ListItem(row["username"].ToString(), row["id"].ToString()));
            }
            if (this.MID > -1)
            {
                this.ddlmange.SelectedValue = this.MID.ToString();
            }
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            int result = 0;
            string str = this.txtOrderId.Text.Trim();
            string str2 = this.txtUserOrder.Text.Trim();
            string str3 = this.txtSuppOrder.Text.Trim();
            string str4 = this.txtCardNo.Text.Trim();
            if (!base.isSuperAdmin && (((string.IsNullOrEmpty(str) && string.IsNullOrEmpty(str2)) && string.IsNullOrEmpty(str3)) && string.IsNullOrEmpty(str4)))
            {
                searchParams.Add(new SearchParam("manageId", base.ManageId));
                searchParams.Add(new SearchParam("status", 2));
            }
            if (!string.IsNullOrEmpty(str))
            {
                searchParams.Add(new SearchParam("orderid", str));
            }
            if (!string.IsNullOrEmpty(str2))
            {
                searchParams.Add(new SearchParam("userorder", str2));
            }
            if (!string.IsNullOrEmpty(str3))
            {
                searchParams.Add(new SearchParam("supplierorder", str3));
            }
            if (!string.IsNullOrEmpty(str4))
            {
                searchParams.Add(new SearchParam("cardno", str4));
            }
            if (!string.IsNullOrEmpty(this.txtuserid.Text.Trim()) && int.TryParse(this.txtuserid.Text.Trim(), out result))
            {
                searchParams.Add(new SearchParam("userid", result));
            }
            if ((!string.IsNullOrEmpty(this.ddlChannelType.SelectedValue) && int.TryParse(this.ddlChannelType.SelectedValue, out result)) && (result > 0))
            {
                searchParams.Add(new SearchParam("typeId", result));
            }
            if ((!string.IsNullOrEmpty(this.ddlNotifyStatus.SelectedValue) && int.TryParse(this.ddlNotifyStatus.SelectedValue, out result)) && (result > 0))
            {
                searchParams.Add(new SearchParam("notifystat", result));
            }
            DateTime minValue = DateTime.MinValue;
            if ((!string.IsNullOrEmpty(this.StimeBox.Text.Trim()) && DateTime.TryParse(this.StimeBox.Text.Trim(), out minValue)) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("stime", this.StimeBox.Text.Trim()));
            }
            if ((!string.IsNullOrEmpty(this.EtimeBox.Text.Trim()) && DateTime.TryParse(this.EtimeBox.Text.Trim(), out minValue)) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("etime", minValue.AddDays(1.0)));
            }
            string orderby = string.Empty;
            DataSet set = new OrderCard().PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptOrders.DataSource = set.Tables[1];
            this.rptOrders.DataBind();
            if (this.currPage > -1)
            {
                this.Pager1.CurrentPageIndex = this.currPage;
            }
            DataTable table = set.Tables[2];
            this.TotalTranATM = table.Rows[0]["realvalue"].ToString();
            this.TotalUserATM = table.Rows[0]["payAmt"].ToString();
            this.TotalPromATM = table.Rows[0]["promAmt"].ToString();
            this.TotalProfit = table.Rows[0]["profits"].ToString();
            if (table.Rows[0]["commission"] != DBNull.Value)
            {
                this.TotalCommission = Convert.ToDecimal(table.Rows[0]["commission"]).ToString("f2");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.InitForm();
                this.LoadData();
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void rptcardDetail_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ResetOrder")
                {
                    string str = e.CommandArgument.ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        string[] strArray = str.Split(new char[] { '$' });
                        base.Response.Redirect(string.Format("ResetOrder.aspx?orderid={0}&oclass=2&supp={1}&amt={2}", strArray[0], strArray[1], strArray[2]));
                    }
                }
            }
            catch (Exception exception)
            {
                base.AlertAndRedirect(exception.Message);
            }
        }

        protected void rptcardDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Button button = e.Item.FindControl("btnitemRest") as Button;
                string str2 = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                if (str2 != null)
                {
                    if (!(str2 == "1"))
                    {
                        if (str2 == "2")
                        {
                            button.Visible = false;
                        }
                        else if (str2 == "4")
                        {
                            button.Visible = false;
                        }
                        else if (str2 == "8")
                        {
                            button.Visible = false;
                        }
                    }
                    else
                    {
                        button.Visible = true;
                    }
                }
            }
        }

        protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string url = "CardOrderList.aspx?status=2";
            url = ((((((((((url + "&ctype=" + this.ddlChannelType.SelectedValue) + "&userid=" + this.txtuserid.Text) + "&ns=" + this.ddlNotifyStatus.SelectedValue) + "&ka=" + this.txtCardNo.Text) + "&stime=" + this.StimeBox.Text) + "&etime=" + this.EtimeBox.Text) + "&mid=" + this.ddlmange.SelectedValue) + "&orderid=" + this.txtOrderId.Text) + "&userorder=" + this.txtUserOrder.Text) + "&supporder=" + this.txtSuppOrder.Text) + "&currpage=" + this.Pager1.CurrentPageIndex;
            try
            {
                string str2;
                if (e.CommandName == "Reissue")
                {
                    str2 = e.CommandArgument.ToString();
                    if (!string.IsNullOrEmpty(str2))
                    {
                        string str3 = new OrderCardNotify().SynchronousNotify(str2);
                        base.AlertAndRedirect("返回：" + str3, url);
                    }
                }
                else if (e.CommandName == "ResetOrder")
                {
                    string str4 = e.CommandArgument.ToString();
                    if (!string.IsNullOrEmpty(str4))
                    {
                        string[] strArray = str4.Split(new char[] { '$' });
                        base.Response.Redirect(string.Format("ResetOrder.aspx?orderid={0}&oclass=2&supp={1}&amt={2}", strArray[0], strArray[1], strArray[2]));
                    }
                }
                else if (e.CommandName == "Deduct")
                {
                    str2 = e.CommandArgument.ToString();
                    if (this.bll.Deduct(str2))
                    {
                        base.AlertAndRedirect("扣量成功", url);
                    }
                    else
                    {
                        base.AlertAndRedirect("扣量失败，可能是余额不足", url);
                    }
                    this.LoadData();
                }
                else if (e.CommandName == "ReDeduct")
                {
                    str2 = e.CommandArgument.ToString();
                    OrderCard card = new OrderCard();
                    if (card.ReDeduct(str2))
                    {
                        base.AlertAndRedirect("还单成功", url);
                    }
                    else
                    {
                        base.AlertAndRedirect("还单失败", url);
                    }
                    this.LoadData();
                }
            }
            catch (Exception exception)
            {
                base.AlertAndRedirect(exception.Message, url);
            }
        }

        protected void rptOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HtmlTableCell cell;
            HtmlTableCell cell2;
            if ((e.Item.ItemType == ListItemType.Header) && !base.isSuperAdmin)
            {
                cell = e.Item.FindControl("th_profits") as HtmlTableCell;
                if (cell != null)
                {
                    cell.Visible = false;
                }
                cell2 = e.Item.FindControl("th_supplier") as HtmlTableCell;
                if (cell2 != null)
                {
                    cell2.Visible = false;
                }
            }
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                if (!base.isSuperAdmin)
                {
                    cell = e.Item.FindControl("tr_profits") as HtmlTableCell;
                    if (cell != null)
                    {
                        cell.Visible = false;
                    }
                    cell2 = e.Item.FindControl("tr_supplier") as HtmlTableCell;
                    if (cell2 != null)
                    {
                        cell2.Visible = false;
                    }
                }
                Button button = e.Item.FindControl("btnReissue") as Button;
                Button button2 = e.Item.FindControl("btnRest") as Button;
                Button button3 = e.Item.FindControl("btnDeduct") as Button;
                Button button4 = e.Item.FindControl("btnReDeduct") as Button;
                HtmlTableRow row = e.Item.FindControl("tr_carddetail") as HtmlTableRow;
                Literal literal = e.Item.FindControl("litimg") as Literal;
                int num = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "userid").ToString());
                int num2 = 0;
                object obj2 = DataBinder.Eval(e.Item.DataItem, "cardnum");
                if (obj2 != DBNull.Value)
                {
                    num2 = Convert.ToInt32(obj2);
                }
                if (num2 > 1)
                {
                    literal.Text = string.Format("<img src=\"../style/images/folder_close.gif\" style=\"cursor: hand\" onclick=\"collapse(this, '{0}')\" alt=\"\" />", row.ClientID);
                    string orderId = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "orderid"));
                    Repeater repeater = (Repeater)e.Item.FindControl("rptcardDetail");
                    DataTable table = this.bll.DataItemsByOrderId(orderId);
                    repeater.DataSource = table;
                    repeater.DataBind();
                }
                else
                {
                    row.Style.Add("display", "none");
                }
                string str3 = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                if (str3 != null)
                {
                    if (!(str3 == "1"))
                    {
                        if (str3 == "2")
                        {
                            button.Visible = true;
                            button2.Visible = false;
                            button4.Visible = false;
                            button3.Visible = false;
                        }
                        else if (str3 == "4")
                        {
                            button.Visible = true;
                            button2.Visible = false;
                            button3.Visible = false;
                            button4.Visible = false;
                        }
                        else if (str3 == "8")
                        {
                            button.Visible = true;
                            button2.Visible = false;
                            button3.Visible = false;
                            button4.Visible = true;
                        }
                        else if (str3 == "32")
                        {
                            button.Visible = false;
                            button2.Visible = false;
                            button3.Visible = false;
                            button4.Visible = false;
                        }
                    }
                    else
                    {
                        button.Visible = false;
                        button2.Visible = true;
                        button3.Visible = false;
                        button4.Visible = false;
                    }
                }
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Orders))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        protected int ctype
        {
            get
            {
                return WebBase.GetQueryStringInt32("ctype", -1);
            }
        }

        protected int currPage
        {
            get
            {
                return WebBase.GetQueryStringInt32("currpage", -1);
            }
        }

        protected string etime
        {
            get
            {
                return WebBase.GetQueryStringString("etime", string.Empty);
            }
        }

        protected string kano
        {
            get
            {
                return WebBase.GetQueryStringString("ka", string.Empty);
            }
        }

        protected int MID
        {
            get
            {
                return WebBase.GetQueryStringInt32("mid", -1);
            }
        }

        protected int NotifyStatus
        {
            get
            {
                return WebBase.GetQueryStringInt32("ns", -1);
            }
        }

        protected int Status
        {
            get
            {
                return WebBase.GetQueryStringInt32("status", -1);
            }
        }

        protected string stime
        {
            get
            {
                return WebBase.GetQueryStringString("stime", string.Empty);
            }
        }

        protected string supporderid
        {
            get
            {
                return WebBase.GetQueryStringString("supporder", string.Empty);
            }
        }

        protected string sysorderid
        {
            get
            {
                return WebBase.GetQueryStringString("orderid", string.Empty);
            }
        }

        protected int UserId
        {
            get
            {
                return WebBase.GetQueryStringInt32("userid", -1);
            }
        }

        protected string userorderid
        {
            get
            {
                return WebBase.GetQueryStringString("userorder", string.Empty);
            }
        }
    }
}

