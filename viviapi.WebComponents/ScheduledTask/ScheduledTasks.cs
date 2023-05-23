using System;
using System.Collections.Generic;
using viviLib.ExceptionHandling;
using viviLib.ScheduledTask;

namespace viviapi.WebComponents.ScheduledTask
{
    public class ScheduledTasks
    {
        public static bool TaskExecuting = false;
        private viviLib.ScheduledTask.ScheduledTask[] _scheduledTasks;

        public void Start()
        {
            List<ScheduledTaskConfiguration> configs = ScheduledTaskConfigurationSectionHandler.GetConfigs();
            if (configs == null)
                return;
            this._scheduledTasks = new viviLib.ScheduledTask.ScheduledTask[configs.Count];
            for (int index = 0; index < configs.Count; ++index)
            {
                try
                {
                    viviLib.ScheduledTask.ScheduledTask scheduledTask = Activator.CreateInstance(Type.GetType(configs[index].ScheduledTaskType)) as viviLib.ScheduledTask.ScheduledTask;
                    if (scheduledTask != null)
                    {
                        scheduledTask.Execute(configs[index]);
                        this._scheduledTasks[index] = scheduledTask;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                }
            }
        }

        public void Stop()
        {
            if (this._scheduledTasks == null)
                return;
            for (int index = 0; index < this._scheduledTasks.Length; ++index)
            {
                if (this._scheduledTasks[index] != null)
                    this._scheduledTasks[index].Stop();
            }
        }
    }
}
