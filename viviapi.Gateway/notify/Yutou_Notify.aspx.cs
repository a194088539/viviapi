using System;
using System.Web.UI;
using viviapi.ETAPI.Yutou;

namespace viviapi.gateway.notify
{
    public class Yutou_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}