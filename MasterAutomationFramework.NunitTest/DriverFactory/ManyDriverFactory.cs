using FluentAssertions;
using NUnit.Framework;
using NunitTest.Configuration_ThirdOption;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.Essentials;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NunitTest.DriverFactory
{
    public static class ManyDriverFactory
    {
        private static ConcurrentDictionary<string, IWebDriver> _sessionDrivers;
        public static ConcurrentDictionary<string, IWebDriver> SessionDrivers
        {
            get
            {
                if (_sessionDrivers == null)
                {
                    _sessionDrivers = new ConcurrentDictionary<string, IWebDriver>();
                }
                return _sessionDrivers;
            }
        }

        /// <summary>
        /// Opens a Webdriver
        /// On Debug mode - will always open the browser on the local computer
        /// On Release mode - Check if the browser capability json is available and contains the 
        /// browserType defined in that file, else will fall back to the local browser
        /// </summary>
        /// <param name="browserType"></param>
        public static IWebDriver InitializeDriver(string browserType)
        {
            IWebDriver driver = null;

                driver = BrowserHelper.GetChromeBrowser();
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(JsonFileConfiguration.EnvData["PageLoadTimeoutInSeconds"].ToInteger());

            if (SessionDrivers.ContainsKey(TestContext.CurrentContext.Test.Name))
            {
                driver?.CloseDriver();
                SessionDrivers.ContainsKey(TestContext.CurrentContext.Test.Name)
                    .Should()
                    .BeFalse($"Driver already initiated for test: [{TestContext.CurrentContext.Test.Name}] with session id [{(driver as RemoteWebDriver).SessionId.ToString()}]");
            }
            else
            {
                SessionDrivers.TryAdd(TestContext.CurrentContext.Test.Name, driver);
            }
            return driver;
        }
    }
}
