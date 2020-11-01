using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterAutomationFramework.Common.ConfigLoader.ProjectConfig
{
    /// <summary>
    /// Config File is loaded from assembly scope only.
    /// </summary>
    public static class AppConfigFields
    {
        public static string GetConfigFieldValue(string fieldName)
        {
            // Add Project Reference System.Configuration from Assemblies tab
            var fileName = System.Configuration.ConfigurationManager.AppSettings[fieldName];
            return fileName;
        }
    }
}
