using System;
using System.Web.UI;
using viviapi.ETAPI.Mengma;
using viviLib.ExceptionHandling;

namespace viviapi.gateway.smsnotify
{
    public class mengma : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                new Sms().notify();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
    }
}
