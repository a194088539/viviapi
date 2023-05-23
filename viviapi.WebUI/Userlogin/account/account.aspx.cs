namespace viviapi.WebUI.Userlogin.account
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Order;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using Wuqi.Webdiyer;

    public class account : UserPageBase
    {
        protected string Channeltypes = "";
        protected string earnings = "0";
        protected HtmlForm form1;
        public string getemail = "";
        public string getemailbtn = "";
        public string getlastip = "";
        public string getlastm = "";
        public string getmoney = "";
        public string getnid = "";
        public string getnm = "";
        protected Literal litbalance;
        protected AspNetPager Pager1;
        protected AspNetPager Pager2;
        protected Repeater rporderbank;
        protected Repeater rpordercard;
        protected string Thenumber = "0";
        protected string totalordertotal = "0";
        protected string totalordertotal1 = "0";
        protected string totalordertotal2 = "0";
        protected string totalrealvalue = "0";
        protected string totalrealvalue1 = "0";
        protected string totalrealvalue2 = "0";
        protected string totalrefervalue = "0";
        protected string totalrefervalue1 = "0";
        protected string totalrefervalue2 = "0";
        protected string totalsuccordertotal = "0";
        protected string totalsuccordertotal1 = "0";
        protected string totalsuccordertotal2 = "0";
        protected string Transactionamount = "0";

        public string builderdate(string date, string hour, string m, string s)
        {
            return string.Format("{0} {1}:{2}:{3}", new object[] { date, hour, m, s });
        }

        private void InitForm()
        {
            this.getemail = base.UserViewEmail;
            this.getnid = base.currentUser.ID.ToString();
            this.getnm = base.currentUser.UserName;
            this.getlastm = base.currentUser.LastLoginTime.ToString();
            this.getlastip = base.currentUser.LastLoginIp.ToString();
            this.getmoney = base.currentUser.Balance.ToString("f2");
            if (base.currentUser.IsEmailPass == 1)
            {
                this.getemailbtn = "<a href='#' class='font12 weight-n'/>[已认证]";
            }
            else
            {
                this.getemailbtn = "<a href='/Userlogin/safety/modiemail.aspx' class'font12 weight-n'/>[未认证]";
            }
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("userid", base.UserId));
            DateTime minValue = DateTime.MinValue;
            if (DateTime.TryParse(this.builderdate(DateTime.Now.ToString("yyyy-MM-dd"), "00", "00", "00"), out minValue) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("stime", minValue.ToString()));
            }
            if (DateTime.TryParse(this.builderdate(DateTime.Now.ToString("yyyy-MM-dd"), "23", "59", "59"), out minValue) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("etime", minValue.ToString()));
            }
            string orderby = string.Empty;
            OrderBank bank = new OrderBank();
            DataTable table = bank.PageSearch(searchParams, 20, 1, orderby).Tables[2];
            if ((table != null) && (table.Rows.Count > 0))
            {
                try
                {
                    this.totalrefervalue1 = Convert.ToDecimal(table.Rows[0]["refervalue"]).ToString("f0");
                    this.totalrealvalue1 = Convert.ToDecimal(table.Rows[0]["realvalue"]).ToString("f0");
                    this.totalordertotal1 = Convert.ToDecimal(table.Rows[0]["ordtotal"]).ToString("f0");
                    this.totalsuccordertotal1 = Convert.ToDecimal(table.Rows[0]["succordtotal"]).ToString("f0");
                }
                catch
                {
                }
            }
            searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("userid", base.UserId));
            minValue = DateTime.MinValue;
            if (DateTime.TryParse(this.builderdate(DateTime.Now.ToString("yyyy-MM-dd"), "00", "00", "00"), out minValue) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("stime", minValue.ToString()));
            }
            if (DateTime.TryParse(this.builderdate(DateTime.Now.ToString("yyyy-MM-dd"), "23", "59", "59"), out minValue) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("etime", minValue.ToString()));
            }
            orderby = string.Empty;
            OrderCard card = new OrderCard();
            DataTable table2 = card.PageSearch(searchParams, 20, 1, orderby).Tables[2];
            if ((table2 != null) && (table2.Rows.Count > 0))
            {
                try
                {
                    string begin = DateTime.Now.ToString("yyyy-MM-dd");
                    string edate = DateTime.Now.AddDays(1.0).ToString("yyyy-MM-dd");
                    this.Channeltypes = Dal.GetUserOrderCount(base.UserId, begin, edate).ToString();
                    this.totalrefervalue2 = Convert.ToDecimal(table2.Rows[0]["refervalue"]).ToString("f0");
                    this.totalrealvalue2 = Convert.ToDecimal(table2.Rows[0]["realvalue"]).ToString("f0");
                    this.totalordertotal2 = Convert.ToDecimal(table2.Rows[0]["ordtotal"]).ToString("f0");
                    this.totalsuccordertotal2 = Convert.ToDecimal(table2.Rows[0]["succordtotal"]).ToString("f0");
                }
                catch
                {
                }
            }
            this.totalordertotal = (Convert.ToDouble(this.totalordertotal1) + Convert.ToDouble(this.totalordertotal2)).ToString();
            this.totalsuccordertotal = (Convert.ToDouble(this.totalsuccordertotal1) + Convert.ToDouble(this.totalsuccordertotal2)).ToString();
            this.totalrefervalue = (Convert.ToDouble(this.totalrefervalue1) + Convert.ToDouble(this.totalrefervalue2)).ToString();
            this.totalrealvalue = (Convert.ToDouble(this.totalrealvalue1) + Convert.ToDouble(this.totalrealvalue2)).ToString();
        }

        public void NewOrderBank()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("userid", base.UserId));
            searchParams.Add(new SearchParam("status", 2));
            DateTime minValue = DateTime.MinValue;
            if (DateTime.TryParse(this.builderdate(DateTime.Now.ToString("yyyy-MM-dd"), "00", "00", "00"), out minValue) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("stime", minValue.ToString()));
            }
            if (DateTime.TryParse(this.builderdate(DateTime.Now.ToString("yyyy-MM-dd"), "23", "59", "59"), out minValue) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("etime", minValue.ToString()));
            }
            string orderby = string.Empty;
            DataSet set = new OrderBank().PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rporderbank.DataSource = set.Tables[1];
            this.rporderbank.DataBind();
        }

        public void NewOrderCard()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("userid", base.UserId));
            searchParams.Add(new SearchParam("status", 2));
            DateTime minValue = DateTime.MinValue;
            if (DateTime.TryParse(this.builderdate(DateTime.Now.ToString("yyyy-MM-dd"), "00", "00", "00"), out minValue) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("stime", minValue.ToString()));
            }
            if (DateTime.TryParse(this.builderdate(DateTime.Now.ToString("yyyy-MM-dd"), "23", "59", "59"), out minValue) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("etime", minValue.ToString()));
            }
            string orderby = string.Empty;
            DataSet set = new OrderCard().PageSearch(searchParams, this.Pager2.PageSize, this.Pager2.CurrentPageIndex, orderby);
            this.Pager2.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rpordercard.DataSource = set.Tables[1];
            this.rpordercard.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitForm();
                this.NewOrderBank();
                this.NewOrderCard();
                this.litbalance.Text = ((base.balance - base.unpayment) - base.Freeze).ToString("f2");
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.NewOrderBank();
        }

        protected void Pager2_PageChanged(object sender, EventArgs e)
        {
            this.NewOrderCard();
        }
    }
}

