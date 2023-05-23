namespace viviapi.WebUI.Userlogin.order
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.User;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class smartexcha : UserPageBase
    {
        private usersettingInfo _usersetting = null;
        public string defaultvalue = string.Empty;
        protected HtmlGenericControl facevaluelist;
        protected HtmlGenericControl facevaluelist_1;
        protected HtmlForm form1;
        protected HtmlInputHidden g_channelId;
        protected HtmlHead Head1;
        protected string ordtotal = "0";
        protected AspNetPager Pager1;
        protected string pagerealvalue = "0";
        protected string pagerefervalue = "0";
        protected Repeater rptorders;
        protected string succordtotal = "0";
        protected string totalpayAmt = "0";
        protected string totalrealvalue = "0";
        protected string totalrefervalue = "0";
        private viviapi.BLL.User.usersetting usersetDAL = new viviapi.BLL.User.usersetting();
        protected HtmlInputHidden xk_channelId;
        protected HtmlInputHidden xk_channelname;

        public string builderdate(string date, string hour, string m, string s)
        {
            return string.Format("{0} {1}:{2}:{3}", new object[] { date, hour, m, s });
        }

        private void InitForm()
        {
            string str = "<div class=\"x_radio\"><input type=\"radio\" name=\"FaceValue\" value=\"5\" class=\"i_radio\" /><label>5元</label></div><div class=\"x_radio\"><input type=\"radio\" name=\"FaceValue\" value=\"10\" class=\"i_radio\" /><label>10元</label></div><div class=\"x_radio\"><input type=\"radio\" name=\"FaceValue\" value=\"15\" class=\"i_radio\" /><label>15元</label></div><div class=\"x_radio\"><input type=\"radio\" name=\"FaceValue\" value=\"20\" class=\"i_radio\" /><label>20元</label></div><div class=\"x_radio\"><input type=\"radio\" name=\"FaceValue\" value=\"30\" class=\"i_radio\" /><label>30元</label></div><div class=\"x_radio\"><input type=\"radio\" name=\"FaceValue\" value=\"50\" class=\"i_radio\" /><label>50元</label></div><div class=\"x_radio\"><input type=\"radio\" checked=\"checked\" name=\"FaceValue\" value=\"100\" class=\"i_radio\" /><label>100元</label></div><div class=\"x_radio\"><input type=\"radio\" name=\"FaceValue\" value=\"200\" class=\"i_radio\" /><label>200元</label></div><div class=\"x_radio\"><input type=\"radio\" name=\"FaceValue\" value=\"300\" class=\"i_radio\" /><label>300元</label></div><div class=\"x_radio\"><input type=\"radio\" name=\"FaceValue\" value=\"500\" class=\"i_radio\" /><label>500元</label></div><div class=\"x_radio\"><input type=\"radio\" name=\"FaceValue\" value=\"1000\" class=\"i_radio\" /><label>1000元</label></div>";
            this.defaultvalue = "100";
            this.facevaluelist.InnerHtml = str;
            this.facevaluelist_1.InnerHtml = str;
        }

        private void LoadData()
        {
            string orderby = string.Empty;
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("userid", UserFactory.CurrentMember.ID));
            DataSet set = new OrderCard().PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            DataTable table = set.Tables[1];
            this.rptorders.DataSource = table;
            this.rptorders.DataBind();
            DataTable table2 = set.Tables[2];
            if ((table2 != null) && (table2.Rows.Count > 0))
            {
                try
                {
                    this.totalrefervalue = Convert.ToDecimal(table2.Rows[0]["refervalue"]).ToString("f0");
                    this.totalrealvalue = Convert.ToDecimal(table2.Rows[0]["realvalue"]).ToString("f0");
                    this.ordtotal = Convert.ToDecimal(table2.Rows[0]["ordtotal"]).ToString("f0");
                    this.succordtotal = Convert.ToDecimal(table2.Rows[0]["succordtotal"]).ToString("f0");
                    this.totalpayAmt = Convert.ToDecimal(table2.Rows[0]["payAmt"]).ToString("f2");
                }
                catch
                {
                }
            }
            table2 = set.Tables[1];
            try
            {
                this.pagerefervalue = Convert.ToDecimal(table2.Compute("sum(refervalue)", "")).ToString("f0");
                this.pagerealvalue = Convert.ToDecimal(table2.Compute("sum(realvalue)", "")).ToString("f0");
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
            }
            this.LoadData();
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void rptorders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Literal literal = e.Item.FindControl("litfooter") as Literal;
                if ((literal != null) && (this.rptorders.Items.Count == 0))
                {
                    literal.Text = "          <tr>\r\n\t\t        <td colspan=\"7\" class=\"nomsg\">－_－^..暂无订单记录</td>\r\n          </tr> ";
                }
            }
        }

        private void setCtrl(int _paytype, bool _visible, string _style)
        {
        }

        public int cid
        {
            get
            {
                return WebBase.GetQueryStringInt32("cid", this.usersetting.defaultpay);
            }
        }

        public bool isdefaultPay
        {
            get
            {
                return (this.cid == this.usersetting.defaultpay);
            }
        }

        public usersettingInfo usersetting
        {
            get
            {
                if (this._usersetting == null)
                {
                    this._usersetting = this.usersetDAL.GetModel(base.UserId);
                }
                return this._usersetting;
            }
        }

        public string writein
        {
            get
            {
                return WebBase.GetQueryStringString("writein", string.Empty);
            }
        }
    }
}

