﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Essentials
{
    public static class DateTimeExtensions
    {
        public const string __DateDefaultFormatString = "yyMMddHHmmssfff";

        public static DateTime LocalTime => TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.Local);

        public static string Timestamp => GetTimestamp(__DateDefaultFormatString);

        public static string GetTimestamp(string format) => LocalTime.ToString(format);

        public static string GetTotalDurationAsString(DateTime start, DateTime end)
        {
            TimeSpan duration = end - start;
            StringBuilder res = new StringBuilder();
            if (duration.Hours > 0)
            {
                res.Append(duration.Hours.ToString() + "h ");
            }
            if (duration.Hours > 0 || duration.Minutes > 0)
            { 
                res.Append(duration.Minutes.ToString() + "m ");
            }
            res.Append(duration.Seconds.ToString() + "s");

            return res.ToString();
        }

        public static TimeSpan GetTotalDurationAsTimeSpan(DateTime start, DateTime end) => end - start;
    }
}
