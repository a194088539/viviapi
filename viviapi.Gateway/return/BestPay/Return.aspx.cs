using System;
using System.Web.UI;
using viviapi.ETAPI.BestPay;

namespace viviapi.gateway.ret.BestPay
{
    public class Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}
