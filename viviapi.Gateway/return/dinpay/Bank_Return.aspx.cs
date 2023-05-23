using System;
using System.Web.UI;
using viviapi.ETAPI.Dinpay;

namespace viviapi.gateway.ret.dinpay
{
    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}
