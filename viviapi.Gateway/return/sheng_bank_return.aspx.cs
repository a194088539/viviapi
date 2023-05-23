using System;
using System.Web.UI;
using viviapi.ETAPI.Shengpay;

namespace viviapi.gateway.Return
{
    public class sheng_bank_return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify(false);
        }
    }
}
