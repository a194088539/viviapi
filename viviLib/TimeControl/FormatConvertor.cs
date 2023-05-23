using System;
using viviLib.ExceptionHandling;

namespace viviLib.TimeControl
{
    public sealed class FormatConvertor
    {
        public static readonly string DATE_FORMAT = "yyyy-MM-dd";
        public static readonly string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public static readonly string DATETIME_FORMAT_WITHOUT_SECOND = "yyyy-MM-dd HH:mm";
        public static readonly DateTime SqlDateTimeMinValue = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        public static readonly string TIME_HOUR_MINUTE_FORMAT = "hh:mm";
        public static readonly string YEARMONTH_FORMAT = "yyyy-MM";

        private FormatConvertor()
        {
        }

        public static string DateTimeToDateString(DateTime d)
        {
            return FormatConvertor.DateTimeToDateString(d, true);
        }

        public static string DateTimeToDateString(DateTime d, bool viewDay)
        {
            if (d == DateTime.MinValue)
                return string.Empty;
            if (viewDay)
                return string.Format("{0:" + FormatConvertor.DATE_FORMAT + "}", (object)d);
            return string.Format("{0:" + FormatConvertor.YEARMONTH_FORMAT + "}", (object)d);
        }

        public static string DateTimeToTimeString(DateTime d)
        {
            return FormatConvertor.DateTimeToTimeString(d, false);
        }

        public static string DateTimeToTimeString(DateTime d, bool viewSecond)
        {
            if (d == DateTime.MinValue)
                return string.Empty;
            if (!viewSecond)
                return string.Format("{0:" + FormatConvertor.DATETIME_FORMAT_WITHOUT_SECOND + "}", (object)d);
            return string.Format("{0:" + FormatConvertor.DATETIME_FORMAT + "}", (object)d);
        }

        public static string GetFormatedTime(string s)
        {
            if (s == null || s.Length == 0)
                return "00:00:00";
            string[] strArray = s.Trim().Split(':');
            try
            {
                switch (strArray.Length)
                {
                    case 1:
                        return string.Format("{0:00}:00:00", (object)Convert.ToInt32(strArray[0], 10));
                    case 2:
                        return string.Format("{0:00}:{1:00}:00", (object)Convert.ToInt32(strArray[0], 10), (object)Convert.ToInt32(strArray[1], 10));
                    case 3:
                        return string.Format("{0:00}:{1:00}:{2:00}", (object)Convert.ToInt32(strArray[0], 10), (object)Convert.ToInt32(strArray[1], 10), (object)Convert.ToInt32(strArray[2], 10));
                    default:
                        return "00:00:00";
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return "00:00:00";
            }
        }

        public static DateTime StringToDateTime(string s)
        {
            if (s == null || s.Length == 0)
                return DateTime.MinValue;
            string[] strArray = s.Trim().Split(new char[3]
            {
        '-',
        ' ',
        ':'
            });
            try
            {
                switch (strArray.Length)
                {
                    case 2:
                        return new DateTime(Convert.ToInt32(strArray[0], 10), Convert.ToInt32(strArray[1], 10), 1);
                    case 3:
                        return new DateTime(Convert.ToInt32(strArray[0], 10), Convert.ToInt32(strArray[1], 10), Convert.ToInt32(strArray[2], 10));
                    case 4:
                    case 5:
                    case 6:
                        return new DateTime(Convert.ToInt32(strArray[0], 10), Convert.ToInt32(strArray[1], 10), Convert.ToInt32(strArray[2], 10), Convert.ToInt32(strArray[3], 10), strArray.Length > 4 ? Convert.ToInt32(strArray[4], 10) : 0, strArray.Length > 5 ? Convert.ToInt32(strArray[5], 10) : 0);
                    default:
                        return DateTime.MinValue;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return DateTime.MinValue;
            }
        }

        public static TimeSpan StringToTimeSpan(string s)
        {
            if (s == null || s.Length == 0)
                return new TimeSpan(0L);
            try
            {
                return TimeSpan.Parse(s);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return new TimeSpan(0L);
            }
        }

        public static string TimsSpanToString(TimeSpan timeSpan)
        {
            return timeSpan.ToString();
        }
    }
}
