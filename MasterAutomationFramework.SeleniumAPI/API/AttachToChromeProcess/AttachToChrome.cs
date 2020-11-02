namespace MasterAutomationFramework.SeleniumAPI._00_Functionality._01_AttachToChromeProcess
{
    using System;
    using System.Diagnostics;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class AttachToChrome
    {
        IWebDriver Driver;
        const string FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
        const string UserDataDir = @"--user-data-dir=""C:\Users\Bartony\AppData\Local\Google\Chrome\User Data"" ";
        const string ProfileDirectory = @"--profile-directory=""Default"" ";

        private void StartChromeProcess()
        {
            //--profile-directory="Default"      "Profile 1" "Profile 2" etc.
            // --------------------------------------------------------------------------------------
            // --------------------------------------------------------------------------------------
            // How to understand google chrome version path location =>
            // chrome://version/ => C:\Users\Bartony\AppData\Local\Google\Chrome\User Data\Profile 1
            //    --user-data-dir=""C:\Users\Bartony\AppData\Local\Google\Chrome\User Data""  --profile-directory=""Profile 1""
            // --------------------------------------------------------------------------------------
            // --------------------------------------------------------------------------------------
            //C:\Users\Bartony\AppData\Local\Google\Chrome\User Data\Default
            //C:\Users\Bartony\AppData\Local\Google\Chrome\User Data\Profile 1
            // --------------------------------------------------------------------------------------
            // --------------------------------------------------------------------------------------
            Process proc = new Process();
            proc.StartInfo.FileName = FileName;
            proc.StartInfo.Arguments = $@"--new-window --remote-debugging-port=9222 {UserDataDir} {ProfileDirectory} ";
            proc.Start();
        }

        // ---------------------------------------------------
        // chrome://version/    -- Informacje O Profilu
        // ---------------------------------------------------
        // ---------------------------------------------------
        //  --flag-switches-begin --flag-switches-end   
        // This is useful to see which switches were added by about:flags on about:version. 
        // They don't have any effect.
        public void AttachToProcess()
        {
            try
            {
                this.StartChromeProcess();

                var m_Options = new ChromeOptions();
                m_Options.DebuggerAddress = "127.0.0.1:9222";
                m_Options.AddArgument($"{UserDataDir}");
                m_Options.AddArgument($"{ProfileDirectory}");

                Driver = new ChromeDriver(m_Options);
                this.Driver.Url = "https://google.pl";
                Console.WriteLine(this.Driver.Url);
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }
}
