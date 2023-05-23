using System;
using System.Web.UI;
using viviapi.ETAPI.tftpay;

namespace viviapi.gateway.Ret.tftpay
{
    public class tftpay_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new RMB().ReturnBank();
        }
    }
}
