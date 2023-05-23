using System;
using System.Web.UI;
using viviapi.ETAPI._51upay;

namespace viviapi.gateway.ret._51upay
{
    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}

