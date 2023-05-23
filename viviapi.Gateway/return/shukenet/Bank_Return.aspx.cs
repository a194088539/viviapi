using System;
using System.Web.UI;
using viviapi.ETAPI.shukenet;

namespace viviapi.gateway.ret.shukenet
{
    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}
