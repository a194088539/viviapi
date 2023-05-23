namespace viviapi.WebUI.Userlogin.behalf
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
        protected HtmlForm form1;
        protected AspNetPager Pager1;
        protected Repeater rptrecharges;
        protected HtmlInputText sdate;
        protected viviapi.BLL.Withdraw.settledAgent stlAgtBLL = new viviapi.BLL.Withdraw.settledAgent();

        private string Affirm(string lotno, int status)
        {
            string str = string.Empty;
            int num = this._bll.Affirm(lotno, status, base.UserId, base.currentUser.UserName, ServerVariables.TrueIP);
            if (num == 0)
            {
                str = "操作成功";
            }
            else if (num == 1)
            {
                str = "不存在此单";
            }
            else if (num == 2)
            {
                str = "此单已处理，不可重复操作";
            }
            else if (num == 3)
            {
                str = "输入状态不正确";
            }
            else if (num == 4)
            {
                str = "系统出错";
            }
            else
            {
                str = "未知错误";
            }
            if ((status == 2) && (num == 0))
            {
                if ((this.scheme.tranRequiredAudit != 0) || (this.scheme.vaiInterface != 1))
                {
                    return str;
                }
                List<viviapi.Model.Withdraw.settledAgent> modelList = this.stlAgtBLL.GetModelList("lotno=" + lotno);
                foreach (viviapi.Model.Withdraw.settledAgent agent in modelList)
                {
                    if (!((agent.suppid <= 0) || agent.is_cancel))
                    {
                        Withdraw.InitDistribution2(agent);
                    }
                }
            }
            return str;
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
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("userid", base.UserId));
            DateTime minValue = DateTime.MinValue;
            if ((!string.IsNullOrEmpty(this.sdate.Value.Trim()) && DateTime.TryParse(this.sdate.Value.Trim(), out minValue)) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("starttime", minValue));
            }
            if ((!string.IsNullOrEmpty(this.edate.Value.Trim()) && DateTime.TryParse(this.edate.Value.Trim(), out minValue)) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("endtime", minValue.AddDays(1.0)));
            }
            DataSet set = this._bll.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty, false);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptrecharges.DataSource = set.Tables[1];
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
                Button button = e.Item.FindControl("btnSure") as Button;
                Button button2 = e.Item.FindControl("btnCancel") as Button;
                button.Visible = true;
                button2.Visible = true;
            }
            if ((e.Item.ItemType == ListItemType.Footer) && (this.rptrecharges.Items.Count == 0))
            {
                Literal literal = (Literal)e.Item.FindControl("litfoot");
                literal.Text = " <tfoot>\r\n                        <tr>\r\n                            <td colspan=\"10\" class=\"nomsg\">\r\n                                －_－^..暂无记录\r\n                            </td>\r\n                        </tr>\r\n                     </tfoot>     ";
            }
        }

        protected void rptrecharges_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                string msg = string.Empty;
                string lotno = e.CommandArgument.ToString();
                if (e.CommandName == "sure")
                {
                    msg = this.Affirm(lotno, 2);
                }
                else if (e.CommandName == "cancel")
                {
                    msg = this.Affirm(lotno, 3);
                }
                base.AlertAndRedirect(msg, "importlist.aspx");
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

