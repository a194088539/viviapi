using System;
using System.Web.UI;
using viviapi.ETAPI.Yt;

namespace viviapi.gateway.ret.yt
{
    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}