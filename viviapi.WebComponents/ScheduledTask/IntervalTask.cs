using System;
using System.Threading;
using viviLib.ExceptionHandling;
using viviLib.ScheduledTask;

namespace viviapi.WebComponents.ScheduledTask
{
    public class IntervalTask : viviLib.ScheduledTask.ScheduledTask
    {
        protected override void ScheduleCallback()
        {
            while (true)
            {
                while (ScheduledTasks.TaskExecuting)
                    Thread.Sleep(1000);
                try
                {
                    ScheduledTasks.TaskExecuting = true;
                    this.ExecuteTask();
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
                Thread.Sleep(this.Config.ThreadSleepSecond * 1000);
            }
        }
    }
}