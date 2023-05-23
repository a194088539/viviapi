namespace viviapi.WebUI.Managements
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
    using Wuqi.Webdiyer;

    public class NewsList : ManagePageBase
    {
        protected Button btnPublish;
        protected Button btnSearch;
        protected DropDownList ddl_type;
        protected HtmlForm form1;
        protected GridView GridView1;
        protected AspNetPager Pager1;
        protected TextBox txtTitle;

        protected void BindData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            if (!string.IsNullOrEmpty(this.ddl_type.SelectedValue))
            {
                searchParams.Add(new SearchParam("newstype", int.Parse(this.ddl_type.SelectedValue)));
            }
            string str = this.txtTitle.Text.Trim();
            if (!string.IsNullOrEmpty(str))
            {
                searchParams.Add(new SearchParam("newstitle", str));
            }
            DataSet set = NewsFactory.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.GridView1.DataSource = set.Tables[1];
            this.GridView1.DataBind();
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            string str = string.Format("{0}月{1}日提现已出款请商户查收", DateTime.Now.Month, DateTime.Now.Day);
            NewsInfo model = new NewsInfo();
            model.newstype = 2;
            model.newstitle = str;
            model.addTime = DateTime.Now;
            model.newscontent = str;
            model.IsRed = 0;
            model.IsTop = 0;
            model.IsPop = 0;
            model.Isbold = 0;
            model.Color = "";
            if (NewsFactory.Add(model) > 0)
            {
                base.AlertAndRedirect("发送成功!", "NewsList.aspx");
            }
            else
            {
                base.AlertAndRedirect("发送失败!", "NewsList.aspx");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int newsid = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "update")
            {
                base.Response.Redirect("NewsEdit.aspx?id=" + newsid.ToString(), true);
            }
            else if (e.CommandName == "del")
            {
                if (NewsFactory.Delete(newsid))
                {
                    base.AlertAndRedirect("删除成功");
                    this.BindData();
                }
                else
                {
                    base.AlertAndRedirect("删除失败");
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dataItem = (DataRowView)e.Row.DataItem;
                Label label = e.Row.FindControl("lblNewsType") as Label;
                if (label != null)
                {
                    label.Text = Enum.GetName(typeof(NewsType), dataItem["newstype"]);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                foreach (int num in Enum.GetValues(typeof(NewsType)))
                {
                    this.ddl_type.Items.Add(new ListItem(Enum.GetName(typeof(NewsType), num), num.ToString()));
                }
                this.BindData();
            }
        }

        protected void Pager1_PageChanging(object src, PageChangingEventArgs e)
        {
            this.BindData();
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.News))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }
    }
}

