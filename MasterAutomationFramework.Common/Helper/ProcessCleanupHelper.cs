
namespace MasterAutomationFramework.Common.Helper
{
    using System.Diagnostics;
    using System;
    using System.Management;

    public class ProcessCleanupHelper
    {
        public static void KillProcesses(string processName)
        {
            Console.WriteLine("Starting chromedriver clean up");
            var chromeDriverProcesses = Process.GetProcessesByName(processName);

            foreach (var chromeDriverProcess in chromeDriverProcesses)
            {
                try
                {
                    chromeDriverProcess.Kill();
                    Console.WriteLine("chromedriver process killed");
                }
                catch (Exception)
                {
                }
            }
        }

        public static void ForceKillAllChromeRelatedProcesses()
        {
            KillProcesses("chromedriver");
            KillProcesses("chrome");
        }

        public static void KillChromeDriverTreeProcessGracefully()
        {
            var processesByName = Process.GetProcessesByName("chromedriver");

            foreach (var process in processesByName)
            {
                var parent = GetParentProcess(process);
                var current = Process.GetCurrentProcess();

                if (parent == null || parent.Id == current.Id)
                {
                    Console.WriteLine($"Terminating Chrome Driver process with PID [{process.Id}]....");
                    KillProcessTree(process.Id, GetProcessTree(process.Id));
                    Console.WriteLine("Done.");
                }
                else
                {
                    Console.WriteLine(
                        $"Chrome Driver process (name: [{process.ProcessName}], PID: [{process.Id}]) is probably used by other tests execution. Process will not be terminated.");
                }
            }
        }

        private static Process GetParentProcess(Process process)
        {
            try
            {
                return ParentProcess(process);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Probably Chrome Driver was closed while getting parent process.");
                return null;
            }
        }

        /// <summary>
        /// Get parent process by process.
        /// </summary>
        /// <param name="process">Process to find parent for.</param>
        /// <returns>The <see cref="Process"/>.</returns>
        private static Process ParentProcess(Process process)
        {
            try
            {
                return FindPidByIndexedName(FindProcessIndexedNameByPid(process.Id));
            }
            catch (ArgumentException)
            {
                return null;
            }
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
        private static ManagementObjectCollection GetProcessTree(int processId)
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
        private static void KillProcessTree(int processId, ManagementObjectCollection childrenProcessTree)
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
