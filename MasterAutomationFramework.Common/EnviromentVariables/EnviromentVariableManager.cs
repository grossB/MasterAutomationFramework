using System;

namespace MasterAutomationFramework.Common.EnviromentVariablesReader
{
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

        /// <summary>
        /// Create or override existing environment variable, Default Target = Machine.
        /// </summary>
        /// <param name="variabelName">Variable name</param>
        /// <param name="variableValue">Variable value</param>
        /// <param name="enviromentTarget">Targeted scope value, default <[EnvironmentVariableTarget.Machine]> </param>
        public static void SetEnviromentVariable(string variabelName, string variableValue, EnvironmentVariableTarget enviromentTarget = EnvironmentVariableTarget.Machine)
        {
            Environment.SetEnvironmentVariable(variabelName, variableValue, enviromentTarget);
        }

        /// <summary>
        /// Gets existing environment variable, Default Target = Machine.
        /// </summary>
        /// <param name="variabelName">Variable name</param>
        /// <param name="variableValue">Variable value</param>
        /// <param name="enviromentTarget">Targeted scope value, default <[EnvironmentVariableTarget.Machine]> </param>
        public static string GetEnviromentVariable(string variabelName, EnvironmentVariableTarget enviromentTarget = EnvironmentVariableTarget.Machine)
        {
            return Environment.GetEnvironmentVariable(variabelName, enviromentTarget);
        }
    }
}
