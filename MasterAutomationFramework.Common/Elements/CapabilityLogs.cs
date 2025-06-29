namespace SeleniumAPI.Elements
{
    using MasterAutomationFramework.Common.ConfigurationHelper;
    using MasterAutomationFramework.Common.Helper;
    using MasterAutomationFramework.Common.Models;
    using MasterAutomationFramework.SeleniumAPI.API.Drivers;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    // Driver dispose has to be called in order to saved browserLog to a file 
    public class CapabilityLogs
    {
        IWebDriver Driver;
        const string LogFilePath = "C:\\chromedriver.log";

        public void AnalyzeWebBrowserLogForJsError()
        {
            if (ConfigHelper.CollectinJavaScriptLogs)
            {
                var errorStrings = new List<string>
                {
                    "SyntaxError",
                    "EvalError",
                    "ReferenceError",
                    "RangeError",
                    "URIError",

                    // commented out because of misleading information
                    // "TypeError",
              		// Worthy to check manually in log
                                           // "Exception",
                                           // "Error\""(CaseSensitive),
                                           // "\"exceptionId\": "
                };

                var fileLogContent = string.Empty;
                var browserLogEnteries = new List<BrowserEntryLog>();

                try
                {
                    RetryActionHelper.RunActionWithRetries(
                    () =>
                    {
                        fileLogContent = File.ReadAllText(DriverFactoryWithOptions.ConsoleLogFilePath).Replace(Environment.NewLine, string.Empty);
                    }, retryCount: 20, sleepDuration: TimeSpan.FromSeconds(3));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Can not ready log file. Seems like file is being used. {e}");
                }

                Regex.Split(fileLogContent, "[d+.d+][(?<level>[a-z]*[A-Z]*]:", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(500))
                    .ToList()
                    .ForEach(x => browserLogEnteries.Add(new BrowserEntryLog(x)));

                var jsErrors = browserLogEnteries.Where(x => errorStrings.Any(e => x.Content.Contains(e)));

                if (jsErrors.Any())
                {
                    Console.WriteLine($"{Environment.NewLine}JavaScript error(s):{Environment.NewLine} {jsErrors.Aggregate(string.Empty, (s, entry) => s + entry.Content + Environment.NewLine)}");
                    throw new Exception("JavaScript error found");
                }
                Console.WriteLine($"Console log file: {new Uri("DriverFactory.ConsoleLogFilePath")}");
            }
        }







        // Chrome driver debug logging info save to file.
        public void Logs2_DriverBinaryLogging()
        {
            var service = ChromeDriverService.CreateDefaultService();
            service.LogPath = LogFilePath;
            service.EnableVerboseLogging = true;
            var options = new ChromeOptions();
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            //options.SetLoggingPreference(LogType.Driver, LogLevel.All);
            //options.SetLoggingPreference(LogType.Client, LogLevel.All);
            //options.SetLoggingPreference(LogType.Profiler, LogLevel.All);
            //options.SetLoggingPreference(LogType.Server, LogLevel.All);
            options.AddArguments("--start-maximized");
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalCapability("useAutomationExtension", false);
            this.Driver = new ChromeDriver(service, options);
            this.Driver.Url = "https://demos.telerik.com/aspnet-mvc/grid";
            var gg = this.Driver.FindElement(By.XPath("//*[@id='source-code']/ul/li[2]/span"));
            try
            {
                this.Driver.Url = "https://www.telerik.com/kendo-jquery-ui?_ga=2.83424790.1039773608.1572957809-1960745617.1572957809";
                ((IJavaScriptExecutor)this.Driver).ExecuteScript("return console.log('sdsd');");
                ((IJavaScriptExecutor)this.Driver).ExecuteScript("return 'Some Console Info from java script console' ");
                gg.Click();
            }
            catch (Exception e)
            {
                this.Driver.Quit();
                this.Driver.Dispose();
            }
        }

        public List<string> SearchForStringInLogFile()
        {
            var lines = new List<string>();

            try
            {
                using (StreamReader fs = new StreamReader(LogFilePath))
                {
                    string line = fs.ReadLine();
                    while (fs.Peek() >= 0)
                    {
                        if (line.Contains("Some Console Info from java script console"))
                            lines.Add(line);
                        line = fs.ReadLine();
                    }
                }
            }
            catch (Exception){}

            return lines;
        }

        // Currently Disable
        public void Logs()
        {
            // Selenium Webdriver 3.141.0 driver.Manage().Logs.AvailableLogTypes throwing System.NullReference Exception
            // It has been fixed in the .NET bindings already, and will be released in the forthcoming 4.0 alpha 3 release
            var options = new ChromeOptions();
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            options.SetLoggingPreference(LogType.Driver, LogLevel.All);
            options.SetLoggingPreference(LogType.Client, LogLevel.All);
            options.SetLoggingPreference(LogType.Profiler, LogLevel.All);
            options.SetLoggingPreference(LogType.Server, LogLevel.All);

            options.AddArguments("--start-maximized");
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalCapability("useAutomationExtension", false);
            this.Driver = new ChromeDriver(options);
            this.Driver.Url = "https://demos.telerik.com/aspnet-mvc/grid";

            try
            {
                var availableLogs = new List<LogEntry>();

                foreach (var logsAvailableLogType in this.Driver.Manage().Logs.AvailableLogTypes)
                {
                    availableLogs.AddRange(this.Driver.Manage().Logs.GetLog(logsAvailableLogType).ToList());
                    Console.WriteLine(logsAvailableLogType);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
