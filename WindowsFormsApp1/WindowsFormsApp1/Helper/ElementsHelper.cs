using MasterAutomationFramework.SeleniumAPI.Elements;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WindowsFormsApp1.Main;
using MasterAutomationFramework;

namespace WindowsFormsApp1.Pages
{
    public class ElementsHelper
    {
        static ElementsWaitConditions wait = new ElementsWaitConditions(SeleniumDriver.WebDriver);

        public static IWebElement FindElement(By by, int timeout = 500)
        {
            return wait.FindElementSafe(by, timeout);
        }

        public static IWebElement FindElementSafe(By by, int timeout = 500)
        {
            return wait.FindElementSafe(by, timeout);
        }

        public static void WaitForElementNotExist(By by, TimeSpan timeSpan = default(TimeSpan))
        {
            SeleniumDriver.Wait.Timeout = timeSpan == default(TimeSpan) ? TimeSpan.FromSeconds(5) : timeSpan;
            SeleniumDriver.Wait.Until(x =>
            {
                try
                {
                    return !FindElement(by).Displayed;
                }
                catch (Exception e)
                {
                    return true;
                }
            });
        }

        public static void WaitForElementNotExist(IWebElement by, TimeSpan timeSpan = default(TimeSpan))
        {
            SeleniumDriver.Wait.Timeout = timeSpan == default(TimeSpan) ? TimeSpan.FromSeconds(5) : timeSpan;
            SeleniumDriver.Wait.Until(x =>
            {
                try
                {
                    return !by.Displayed;
                }
                catch (Exception e)
                {
                    return true;
                }
            });
        }

        public static void ClickElement(IWebElement by)
        {
            try
            {
                var frame = ElementsHelper.FindElementSafe(By.XPath("//*[@id='bot_check']//iframe"), timeout: 300);
                if (frame != null)
                {
                    SeleniumDriver.WebDriver.SwitchTo().Frame(frame);
                    By captcha = By.Id("rc-anchor-container");
                    ReadOnlyCollection<IWebElement> captchas = SeleniumDriver.WebDriver.FindElements(captcha);
                    if (captchas.Count != 0)
                    {
                        SeleniumDriver.WebDriver.FindElement(By.XPath("//*[@id='recaptcha-anchor']/div[1]")).Click();
                        ElementsHelper.WaitForElementNotExist(captchas.First(), TimeSpan.FromSeconds(6));
                        SeleniumDriver.WebDriver.SwitchTo().DefaultContent();
                    }
                }
            }
            catch (Exception)
            {

            }

            SeleniumDriver.Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
            SeleniumDriver.WebDriver.ExecuteJavaScript("arguments[0].scrollIntoView({block: 'center'});", by);
            by.Click();
        }

    }
}
