using System;
using viviLib.ScheduledTask;
using viviLib.TimeControl;

namespace viviLib.Configuration
{
    public sealed class LogSetting
    {
        internal static readonly string _group = "logSettings";

        public static bool ExceptionLogEnabled
        {
            get
            {
                string config = ConfigHelper.GetConfig(LogSetting.SettingGroup, "ExceptionLogEnabled");
                return config != null && string.Compare(config, bool.TrueString, true) == 0;
            }
        }

        public static bool ScheduledTaskLogEnabled
        {
            get
            {
                string config = ConfigHelper.GetConfig(LogSetting.SettingGroup, "ScheduledTaskLogEnabled");
                return config != null && string.Compare(config, bool.TrueString, true) == 0;
            }
        }

        public static string SettingGroup
        {
            get
            {
                return LogSetting._group;
            }
        }

        public static bool SMSLogEnabled
        {
            get
            {
                return true;
            }
        }

        private LogSetting()
        {
        }

        public static string ExceptionLogFilePath(DateTime date)
        {
            return AppDomain.CurrentDomain.BaseDirectory + "LogFiles/Exceptions/" + string.Format("{0:yyyy-MM-dd}", (object)date) + ".log";
        }

        public static string ScheduleTaskExecuteLogFilePath(DateTime date)
        {
            return AppDomain.CurrentDomain.BaseDirectory + "LogFiles/ScheduleTask/" + FormatConvertor.DateTimeToDateString(date, true) + "_execute.log";
        }

        public static string ScheduleTaskLogFilePath(DateTime date, ScheduledTaskConfiguration config)
        {
            return AppDomain.CurrentDomain.BaseDirectory + "LogFiles/ScheduleTask/" + FormatConvertor.DateTimeToDateString(date, true) + ".log";
        }
    }
}
