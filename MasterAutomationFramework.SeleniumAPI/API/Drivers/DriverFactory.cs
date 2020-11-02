namespace MasterAutomationFramework.SeleniumAPI.API.DriverFactory
{
    using System;
    using Enums;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.Events;

    public static class DriverFactoryEx
    {
        public static IWebDriver CreateDriver(ChromeOptions options = null)
        {
            return CreateDriver(BrowserType.Chrome, options);
        }

        public static IWebDriver CreateDriver(BrowserType name = BrowserType.Chrome, ChromeOptions options = null)
        {
            switch (name)
            {
                case BrowserType.Chrome:
                    ChromeOptions chromeOption = options ?? new ChromeOptions();
                    return new ChromeDriver(chromeOption);
                case BrowserType.EventFiringWebDriver:
                    return new EventFiringWebDriver(new ChromeDriver(options ?? new ChromeOptions()));
                case BrowserType.Firefox:
                    throw new NotImplementedException();
                    break;
            }

            return null;
        }
    }
}