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

    public class CardReportList : ManagePageBase
    {
        protected Button btn_Search;
        protected DropDownList ddlChannelType;
        protected DropDownList ddlmange;
        protected DropDownList ddlNotifyStatus;
        protected DropDownList ddlOrderStatus;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected AspNetPager Pager1;
        protected Repeater rptOrders;
        protected TextBox StimeBox;
        protected TextBox txtCardNo;
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
            if (this.Status > -1)
            {
                this.ddlOrderStatus.SelectedValue = this.Status.ToString();
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
            searchParams.Add(new SearchParam("ordertype", 1));
            searchParams.Add(new SearchParam("status", "<>", 1));
            int result = 0;
            if (!string.IsNullOrEmpty(this.txtCardNo.Text.Trim()))
            {
                searchParams.Add(new SearchParam("cardno", this.txtCardNo.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtOrderId.Text.Trim()))
            {
                searchParams.Add(new SearchParam("orderId_like", this.txtOrderId.Text));
            }
            if (!string.IsNullOrEmpty(this.txtuserid.Text.Trim()) && int.TryParse(this.txtuserid.Text.Trim(), out result))
            {
                searchParams.Add(new SearchParam("userid", result));
            }
            if ((!string.IsNullOrEmpty(this.ddlChannelType.SelectedValue) && int.TryParse(this.ddlChannelType.SelectedValue, out result)) && (result > 0))
            {
                searchParams.Add(new SearchParam("typeId", result));
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
            DataSet set = new OrderCard().PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptOrders.DataSource = set.Tables[1];
            this.rptOrders.DataBind();
            if (this.currPage > 0)
            {
                this.Pager1.CurrentPageIndex = this.currPage;
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
            string url = ((((((((((("CardReportList.aspx?status=" + this.ddlOrderStatus.SelectedValue) + "&ctype=" + this.ddlChannelType.SelectedValue) + "&userid=" + this.txtuserid.Text) + "&ns=" + this.ddlNotifyStatus.SelectedValue) + "&ka=" + this.txtCardNo.Text) + "&stime=" + this.StimeBox.Text) + "&etime=" + this.EtimeBox.Text) + "&mid=" + this.ddlmange.SelectedValue) + "&orderid=" + this.txtOrderId.Text) + "&userorder=" + this.txtUserOrder.Text) + "&supporder=" + this.txtSuppOrder.Text) + "&currpage=" + this.Pager1.CurrentPageIndex;
            try
            {
                if (e.CommandName == "Reissue")
                {
                    string str2 = e.CommandArgument.ToString();
                    if (!string.IsNullOrEmpty(str2))
                    {
                        string str3 = new OrderCardNotify().SynchronousNotify(str2);
                        base.AlertAndRedirect("返回：" + str3, url);
                    }
                }
            }
            catch (Exception exception)
            {
                base.AlertAndRedirect(exception.Message, url);
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

