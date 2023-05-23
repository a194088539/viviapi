namespace viviapi.WebUI.Userlogin.account
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

    public class messages : UserPageBase
    {
        protected Button b_search;
        protected Button btnRpt;
        protected HtmlInputText edate;
        protected HtmlForm form1;
        protected Repeater msg_data;
        protected AspNetPager Pager1;
        protected HtmlInputText sdate;

        protected void b_search_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void btnRpt_Click(object sender, EventArgs e)
        {
            string iDlist = "";
            for (int i = 0; i < this.msg_data.Items.Count; i++)
            {
                CheckBox box = (CheckBox)this.msg_data.Items[i].FindControl("ckbIndex");
                if (box.Checked)
                {
                    Label label = (Label)this.msg_data.Items[i].FindControl("lbmsid");
                    iDlist = iDlist + label.Text + ",";
                }
            }
            if ((iDlist.Length > 0) && IMSGFactory.DeleteList(iDlist))
            {
                base.AlertAndRedirect("成功删除");
            }
            this.LoadData();
        }

        public string builderdate(string date, string hour, string m, string s)
        {
            return string.Format("{0} {1}:{2}:{3}", new object[] { date, hour, m, s });
        }

        private void InitForm()
        {
            this.sdate.Value = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
            this.edate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("msg_to", base.UserId));
            DateTime minValue = DateTime.MinValue;
            if (DateTime.TryParse(this.builderdate(this.sdate.Value, "00", "00", "00"), out minValue) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("stime", minValue.ToString()));
            }
            if (DateTime.TryParse(this.builderdate(this.edate.Value, "23", "59", "59"), out minValue) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("etime", minValue.ToString()));
            }
            try
            {
                DataSet set = IMSGFactory.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
                this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
                this.msg_data.DataSource = set.Tables[1];
                this.msg_data.DataBind();
            }
            catch
            {
            }
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
    }
}

