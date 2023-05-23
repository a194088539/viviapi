namespace viviapi.WebUI.Userlogin.product
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using Wuqi.Webdiyer;

    public class banktype : UserPageBase
    {
        protected HtmlForm form1;
        protected AspNetPager Pager1;
        protected Repeater rptcardtypes;

        private void LoadData()
        {
            List<SearchParam> list = new List<SearchParam>();
            int typeid = 0;
            int pageSize = this.Pager1.PageSize;
            DataSet set = viviapi.BLL.Channel.Channel.GetBankChanels(this.Pager1.CurrentPageIndex, pageSize, base.UserId, typeid, 0, -1);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptcardtypes.DataSource = set.Tables[1];
            this.rptcardtypes.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.LoadData();
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }
    }
}

