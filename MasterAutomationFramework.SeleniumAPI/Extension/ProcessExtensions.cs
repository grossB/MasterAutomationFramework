namespace MasterAutomationFramework.SeleniumAPI.Extension
{
    using System;
    using System.Diagnostics;
    using System.Management;

    /// <summary>
    /// Process extensions.
    /// </summary>
    public static class ProcessExtensions
    {
        /// <summary>
        /// Get parent process by process.
        /// </summary>
        /// <param name="process">Process to find parent for.</param>
        /// <returns>The <see cref="Process"/>.</returns>
        public static Process ParentProcess(this Process process)
        {
            try
            {
                return FindPidByIndexedName(FindProcessIndexedNameByPid(process.Id));
            }
            catch (ArgumentException argumentException)
            {
                return null;
            }
            //catch (Exception exception)
            //{
            //    if (exception is ArgumentException && exception.Message.Contains("is not running"))
            //    {
            //        return null;
            //    }

            //    throw;
            //}
        }

        /// <summary>
        /// Find indexed process name.
        /// </summary>
        /// <param name="pid">Process ID.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string FindProcessIndexedNameByPid(int pid)
        {
            var processName = Process.GetProcessById(pid).ProcessName;
            var processesByName = Process.GetProcessesByName(processName);
            string indexedProcessName = null;

            for (var index = 0; index < processesByName.Length; index++)
            {
                indexedProcessName = index == 0 ? processName : processName + "#" + index;
                var processId = new PerformanceCounter("Process", "ID Process", indexedProcessName);
                if ((int)processId.NextValue() == pid)
                {
                    return indexedProcessName;
                }
            }

            return indexedProcessName;
        }

        /// <summary>
        /// Find PID by indexed name.
        /// </summary>
        /// <param name="indexedProcessName">Indexed process name.</param>
        /// <returns>The <see cref="Process"/>.</returns>
        private static Process FindPidByIndexedName(string indexedProcessName)
        {
            var parentId = new PerformanceCounter("Process", "Creating Process ID", indexedProcessName);
            return Process.GetProcessById((int)parentId.NextValue());
        }

        /// <summary>
        /// Get process tree.
        /// </summary>
        /// <param name="processId">
        /// Process that contains children processes to find.
        /// </param>
        /// <returns>
        /// Children processes.
        /// </returns>
        public static ManagementObjectCollection GetProcessTree(int processId)
        {
            var searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + processId);
            var managementObjectCollection = searcher.Get();

            return managementObjectCollection;
        }

        /// <summary>
        /// If process with given ID exist - kill it with all children processes.
        /// </summary>
        /// <param name="processId">
        /// Process ID to be killed with its' tree.
        /// </param>
        /// <param name="childrenProcessTree">
        /// Children process list.
        /// </param>
        public static void KillProcessTree(int processId, ManagementObjectCollection childrenProcessTree)
        {
            // Kill children of children process.
            foreach (var managementBaseObject in childrenProcessTree)
            {
                var managementObject = (ManagementObject)managementBaseObject;

                var children = GetProcessTree(Convert.ToInt32(managementObject["ProcessID"]));
                KillProcessTree(Convert.ToInt32(managementObject["ProcessID"]), children);
            }

            try
            {
                // Kill main process.
                var parentProcessToKill = Process.GetProcessById(processId);
                parentProcessToKill.Kill();
                parentProcessToKill.WaitForExit();
            }
            catch (ArgumentException)
            {
                Debug.Write("Argument exception thrown while trying to obtain process by ID. No action required.");
            }
            catch (SystemException)
            {
                Debug.Write("System exception thrown. No action required.");
            }
        }
    }
}

