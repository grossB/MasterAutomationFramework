using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MasterAutomationFramework.Common.Extension
{
    public static class ProcessExtension
    {
        /// <summary>
        /// Returns the title of the main window of the specified process as it is now
        /// </summary>
        /// <param name="process">The process to look for its main window title</param>
        /// <returns>The title of the main window</returns>
        /// <remarks>
        /// Unlike <see cref="Process.MainWindowTitle"/>, this method always return the up-to-date title of the main window, even if it has changed after the <see cref="Process"/> has been started
        /// </remarks>
        public static string GetCurrentMainWindowTitle(this Process process)
        {
            process.Refresh();
            return process.MainWindowTitle;
        }
    }
}
