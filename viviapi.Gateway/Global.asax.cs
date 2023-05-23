namespace viviapi.gateway
{
    using System;
    using System.Web;
    using viviapi.WebComponents.ScheduledTask;
    using viviLib.ExceptionHandling;
    using viviLib.Web;

    public class Global : HttpApplication
    {
        private ScheduledTasks scheduledTasks;

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (this.scheduledTasks != null)
            {
                this.scheduledTasks.Stop();
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            ExceptionHandler.HandleException(base.Server.GetLastError());
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            WebBase.HttpApplication = this;
            this.scheduledTasks = new ScheduledTasks();
            this.scheduledTasks.Start();
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }
    }
}

