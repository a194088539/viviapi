namespace viviapi.WebUI.Userlogin.Ajax
{
    using System.Web;
    using System.Web.SessionState;
    using viviapi.BLL.Payment;
    using viviapi.BLL.User;
    using viviapi.Model.User;
    using viviLib.Web;

    public class GetactualMoney_new : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string s = "0.00";
            if (((this.currentUser != null) && (this.rechargemoney > 0M)) && (this.bankcode > 0))
            {
                int typeId = 0x66;
                if (this.bankcode == 0x3e0)
                {
                    typeId = 0x65;
                }
                else if (this.bankcode == 0x3e1)
                {
                    typeId = 100;
                }
                decimal userPayRate = PayRateFactory.GetUserPayRate(this.currentUser.ID, typeId);
                s = decimal.Round(this.rechargemoney * userPayRate, 2).ToString();
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(s);
        }

        public int bankcode
        {
            get
            {
                return WebBase.GetQueryStringInt32("bank", 0);
            }
        }

        public UserInfo currentUser
        {
            get
            {
                return UserFactory.CurrentMember;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public decimal rechargemoney
        {
            get
            {
                return WebBase.GetQueryStringDecimal("rechargemoney", 0M);
            }
        }
    }
}

