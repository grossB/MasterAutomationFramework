namespace MasterAutomationFramework.SeleniumAPI.Elements
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using MasterAutomationFramework.SeleniumAPI.Enums;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

    public class ElementsWaitConditions
    {
        /// <summary>
        /// Gets or set current API WebDriver.
        /// </summary>
        private IWebDriver Driver { get; set; }

        public ElementsWaitConditions(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public void WaitForElementState(By elementLocator, ElementState state, TimeSpan timeout = default(TimeSpan), List<Type> ignoreExceptionTypes = null)
        {
            if (timeout == default(TimeSpan))
            {
                timeout = TimeSpan.FromSeconds(10);
            }

            WebDriverWait seleniumDriverWait = new WebDriverWait(this.Driver, timeout);
            ignoreExceptionTypes?.ForEach(exceptionType => seleniumDriverWait.IgnoreExceptionTypes(exceptionType));

            switch (state)
            {
                case ElementState.ElementToBeClickable:
                    seleniumDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(elementLocator));
                    break;
                case ElementState.ElementIsVisible:
                    seleniumDriverWait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
                    break;
                case ElementState.ElementExists:
                    seleniumDriverWait.Until(ExpectedConditions.ElementExists(elementLocator));
                    break;
                case ElementState.InvisibilityOfElementLocated:
                    seleniumDriverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(elementLocator));
                    break;
                default:
                    break;
            }

        }

        public TResult WaitCondition<TResult>(Func<IWebDriver, TResult> condition, TimeSpan timeout = default(TimeSpan), List<Type> ignoreExceptionTypes = null, int interval = 500)
        {
            if (timeout == default(TimeSpan))
            {
                timeout = TimeSpan.FromSeconds(10);
            }

            WebDriverWait seleniumDriverWait = new WebDriverWait(this.Driver, timeout);
            ignoreExceptionTypes?.ForEach(exceptionType => seleniumDriverWait.IgnoreExceptionTypes(exceptionType));

            if (interval != 500)
            {
                seleniumDriverWait.PollingInterval = TimeSpan.FromMilliseconds(interval);
            }

            TResult result = seleniumDriverWait.Until(condition);
            return result;
        }

        public bool Retry(Func<bool> action, int retry, int interval)
        {
            int attemptCounter = 0;
            bool output = false;
            while (attemptCounter < retry)
            {
                try
                {
                    output = action.Invoke();
                    break;
                }
                catch (Exception e)
                {
                    Thread.Sleep(interval);
                    attemptCounter++;
                    continue;
                }
            }
            return output;
        }

        public void VerifySendKeys(IWebElement element, string text)
        {
            WebDriverWait driverWait = new WebDriverWait(driver: Driver, TimeSpan.FromSeconds(10));
            driverWait.PollingInterval = TimeSpan.FromMilliseconds(200);
            driverWait.Until(x =>
            {
                element.Clear();
                element.SendKeys(text);
                string value = element.GetAttribute("value");
                return value == text;
            });

            #region Alternative way to send each key with interval

            //foreach (var character in text)
            //{
            //    //zamiast tego można tak
            //    //element.SendKeys(character.ToString());
            //    //Thread.Sleep(TimeSpan.FromMilliseconds(200));
            //    string send = "";
            //    this.Wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            //    this.Wait.Until( x =>
            //    {
            //        send += character.ToString();
            //        element.SendKeys(character.ToString());
            //        string value = element.GetAttribute("value");
            //        return value == send;
            //    }
            //    );
            //}

            #endregion
        }

        /// <summary>
        /// Trying to find element within certain time and returns null if element was not on page.
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeout"></param>
        /// <returns>IWebElement</returns>
        public IWebElement FindElementSafe(By by, int timeout = 3500)
        {
            try
            {
                WebDriverWait driverWait = new WebDriverWait(
                    driver: Driver,
                    timeout == 3500 ? TimeSpan.FromMilliseconds(3500) : TimeSpan.FromMilliseconds(timeout));
                driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
                var element = Driver.FindElement(by);
                return element;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
