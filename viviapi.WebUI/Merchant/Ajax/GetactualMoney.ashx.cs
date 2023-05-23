using System;
using System.Web;
using System.Web.SessionState;
using viviapi.BLL.Payment;
using viviapi.BLL.User;
using viviapi.Model.User;
using viviLib.Web;

namespace viviapi.WebUI.LongBao.merchant.Ajax
{
    public class GetactualMoney : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        public Decimal rechargemoney
        {
            get
            {
                return WebBase.GetQueryStringDecimal("rechargemoney", new Decimal(0));
            }
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

        public void ProcessRequest(HttpContext context)
        {
            string s = "0.00";
            if (this.currentUser != null && this.rechargemoney > new Decimal(0) && this.bankcode > 0)
            {
                int typeId = 102;
                if (this.bankcode == 992)
                    typeId = 101;
                else if (this.bankcode == 993)
                    typeId = 100;
                s = Decimal.Round(this.rechargemoney * PayRateFactory.GetUserPayRate(this.currentUser.ID, typeId), 2).ToString();
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(s);
        }
    }
}
