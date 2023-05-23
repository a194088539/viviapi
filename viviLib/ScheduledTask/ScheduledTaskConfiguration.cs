using System;
using System.Collections.Generic;

namespace viviLib.ScheduledTask
{
    [Serializable]
    public class ScheduledTaskConfiguration
    {
        private List<string> _excutes = new List<string>();
        private string _scheduleTaskType = string.Empty;
        private int _threadSleepSecond = 60;

        public List<string> Executes
        {
            get
            {
                return this._excutes;
            }
        }

        public string ScheduledTaskType
        {
            get
            {
                return this._scheduleTaskType;
            }
            set
            {
                this._scheduleTaskType = value;
            }
        }

        public int ThreadSleepSecond
        {
            get
            {
                return this._threadSleepSecond;
            }
            set
            {
                this._threadSleepSecond = value;
            }
        }
    }
}
