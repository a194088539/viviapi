namespace viviapi.WebUI.Userlogin.account
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using Wuqi.Webdiyer;

    public class feedbacks : UserPageBase
    {
        protected HtmlForm form1;
        protected AspNetPager Pager1;
        protected Repeater rptfeedback;

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
            DataSet set = new viviapi.BLL.feedback().PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptfeedback.DataSource = set.Tables[1];
            this.rptfeedback.DataBind();
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

        protected void rptfeedback_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Literal literal = (Literal)e.Item.FindControl("litdetail");
                switch (DataBinder.Eval(e.Item.DataItem, "status").ToString())
                {
                    case "1":
                        literal.Text = "等待回复";
                        break;

                    case "2":
                        literal.Text = "已回复";
                        break;
                }
            }
        }
    }
}

