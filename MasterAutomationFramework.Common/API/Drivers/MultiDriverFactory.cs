namespace MasterAutomationFramework.SeleniumAPI.API.Drivers
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using SeleniumAPI.Enums;
    using System;
    using System.Collections.Generic;

    public class MultiDriverFactory
    {
        private static readonly IDictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        private static IWebDriver driver;

        public static IWebDriver Driver
        {
            get
            {
                if (driver == null)
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
                return driver;
            }
            private set
            {
                driver = value;
            }
        }

        public static void InitBrowser(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Firefox:
                    if (Driver == null)
                    {
                        driver = new FirefoxDriver();
                        Drivers.Add("Firefox", Driver);
                    }
                    break;

                case BrowserType.EventFiringWebDriver:
                    //if (Driver == null)
                    //{
                    //    driver = new InternetExplorerDriver(@"C:\PathTo\IEDriverServer");
                    //    Drivers.Add("IE", Driver);
                    //}
                    throw new NotImplementedException();
                    break;

                case BrowserType.Chrome:
                    if (Driver == null)
                    {
                        driver = new ChromeDriver(@"C:\PathTo\CHDriverServer");
                        Drivers.Add("Chrome", Driver);
                    }
                    break;
            }
        }

        public static void LoadApplication(string url)
        {
            Driver.Url = url;
        }

        public static void CloseAllDrivers()
        {
            foreach (var key in Drivers.Keys)
            {
                Drivers[key].Close();
                Drivers[key].Quit();
            }
        }
    }
}
