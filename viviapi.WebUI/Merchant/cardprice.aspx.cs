namespace viviapi.WebUI.Merchant
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using Wuqi.Webdiyer;

    public class cardprice : UserPageBase
    {
        protected Button b_search;
        protected HtmlSelect fState;
        protected HtmlSelect fType;
        protected AspNetPager Pager1;
        protected Repeater rptcardtypes;
        protected HtmlInputText txtfacevalue;

        protected void b_search_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected string GetStautsName(object value)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return string.Empty;
            }
            if (value.ToString() == "1")
            {
                return "开启";
            }
            return "关闭";
        }

        protected string GetTogTypeCode(object code)
        {
            if ((code == null) || (code == DBNull.Value))
            {
                return string.Empty;
            }
            string str = code.ToString();
            if (str.Length <= 4)
            {
                return str;
            }
            return str.Substring(0, 4);
        }

        private void LoadData()
        {
            List<SearchParam> list = new List<SearchParam>();
            int typeid = 0;
            if (this.fType.Value != "0")
            {
                typeid = int.Parse(this.fType.Value);
            }
            int result = 0;
            if (string.IsNullOrEmpty(this.txtfacevalue.Value))
            {
                int.TryParse(this.txtfacevalue.Value.Trim(), out result);
            }
            int chanelstatus = -1;
            if (this.fState.Value != "-1")
            {
                chanelstatus = int.Parse(this.fState.Value);
            }
            int pageSize = this.Pager1.PageSize;
            DataSet set = viviapi.BLL.Channel.Channel.GetCardChanels(this.Pager1.CurrentPageIndex, pageSize, base.UserId, typeid, result, chanelstatus);
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

