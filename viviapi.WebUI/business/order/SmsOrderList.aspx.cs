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
    using viviapi.SysConfig;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class SmsOrderList : BusinessPageBase
    {
        protected Button btn_Search;
        protected DropDownList ddlmange;
        protected DropDownList ddlNotifyStatus;
        protected DropDownList ddlOrderStatus;
        protected HtmlGenericControl divmoney;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected AspNetPager Pager1;
        protected Repeater rptOrders;
        protected HtmlGenericControl spangmmoney;
        protected TextBox StimeBox;
        protected string TotalCommission = "0.00";
        protected string TotalProfit = string.Empty;
        protected string TotalPromATM = string.Empty;
        protected string TotalTranATM = string.Empty;
        protected string TotalUserATM = string.Empty;
        protected TextBox txtmobile;
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
            try
            {
                DateTime minValue = DateTime.MinValue;
                if (userId > 0)
                {
                    DateTime? nullable = new DateTime?(UserAccessTime.GetModel(userId).lastAccesstime);
                    if (nullable.HasValue)
                    {
                        minValue = nullable.Value;
                    }
                    return Convert.ToDateTime(completeTime).Subtract(minValue).TotalMinutes;
                }
                return 0.0;
            }
            catch
            {
                return 0.0;
            }
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

        protected string getsupplierName(object obj)
        {
            if ((obj == DBNull.Value) || (obj == null))
            {
                return string.Empty;
            }
            return SupplierFactory.GetModelByCode(int.Parse(obj.ToString())).name;
        }

        private void InitForm()
        {
            if (this.UserId > 0)
            {
                this.txtuserid.Text = this.UserId.ToString();
            }
            if (this.Status > 0)
            {
                this.ddlOrderStatus.SelectedValue = this.Status.ToString();
            }
            this.ddlmange.Items.Add("--请选择业务员--");
            DataTable table = ManageFactory.GetList(" status =1").Tables[0];
            foreach (DataRow row in table.Rows)
            {
                this.ddlmange.Items.Add(new ListItem(row["username"].ToString(), row["id"].ToString()));
            }
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            if (base.currentManage.isSuperAdmin <= 0)
            {
                searchParams.Add(new SearchParam("manageId", base.ManageId));
            }
            int result = 0;
            if (!string.IsNullOrEmpty(this.txtOrderId.Text.Trim()))
            {
                searchParams.Add(new SearchParam("orderId_like", this.txtOrderId.Text));
            }
            if (!string.IsNullOrEmpty(this.txtuserid.Text.Trim()) && int.TryParse(this.txtuserid.Text.Trim(), out result))
            {
                searchParams.Add(new SearchParam("userid", result));
            }
            if (!string.IsNullOrEmpty(this.txtUserOrder.Text.Trim()))
            {
                searchParams.Add(new SearchParam("userorder", this.txtUserOrder.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtSuppOrder.Text.Trim()))
            {
                searchParams.Add(new SearchParam("supplierOrder", this.txtSuppOrder.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtmobile.Text.Trim()))
            {
                searchParams.Add(new SearchParam("mobile", this.txtmobile.Text.Trim()));
            }
            if ((!string.IsNullOrEmpty(this.ddlOrderStatus.SelectedValue) && int.TryParse(this.ddlOrderStatus.SelectedValue, out result)) && (result > 0))
            {
                searchParams.Add(new SearchParam("status", result));
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
            DataSet set = new OrderSms().PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptOrders.DataSource = set.Tables[1];
            this.rptOrders.DataBind();
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

        protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string str;
            if (e.CommandName == "Reissue")
            {
                str = e.CommandArgument.ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    string str2 = new OrderSmsNotify().SynchronousNotify(str);
                    base.AlertAndRedirect("返回：" + str2);
                }
            }
            else if (e.CommandName == "ResetOrder")
            {
                string str3 = e.CommandArgument.ToString();
                if (!string.IsNullOrEmpty(str3))
                {
                    string[] strArray = str3.Split(new char[] { '$' });
                    base.Response.Redirect(string.Format("ResetOrder.aspx?orderid={0}&oclass=3&supp={1}&amt={2}", strArray[0], strArray[1], strArray[2]));
                }
            }
            else
            {
                OrderSms sms;
                if (e.CommandName == "Deduct")
                {
                    str = e.CommandArgument.ToString();
                    sms = new OrderSms();
                    if (sms.Deduct(str))
                    {
                        base.AlertAndRedirect("扣量成功");
                    }
                    else
                    {
                        base.AlertAndRedirect("扣量失败，可能是余额不足");
                    }
                    this.LoadData();
                }
                else if (e.CommandName == "ReDeduct")
                {
                    str = e.CommandArgument.ToString();
                    sms = new OrderSms();
                    if (sms.ReDeduct(str))
                    {
                        base.AlertAndRedirect("还单成功");
                    }
                    else
                    {
                        base.AlertAndRedirect("还单失败");
                    }
                    this.LoadData();
                }
            }
        }

        protected void rptOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Button button = e.Item.FindControl("btnReissue") as Button;
                Button button2 = e.Item.FindControl("btnRest") as Button;
                Button button3 = e.Item.FindControl("btnDeduct") as Button;
                Button button4 = e.Item.FindControl("btnReDeduct") as Button;
                int userId = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "userid").ToString());
                string str2 = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                if (str2 != null)
                {
                    if (!(str2 == "1"))
                    {
                        if (str2 == "2")
                        {
                            button.Visible = true;
                            button2.Visible = false;
                            button4.Visible = false;
                            button3.OnClientClick = "return confirm('是否确定扣量？')";
                            object completeTime = DataBinder.Eval(e.Item.DataItem, "CompleteTime");
                            double difftime = this.GetDifftime(userId, completeTime);
                            if (difftime > RuntimeSetting.DeductSafetyTime)
                            {
                                button3.Text = "扣";
                            }
                            else if ((difftime > 0.0) && (difftime <= RuntimeSetting.DeductSafetyTime))
                            {
                                button3.Text = "危险";
                            }
                            else
                            {
                                button3.Text = "不能";
                            }
                        }
                        else if (str2 == "4")
                        {
                            button.Visible = true;
                            button2.Visible = false;
                            button3.Visible = false;
                            button4.Visible = false;
                        }
                        else if (str2 == "8")
                        {
                            button.Visible = true;
                            button2.Visible = false;
                            button3.Visible = false;
                            button4.Visible = true;
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

        protected int Status
        {
            get
            {
                return WebBase.GetQueryStringInt32("status", 0);
            }
        }

        protected int UserId
        {
            get
            {
                return WebBase.GetQueryStringInt32("userid", 0);
            }
        }
    }
}

