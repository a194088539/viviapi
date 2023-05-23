using System;
using System.Web.UI;
using viviapi.ETAPI.haiou;

namespace viviapi.gateway.notify.haiou
{
    public class Bank_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}