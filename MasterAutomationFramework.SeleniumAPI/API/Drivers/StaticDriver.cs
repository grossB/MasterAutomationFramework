
namespace MasterAutomationFramework.SeleniumAPI.API.Drivers
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public static class Web
    {
        private static IWebDriver _driver;

        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    var options = new ChromeOptions();
                    options.AddArguments("--start-maximized");
                    options.AddExcludedArgument("enable-automation");
                    options.AddAdditionalCapability("useAutomationExtension", false);
                    _driver = new ChromeDriver(options);
                }

                return _driver;
            }
        }
    }
}

