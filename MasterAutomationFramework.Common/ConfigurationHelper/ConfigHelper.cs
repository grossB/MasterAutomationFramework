using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterAutomationFramework.Common.ConfigurationHelper
{
    public class ConfigHelper
    {
        public const string RecordVideosPath = @"D:\SeleniumRecordedTest";

        public static bool RecordScenarioExecution => bool.Parse(GetSettings("RecordScenarioExecution"));

        public static bool HighlightElements => bool.Parse(GetSettings("HighlightElements"));

        public static string Environment => GetSettings("Environment");

        public static string Browser => GetSettings("Browser");

        public static bool CollectinJavaScriptLogs => bool.Parse(GetSettings("CollectinJavaScriptLogs"));

        private static string GetSettings(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}
