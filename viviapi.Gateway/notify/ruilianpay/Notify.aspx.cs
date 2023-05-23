using System;
using System.Web.UI;
using viviapi.ETAPI.ruilianpay;

namespace viviapi.Gateway.notify.ruilianpay
{
    public class Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}
