using System;
using System.Web.UI;
using viviapi.ETAPI.BestPay;

namespace viviapi.gateway.notify.BestPay
{
    public class callback : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}
