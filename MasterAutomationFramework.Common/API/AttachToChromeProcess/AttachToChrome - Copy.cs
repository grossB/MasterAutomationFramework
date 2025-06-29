namespace MasterAutomationFramework.SeleniumAPI._00_Functionality._01_AttachToChromeProcess
{
    using System;
    using System.Diagnostics;
    using MasterAutomationFramework.Common.Helper;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class AttachToChrome2
    {
        IWebDriver Driver;
        const string FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
        const string UserDataDir = @"--user-data-dir=""C:\Users\Bartony\AppData\Local\Google\Chrome\User Data"" ";
        const string ProfileDirectory = @"--profile-directory=""Default"" ";

        // ---------------------------------------------------
        // chrome://version/    -- Informacje O Profilu
        // ---------------------------------------------------
        public IWebDriver AttachToProcess()
        {
            // Kill All chrome processes
            ProcessCleanupHelper.KillChromeDriverTreeProcessGracefully();
            //ProcessCleanupHelper.ForceKillAllChromeRelatedProcesses();

            try
            {
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
                // Hide Console
                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                Driver = new ChromeDriver(chromeDriverService, chromeOptions);
                //Driver.SwitchTo().Window(Driver.WindowHandles.First());
                Driver.ExecuteJavaScript("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})");
                //Driver.Url = "https://google.pl";
                //var gg = Driver.WindowHandles;

                Console.WriteLine(Driver.Title);
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            return Driver;
        }
    }
}
