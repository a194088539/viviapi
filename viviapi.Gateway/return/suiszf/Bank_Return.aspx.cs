using System;
using System.Web.UI;
using viviapi.ETAPI.suiszf;

namespace viviapi.gateway.ret.suiszf
{
    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().BankReturn();
        }
    }
}
