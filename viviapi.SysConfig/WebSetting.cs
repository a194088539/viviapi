using System.Collections.Generic;
using System.Configuration;
using viviLib.ScheduledTask;

namespace viviapi.SysConfig
{
    public class WebSetting
    {
        public static List<ScheduledTask> ScheduledTaskSettings
        {
            get
            {
                if (ConfigurationManager.GetSection("officeKStar/scheduledTaskSettings") != null)
                    return ConfigurationManager.GetSection("officeKStar/scheduledTaskSettings") as List<ScheduledTask>;
                return new List<ScheduledTask>();
            }
        }
    }
}
