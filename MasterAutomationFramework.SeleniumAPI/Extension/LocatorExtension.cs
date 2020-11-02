namespace MasterAutomationFramework.SeleniumAPI.API.Drivers
{
    using OpenQA.Selenium;

    public static class LocatorExtension
    {
        private static IWebDriver _webDriver;

        private static ElementsWaitConditions elementConditions;

        public static IWebDriver WebDriver
        {
            get
            {
                if (_webDriver == null)
                    throw new WebDriverException("Web Driver Must be initialize before class usage");

                return _webDriver;
            }
            set
            {
                _webDriver = value;
                elementConditions = new ElementsWaitConditions(value);
            }
        }

        public static void ClickElement(this By elementLocator)
        {
            elementConditions.WaitForElementState(elementLocator, ElementState.ElementToBeClickable);
            WebDriver.FindElement(elementLocator).Click();
        }
    }
}
