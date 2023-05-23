namespace viviapi.WebUI.Userlogin.settlement
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using Wuqi.Webdiyer;

    public class costlog : UserPageBase
    {
        protected Button b_search;
        protected HtmlForm form1;
        protected HtmlSelect fState;
        protected AspNetPager Pager1;
        protected Repeater rptDetails;

        protected void b_search_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void InitForm()
        {
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("userid", base.UserId));
            if (this.fState.Value != "-1")
            {
                searchParams.Add(new SearchParam("status", int.Parse(this.fState.Value)));
            }
            string orderby = string.Empty;
            DataSet set = SettledFactory.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptDetails.DataSource = set.Tables[1];
            this.rptDetails.DataBind();
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
        }
    }
}

