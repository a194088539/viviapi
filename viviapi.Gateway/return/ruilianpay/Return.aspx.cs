using System;
using System.Web.UI;
using viviapi.ETAPI.ruilianpay;

namespace viviapi.Gateway.ret.ruilianpay
{
    public class Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Return();
        }
    }
}