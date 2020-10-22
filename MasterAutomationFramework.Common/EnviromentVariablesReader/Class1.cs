using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterAutomationFramework.Common.EnviromentVariablesReader
{
    [ExcludeFromCodeCoverage]
    public class EnviromentVariableManager
    {
        /// <summary>
        /// Returns the absolute path of the desktop folder
        /// </summary>
        /// <returns>The absolute path of the desktop folder</returns>
        public static string GetDesktopFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        public static string GetDesktopFolder(string variabelName)
        {
            return Environment.GetEnvironmentVariable(variabelName);
        }
    }
}
