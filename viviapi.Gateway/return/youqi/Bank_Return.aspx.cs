using System;
using System.Web.UI;
using viviapi.ETAPI.Youqi;

namespace viviapi.gateway.ret.youqi
{
    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}