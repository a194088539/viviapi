using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.WebComponents.Web;
using viviLib.Data;
using Wuqi.Webdiyer;

namespace viviapi.WebUI.Merchant
{
    public class messages : UserPageBase
    {
        protected Repeater msg_data;
        protected AspNetPager Pager1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
                return;
            this.InitForm();
            this.LoadData();
        }

        private void InitForm()
        {
        }

        private void LoadData()
        {
            DataSet dataSet = IMSGFactory.PageSearch(new List<SearchParam>()
      {
        new SearchParam("msg_to", (object) this.UserId)
      }, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
            this.msg_data.DataSource = (object)dataSet.Tables[1];
            this.msg_data.DataBind();
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }
    }
}
