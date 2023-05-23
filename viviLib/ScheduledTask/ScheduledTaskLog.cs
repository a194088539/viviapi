using System;
using viviLib.Configuration;
using viviLib.ExceptionHandling;
using viviLib.Logging;
using viviLib.TimeControl;

namespace viviLib.ScheduledTask
{
    public sealed class ScheduledTaskLog
    {
        private ScheduledTaskLog()
        {
        }

        public static void WriteExecuteLog(Type type, DateTime startTime, DateTime endTime)
        {
            if (!LogSetting.ScheduledTaskLogEnabled)
                return;
            try
            {
                LogHelper.Write(LogSetting.ScheduleTaskExecuteLogFilePath(DateTime.Today), string.Format("{0},{1:yyyy-MM-dd HH:mm:ss.fff},{2:yyyy-MM-dd HH:mm:ss.fff}", (object)type.FullName, (object)startTime, (object)endTime), false);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public static void WriteLog(ScheduledTaskConfiguration config)
        {
            if (config == null || !LogSetting.ScheduledTaskLogEnabled)
                return;
            try
            {
                string str = string.Format("Task\t\t\t\t= {0}\r\nTime              = {1}", (object)config.ScheduledTaskType, (object)FormatConvertor.DateTimeToTimeString(DateTime.Now, true));
                LogHelper.Write(LogSetting.ScheduleTaskLogFilePath(DateTime.Today, config), str);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
    }
}
