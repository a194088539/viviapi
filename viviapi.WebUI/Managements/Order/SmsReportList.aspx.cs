namespace viviapi.WebUI.Managements.Order
{
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

    public class SmsReportList : ManagePageBase
    {
        protected Button btn_Search;
        protected DropDownList ddlmange;
        protected DropDownList ddlNotifyStatus;
        protected DropDownList ddlOrderStatus;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected AspNetPager Pager1;
        protected Repeater rptOrders;
        protected TextBox StimeBox;
        protected TextBox txtmobile;
        protected TextBox txtOrderId;
        protected TextBox txtSuppOrder;
        protected TextBox txtuserid;
        protected TextBox txtUserOrder;

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            this.LoadData();
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
            searchParams.Add(new SearchParam("ordertype", 1));
            searchParams.Add(new SearchParam("status", "<>", 1));
            int result = 0;
            if (!string.IsNullOrEmpty(this.txtOrderId.Text.Trim()))
            {
                searchParams.Add(new SearchParam("orderId_like", this.txtOrderId.Text));
            }
            if (!string.IsNullOrEmpty(this.txtuserid.Text.Trim()) && int.TryParse(this.txtuserid.Text.Trim(), out result))
            {
                searchParams.Add(new SearchParam("userid", result));
            }
            if (!string.IsNullOrEmpty(this.txtmobile.Text.Trim()))
            {
                searchParams.Add(new SearchParam("mobile", this.txtmobile.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtUserOrder.Text.Trim()))
            {
                searchParams.Add(new SearchParam("userorder", this.txtUserOrder.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtSuppOrder.Text.Trim()))
            {
                searchParams.Add(new SearchParam("supplierOrder", this.txtSuppOrder.Text.Trim()));
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
            try
            {
                if (e.CommandName == "Reissue")
                {
                    string str = e.CommandArgument.ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        string str2 = new OrderBankNotify().SynchronousNotify(str);
                        base.AlertAndRedirect("返回：" + str2);
                    }
                }
            }
            catch (Exception exception)
            {
                base.AlertAndRedirect(exception.Message);
            }
        }

        protected void rptOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
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

