namespace viviapi.WebUI.Merchant
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.Settled;
    using viviapi.ETAPI;
    using viviapi.Model.Settled;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class importlist : UserPageBase
    {
        protected viviapi.BLL.Withdraw.settledAgentSummary _bll = new viviapi.BLL.Withdraw.settledAgentSummary();
        private TocashSchemeInfo _scheme = null;
        protected Button b_search;
        protected HtmlInputText edate;
        protected AspNetPager Pager1;
        protected Repeater rptrecharges;
        protected HtmlInputText sdate;
        protected viviapi.BLL.Withdraw.settledAgent stlAgtBLL = new viviapi.BLL.Withdraw.settledAgent();

        private string Affirm(string lotno, int status)
        {
            string message = string.Empty;
            int result = this._bll.Affirm(lotno, status, base.UserId, base.currentUser.UserName, ServerVariables.TrueIP);
            if (result == 0)
            {
                message = "????";
            }
            else if (result == 1)
            {
                message = "?????";
            }
            else if (result == 2)
            {
                message = "????????????";
            }
            else if (result == 3)
            {
                message = "???????";
            }
            else if (result == 4)
            {
                message = "????";
            }
            else
            {
                message = "????";
            }
            if ((status == 2) && (result == 0))
            {
                if ((this.scheme.tranRequiredAudit != 0) || (this.scheme.vaiInterface != 1))
                {
                    return message;
                }
                List<viviapi.Model.Withdraw.settledAgent> list = this.stlAgtBLL.GetModelList("lotno=" + lotno);
                foreach (viviapi.Model.Withdraw.settledAgent item in list)
                {
                    if (!((item.suppid <= 0) || item.is_cancel))
                    {
                        Withdraw.InitDistribution2(item);
                    }
                }
            }
            return message;
        }

        protected void b_search_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        public string builderdate(string date, string hour, string m, string s)
        {
            return string.Format("{0} {1}:{2}:{3}", new object[] { date, hour, m, s });
        }

        private void InitForm()
        {
            this.sdate.Value = DateTime.Now.AddDays(-7.0).ToString("yyyy-MM-dd");
            this.edate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void LoadData()
        {
            List<SearchParam> listParam = new List<SearchParam>();
            listParam.Add(new SearchParam("userid", base.UserId));
            DateTime tempdt = DateTime.MinValue;
            if ((!string.IsNullOrEmpty(this.sdate.Value.Trim()) && DateTime.TryParse(this.sdate.Value.Trim(), out tempdt)) && (tempdt > DateTime.MinValue))
            {
                listParam.Add(new SearchParam("starttime", tempdt));
            }
            if ((!string.IsNullOrEmpty(this.edate.Value.Trim()) && DateTime.TryParse(this.edate.Value.Trim(), out tempdt)) && (tempdt > DateTime.MinValue))
            {
                listParam.Add(new SearchParam("endtime", tempdt.AddDays(1.0)));
            }
            DataSet pageData = this._bll.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty, false);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptrecharges.DataSource = pageData.Tables[1];
            this.rptrecharges.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitForm();
                this.LoadData();
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void rptDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) && (DataBinder.Eval(e.Item.DataItem, "audit_status").ToString() == "1"))
            {
                Button btnSure = e.Item.FindControl("btnSure") as Button;
                Button btnCancel = e.Item.FindControl("btnCancel") as Button;
                btnSure.Visible = true;
                btnCancel.Visible = true;
            }
            if ((e.Item.ItemType == ListItemType.Footer) && (this.rptrecharges.Items.Count == 0))
            {
                Literal lit = (Literal)e.Item.FindControl("litfoot");
                lit.Text = " <tfoot>\r\n                        <tr>\r\n                            <td colspan=\"10\" class=\"nomsg\">\r\n                                ?_?^..????\r\n                            </td>\r\n                        </tr>\r\n                     </tfoot>     ";
            }
        }

        protected void rptrecharges_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                string message = string.Empty;
                string lotno = e.CommandArgument.ToString();
                if (e.CommandName == "sure")
                {
                    message = this.Affirm(lotno, 2);
                }
                else if (e.CommandName == "cancel")
                {
                    message = this.Affirm(lotno, 3);
                }
                base.AlertAndRedirect(message, "importlist.aspx");
            }
        }

        protected TocashSchemeInfo scheme
        {
            get
            {
                if (this._scheme == null)
                {
                    this._scheme = TocashScheme.GetModelByUser(2, base.UserId);
                }
                return this._scheme;
            }
        }
    }
}

