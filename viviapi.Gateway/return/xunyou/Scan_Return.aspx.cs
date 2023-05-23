using System;
using System.Web.UI;
using viviapi.ETAPI.xunyou;

namespace viviapi.Gateway.ret.xunyou
{
    public class Scan_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Scan().ReturnScan();
        }
    }
}