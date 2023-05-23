using System;
using System.Web.UI;
using viviapi.ETAPI.Shengpay;

namespace viviapi.gateway.notify
{
    public class sheng_bank_notity : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify(true);
        }
    }
}
