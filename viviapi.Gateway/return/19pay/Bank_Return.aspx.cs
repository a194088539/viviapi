using System;
using System.Web.UI;
using viviapi.ETAPI._19pay;

namespace viviapi.gateway.ret._19pay
{
    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Return();
        }
    }
}