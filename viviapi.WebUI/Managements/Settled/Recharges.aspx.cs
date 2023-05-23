namespace viviapi.WebUI.Managements.Settled
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.APP;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class Recharges : ManagePageBase
    {
        protected Button btnSearch;
        protected DropDownList ddlstatus;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected AspNetPager Pager1;
        protected apprecharge rechargeBLL = new apprecharge();
        protected Repeater recharges;
        protected HtmlInputHidden selectedUsers;
        protected TextBox StimeBox;
        protected TextBox txtuserId;
        protected string wzfmoney = "0.00";
        protected string yzfmoney = string.Empty;

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected string GetStatusName(object status)
        {
            if (status == DBNull.Value)
            {
                return string.Empty;
            }
            return this.rechargeBLL.GetStatusName(Convert.ToInt32(status));
        }

        private void LoadData()
        {
            string orderby = "id desc";
            if (this.ViewState["__Sort"] != null)
            {
                orderby = this.ViewState["__Sort"].ToString();
            }
            List<SearchParam> searchParams = new List<SearchParam>();
            string s = this.txtuserId.Text.Trim();
            int result = 0;
            if (int.TryParse(s, out result))
            {
                searchParams.Add(new SearchParam("userid", result));
            }
            DateTime minValue = DateTime.MinValue;
            if ((!string.IsNullOrEmpty(this.StimeBox.Text.Trim()) && DateTime.TryParse(this.StimeBox.Text.Trim(), out minValue)) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("starttime", this.StimeBox.Text.Trim()));
            }
            if ((!string.IsNullOrEmpty(this.EtimeBox.Text.Trim()) && DateTime.TryParse(this.EtimeBox.Text.Trim(), out minValue)) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("endtime", minValue.AddDays(1.0)));
            }
            if (!string.IsNullOrEmpty(this.ddlstatus.SelectedValue))
            {
                searchParams.Add(new SearchParam("status", int.Parse(this.ddlstatus.SelectedValue)));
            }
            DataSet set = this.rechargeBLL.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby, true);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.recharges.DataSource = set.Tables[1];
            this.recharges.DataBind();
            try
            {
                DataRow row = set.Tables[2].Rows[0];
                this.wzfmoney = string.Format("{0:f2}", row["realPayAmt"]);
            }
            catch
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Now.AddDays(-30.0).ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.LoadData();
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void Pager1_PageChanging(object src, PageChangingEventArgs e)
        {
            this.LoadData();
        }

        protected void recharges_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                LinkButton button = (LinkButton)e.Item.FindControl("iBtn" + e.CommandName.Trim());
                if (this.ViewState[e.CommandName.Trim()] == null)
                {
                    this.ViewState[e.CommandName.Trim()] = "DESC";
                    button.Text = button.Text + "▼";
                }
                else if (this.ViewState[e.CommandName.Trim()].ToString().Trim() == "DESC")
                {
                    this.ViewState[e.CommandName.Trim()] = "ASC";
                    if (button.Text.IndexOf("▼") != -1)
                    {
                        button.Text = button.Text.Trim().Replace("▼", "▲");
                    }
                    else
                    {
                        button.Text = button.Text + "▲";
                    }
                }
                else
                {
                    this.ViewState[e.CommandName.Trim()] = "DESC";
                    if (button.Text.IndexOf("▲") != -1)
                    {
                        button.Text = button.Text.Replace("▲", "▼");
                    }
                    else
                    {
                        button.Text = button.Text + "▼";
                    }
                }
                this.ViewState["__text"] = button.Text;
                this.ViewState["__id"] = e.CommandName.Trim();
                this.ViewState["__Sort"] = e.CommandName.ToString().Trim() + " " + this.ViewState[e.CommandName.Trim()].ToString().Trim();
                this.LoadData();
            }
            try
            {
                if (e.CommandName == "Freeze")
                {
                    string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                    base.Response.Redirect(string.Format("Freeze.aspx?ID={0}&amt={1}&reason={2}", strArray[0], strArray[1], strArray[2]), false);
                }
                if (e.CommandName == "btnReplenish")
                {
                    base.Response.Redirect(string.Format("resetrecharges.aspx?orderid={0}", e.CommandArgument), false);
                }
            }
            catch (Exception exception)
            {
                base.AlertAndRedirect(exception.Message);
            }
        }

        protected void recharges_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Header) && (this.ViewState["__id"] != null))
            {
                LinkButton button = (LinkButton)e.Item.FindControl("iBtn" + this.ViewState["__id"].ToString().Trim());
                button.Text = this.ViewState["__text"].ToString();
            }
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                int num = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "status"));
                Button button2 = (Button)e.Item.FindControl("btnReplenish");
                button2.Visible = num == 1;
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

