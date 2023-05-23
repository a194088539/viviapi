using System;
using System.Threading;
using viviLib.ExceptionHandling;
using viviLib.ScheduledTask;

namespace viviapi.WebComponents.ScheduledTask
{
    public class DailyTask : viviLib.ScheduledTask.ScheduledTask
    {
        private DateTime lastExecuteTime = DateTime.MinValue;

        protected override void ScheduleCallback()
        {
            while (true)
            {
                if (this.lastExecuteTime.AddDays(1.0) < DateTime.Now)
                {
                    while (ScheduledTasks.TaskExecuting)
                        Thread.Sleep(1000);
                    try
                    {
                        ScheduledTasks.TaskExecuting = true;
                        this.ExecuteTask();
                        this.lastExecuteTime = DateTime.Today;
                        ScheduledTaskLog.WriteLog(this.Config);
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.HandleException(ex);
                    }
                    finally
                    {
                        ScheduledTasks.TaskExecuting = false;
                    }
                }
                Thread.Sleep(this.Config.ThreadSleepSecond * 1000);
            }
        }
    }
}
