using System;

namespace viviLib.ScheduledTask
{
    public abstract class ScheduledTaskExecuteBase : IScheduledTaskExecute
    {
        public void Execute()
        {
            DateTime now = DateTime.Now;
            this.ExecuteTask();
            ScheduledTaskLog.WriteExecuteLog(this.GetType(), now, DateTime.Now);
        }

        protected abstract void ExecuteTask();
    }
}
