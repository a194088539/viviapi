using System;
using System.Web.UI;
using viviapi.ETAPI.epay;

namespace viviapi.gateway.ret.epay
{
    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}