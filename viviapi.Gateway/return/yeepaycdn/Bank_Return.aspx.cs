using System;
using System.Web.UI;
using viviapi.ETAPI.yeepaycdn;

namespace viviapi.gateway.ret.yeepaycdn
{
    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}