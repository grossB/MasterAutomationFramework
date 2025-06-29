namespace MasterAutomationFramework.SeleniumAPI._00_Functionality._01_AttachToChromeProcess
{
    using System;
    using System.Diagnostics;
    using MasterAutomationFramework.SeleniumAPI.API.Drivers;
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
            //  chrome://version/
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
                //this.StartChromeProcess();

                //https://stackoverflow.com/questions/53039551/selenium-webdriver-modifying-navigator-webdriver-flag-to-prevent-selenium-detec
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument(@"--user-data-dir=C:\Users\Bartony\AppData\Local\Google\Chrome\User Data");
                chromeOptions.AddArgument("--remote-debugging-port=9222");
                chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");  // To prevent Selenium driven WebDriver getting detected


                chromeOptions.AddExcludedArgument("test-type");
                chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
                chromeOptions.AddExcludedArgument("enable-automation");
                chromeOptions.AddExcludedArgument("disable-blink-features");
                chromeOptions.AddExcludedArgument("allow-pre-commit-input");
                chromeOptions.AddExcludedArgument("disable-background-networking");

                chromeOptions.AddExcludedArgument("disable-backgrounding-occluded-windows");
                chromeOptions.AddExcludedArgument("disable-client-side-phishing-detection");
                chromeOptions.AddExcludedArgument("disable-default-apps");

                chromeOptions.AddExcludedArgument("disable-hang-monitor");
                chromeOptions.AddExcludedArgument("disable-popup-blocking");
                chromeOptions.AddExcludedArgument("disable-prompt-on-repost");

                chromeOptions.AddExcludedArgument("disable-sync");
                chromeOptions.AddExcludedArgument("enable-blink-features=ShadowDOMV0");
                chromeOptions.AddExcludedArgument("new-window ");


                //chromeOptions.AddExcludedArguments("enable-automation --allow-pre-commit-input --disable-background-networking 
                //--disable-backgrounding-occluded-windows --disable-client-side-phishing-detection --disable-default-apps 
                //--disable-hang-monitor --disable-popup-blocking --disable-prompt-on-repost 
                //--disable-sync --enable-automation --enable-blink-features=ShadowDOMV0");

                Driver = new ChromeDriver(chromeOptions);
                Driver.ExecuteJavaScript("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})");

                this.Driver.Url = "https://google.pl";
                Console.WriteLine(this.Driver.Url);

                ReuseableWebDriver.SaveSessionKey(this.Driver);
                new ReuseableWebDriver().Url = "https://google.pl";
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            //chromeOptions.AddArgument(@"--user-data-dir=""C:\Users\Bartony\AppData\Local\Google\Chrome\User Data"" --profile-directory=""Profile 1""");
            //chromeOptions.AddArguments("disable-infobars");
            //chromeOptions.DebuggerAddress = "127.0.0.1:9222";
            //m_Options.DebuggerAddress = "--remote-debugging-port=9222";
            //chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
            //chromeOptions.AddExcludedArgument("enable-automation");
            //chromeOptions.BinaryLocation = FileName;
        }
    }
}
