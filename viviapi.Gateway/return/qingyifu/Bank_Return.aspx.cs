using System;
using System.Web.UI;
using viviapi.ETAPI.Qingyifu;

namespace viviapi.gateway.ret.qingyifu
{
    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}

