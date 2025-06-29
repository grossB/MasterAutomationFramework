using System;
using System.Configuration;
using System.IO;
using MasterAutomationFramework.Common.ConfigurationHelper;
using NunitTest.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;

namespace MasterAutomationFramework.SeleniumAPI.API.Drivers
{
    public sealed class DriverFactoryWithOptions
    {
        public static string ConsoleLogFilePath { get; set; }

        public static IWebDriver BuildWebDriver(string supportedBrowser)
        {
            const string HubAddress = "local";
            var localSelenium = HubAddress.Equals("local", StringComparison.OrdinalIgnoreCase);

            switch (supportedBrowser)
            {
                case "Chrome":
                    var opt = new ChromeOptions();
                    opt.AddArgument("--no-sandbox");
                    opt.AddExcludedArgument("enable-automation");
                    opt.AddAdditionalCapability("useAutomationExtension", false);
                    return InitChromeDriver(localSelenium ? opt : new ChromeOptions(), localSelenium);

                case "ChromeBeta":
                    opt = new ChromeOptions();
                    opt.SetLoggingPreference(LogType.Browser, LogLevel.All);

                    opt.AddArgument("--no-sandbox");
                    opt.AddArgument("--enable-experimental-web-platform-features");
                    opt.AddArgument("--enable-usermedia-screen-capturing");
                    opt.AddArgument("--allow-http-screen-capture");
                    opt.AddArgument("--auto-select-desktop-capture-source=Entire screen");
                    opt.AddArgument("--autoplay-policy=no-user-gesture-required");
                    opt.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
                    opt.AddUserProfilePreference("download.default_directory", ConfigHelper.RecordVideosPath);
                    opt.AddExcludedArgument("enable-automation");
                    opt.AddAdditionalCapability("useAutomationExtension", false);
                    return InitChromeDriver(opt, localSelenium, chromeBeta: true);

                case "ChromeHeadless":
                    var options = new ChromeOptions();
                    options.SetLoggingPreference(LogType.Browser, LogLevel.All);
                    options.AddArgument("headless");
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--window-size=1920,1080");
                    options.AddExcludedArgument("enable-automation");
                    options.AddAdditionalCapability("useAutomationExtension", false);
                    options.AddArgument("--enable-experimental-web-platform-features");
                    options.AddArgument("--enable-usermedia-screen-capturing");
                    options.AddArgument("--allow-http-screen-capture");
                    options.AddArgument("--auto-select-desktop-capture-source=Entire screen");
                    options.AddArgument("--autoplay-policy=no-user-gesture-required");
                    options.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
                    options.AddUserProfilePreference("download.default_directory", ConfigHelper.RecordVideosPath);
                    return InitChromeDriver(options, localSelenium, chromeBeta: true);

                case "MobileChrome":
                    var mobileChromeOptions = new ChromeOptions();
                    mobileChromeOptions.AddArgument("headless");
                    mobileChromeOptions.AddArgument("--no-sandbox");
                    mobileChromeOptions.AddArgument("--disable-gpu");
                    mobileChromeOptions.AddArgument("--window-size=1920,1080");
                    mobileChromeOptions.AddExcludedArgument("enable-automation");
                    mobileChromeOptions.AddAdditionalCapability("useAutomationExtension", false);
                    mobileChromeOptions.EnableMobileEmulation("iPhone X");
                    return InitChromeDriver(mobileChromeOptions, localSelenium, chromeBeta: true);

                case "Firefox":
                case "IE":
                case "Edge": return  null;
            }

            return null;
        }

        private static IWebDriver InitChromeDriver(ChromeOptions options, bool localSelenium, bool chromeBeta = false)
        {
            if (localSelenium)
            {
                try
                {
                    if (chromeBeta)
                    {
                        options.BinaryLocation = "C:\\Program Files\\Google\\Chrome Beta\\Application\\chrome.exe";
                    }

                    // Solution for exception: "HTTP request to the remote WebDriver server for URL http:/….. timed out after 60 seconds"
                    var service = ChromeDriverService.CreateDefaultService();

                    //if (ConfigHelper.CollectinJavaScriptLogs)
                    //{
                    //    var fileNameBase = $"error_{DateTime.Now:yyyyMMdd_HHmmss}_{DateTime.Now:yyyyMMdd_HHmmss}";
                    //    var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "testresults");

                    //    options.SetLoggingPreference(LogType.Browser, LogLevel.Severe);
                    //    //options.SetLoggingPreference(LogType.Driver, LogLevel.All);
                    //    //options.SetLoggingPreference(LogType.Client, LogLevel.All);
                    //    //options.SetLoggingPreference(LogType.Profiler, LogLevel.All);
                    //    //options.SetLoggingPreference(LogType.Server, LogLevel.All);

                    //    if (!Directory.Exists(artifactDirectory))
                    //    {
                    //        Directory.CreateDirectory(artifactDirectory);
                    //    }

                    //    ConsoleLogFilePath = Path.Combine(artifactDirectory, StringExtensions.GetSubstringIfStringLongerThan("ScenarioName___", 70) + fileNameBase + "_ConsoleLog.log");

                    //    service.LogPath = ConsoleLogFilePath;
                    //    service.EnableVerboseLogging = true;

                    //    var driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(120));
                    //    return driver;
                    //}

                    return new ChromeDriver(service, options);
                }
                catch (WebDriverException ex)
                {
                    MasterAutomationFramework.Common.Helper.ProcessCleanupHelper.KillProcesses("chromedriver");
                    throw ex;
                }
            }

            return null; //new RemoteWebDriver(new Uri(ConfigurationManager.AppSettings["Selenium.Hub"]), options);
        }
    }
}
