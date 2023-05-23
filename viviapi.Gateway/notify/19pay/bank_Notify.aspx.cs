using System;
using System.Web.UI;
using viviapi.ETAPI._19pay;

namespace viviapi.Gateway.notify._19pay
{
    public class bank_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}
