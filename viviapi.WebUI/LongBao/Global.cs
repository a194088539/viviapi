using System;
using System.Web;
using viviapi.WebComponents.ScheduledTask;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviapi.WebUI.LongBao
{
    public class Global : HttpApplication
    {
        private ScheduledTasks scheduledTasks;

        protected void Application_Start(object sender, EventArgs e)
        {
            WebBase.HttpApplication = (HttpApplication)this;
            this.scheduledTasks = new ScheduledTasks();
            this.scheduledTasks.Start();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            ExceptionHandler.HandleException(this.Server.GetLastError());
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}
