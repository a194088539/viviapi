namespace viviapi.WebUI.agent
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Order;
    using viviapi.WebComponents.Web;
    using viviLib.TimeControl;

    public class Index : AgentPageBase
    {
        protected string balance = "0.00";
        protected HtmlForm form1;
        protected Literal litlinks;
        protected string loginip;
        protected string logintime;
        protected string monthcommission = "0.00";
        protected string monthtotalAmt = "0.00";
        protected HtmlGenericControl paysouid;
        protected string todaytotalAmt = "0.00";
        protected string username;
        protected string userscount;
        protected string yeartotalAmt = "0.00";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                try
                {
                    this.litlinks.Text = "?agent=" + base.currentUser.ID.ToString();
                    this.userscount = PromotionUserFactory.GetUserNum(base.UserId).ToString();
                    DateTime today = DateTime.Today;
                    DateTime edt = DateTime.Today.AddDays(1.0);
                    decimal num2 = Dal.GetAgentTotalAmt(base.UserId, today, edt);
                    this.todaytotalAmt = num2.ToString("f2");
                    today = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00"));
                    num2 = Dal.GetAgentTotalAmt(base.UserId, today, edt);
                    decimal num = Dal.GetAgentIncome(base.UserId, today, edt);
                    this.monthtotalAmt = num2.ToString("f2");
                    this.monthcommission = num.ToString("f2");
                    today = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01 00:00:00"));
                    this.yeartotalAmt = Dal.GetAgentTotalAmt(base.UserId, today, edt).ToString("f2");
                    try
                    {
                        this.balance = ((base.currentUserAmt.balance.Value - base.currentUserAmt.Freeze.Value) - base.currentUserAmt.unpayment.Value).ToString("f2");
                    }
                    catch
                    {
                    }
                    this.loginip = base.currentUser.LastLoginIp;
                    this.logintime = FormatConvertor.DateTimeToTimeString(base.currentUser.LastLoginTime);
                    this.username = base.currentUser.UserName;
                }
                catch
                {
                }
            }
        }
    }
}

