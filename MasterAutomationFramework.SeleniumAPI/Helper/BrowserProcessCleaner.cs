
namespace MasterAutomationFramework.SeleniumAPI.Helper
{
    using System.Diagnostics;
    using System;
    using MasterAutomationFramework.SeleniumAPI.Extension;

    public class BrowserProcessCleaner
    {
        //private SeriLogger logger;

        //public BrowserProcessCleaner(SeriLogger _logger)
        //{
        //    this.logger = _logger;
        //}

        public void CleanBrowserProcesses()
        {
            //this.logger?.Debug("Starting browsers clean up...");

            var processesByName = Process.GetProcessesByName("chromedriver");

            //this.logger?.Debug($"[{processesByName.Length}] Chrome Driver processes found.");

            foreach (var process in processesByName)
            {
                var parent = this.GetParentProcess(process);
                var current = Process.GetCurrentProcess();

                if (parent == null || parent.Id == current.Id)
                {
                    //this.logger?.Debug($"Terminating Chrome Driver process with PID [{process.Id}]....");
                    ProcessExtensions.KillProcessTree(process.Id, ProcessExtensions.GetProcessTree(process.Id));
                    //this.logger?.Debug("Done.");
                }
                else
                {
                    //this.logger?.Debug($"Chrome Driver process (name: [{process.ProcessName}], PID: [{process.Id}]) is probably used by other tests execution. Process will not be terminated.");
                }
            }
        }

        /// <summary>
        /// Get parent process.
        /// </summary>
        /// <param name="process">Process to check parent for.</param>
        /// <returns>The <see cref="Process"/>.</returns>
        private Process GetParentProcess(Process process)
        {
            try
            {
                return process.ParentProcess();
            }
            catch (InvalidOperationException exception)
            {
                //Debug("Probably Chrome Driver was closed while getting parent process.");
                return null;
            }
        }
    }
}
