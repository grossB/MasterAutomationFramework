using NunitTest.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterAutomationFramework
{
    // TO THE SAME EXTENSION FOR BY-LOCATOR INSTEAD OF IWEBELEMENT

    // IWebElement Extension Indicate by itself that Element state is operable, that means is was Wait before one of below action is called
    // In case there is no element exception will be thrown
    // To Handle dynamic/block_Steps element action Please use RetryActionHelper.cs with passing of Locator instead of IWebElement
    public static class WebElementExtensions
    {
        public static bool IsVisible2(this IWebElement element)
        {
            if (element == null)
            {
                return false;
            }

            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static void ResetField(this IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }

        public static void SelectDropDownByText(this IWebElement element, string text)
        {
            var selectElement = new SelectElement(element);
            selectElement.SelectByText(text);
        }

        public static void SelectDropDownByValue(this IWebElement element, string text)
        {
            var selectElement = new SelectElement(element);
            selectElement.SelectByValue(text);
        }

        public static void SelectDropDownByIndex(this IWebElement element, int index)
        {
            var selectElement = new SelectElement(element);
            selectElement.SelectByIndex(index);
        }

        public static IList<IWebElement> GetOptions(this IWebElement element)
        {
            var selectElement = new SelectElement(element);
            return selectElement.Options;
        }

        public static IWebElement GetOption(this IWebElement element, string text)
        {
            var selectElement = new SelectElement(element);
            return selectElement.Options.First(o => o.Text.Contains(text));
        }

        public static int GetSelectedOptionIndex(this IWebElement element)
        {
            var selectElement = new SelectElement(element);
            var options = selectElement.Options;
            return options.IndexOf(options.FirstOrDefault(o => o.Text.Contains(selectElement.SelectedOption.Text)));
        }

        public static string GetSelectedOptionValue(this IWebElement element)
        {
            var selectElement = new SelectElement(element);
            return selectElement.SelectedOption.GetAttribute("value");
        }

        public static string GetSelectedOptionText(this IWebElement element)
        {
            var selectElement = new SelectElement(element);
            return selectElement.SelectedOption.Text;
        }

        public static void TryClick(this IWebElement element)
        {
            try
            {
                element.Click();
            }
            catch (Exception)
            {
                Console.WriteLine("Element does not exist");
            }
        }

        public static void SafeClick(this IWebElement element)
        {
            SafeClick(element, 5);
        }

        public static void SafeClick(this IWebElement element, int seconds)
        {
            try
            {
                // IWebElement Extension Indicate by itself that Element state is operable, that means is was Wait before one of below action is called
                //var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
                //wait.IgnoreExceptionTypes(typeof(WebDriverException));
                //wait.Until(d => element.Displayed);

                element.Click();
            }
            catch (Exception)
            {
                try
                {
                    //new MasterAutomationFramework.SeleniumAPI.Elements.JsExecutor(new ChromeDriver()).JavaScriptClick(element);
                }
                catch (Exception actualException)
                {
                    throw new WebDriverException("Cannot click element! Exception: " + actualException);
                }
            }
        }

        public static void WaitElementSendKeys(this IWebElement element, string text)
        {
            WaitElementSendKeys(element, text, 10);
        }

        public static void WaitElementSendKeys(this IWebElement element, string text, int seconds)
        {
            element.SendKeys(text);
        }

        public static bool ExistsAndDisplayed(this IWebElement webElement, int currentRetry = 0)
        {
            try
            {
                return webElement.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            // it can occur exception with a message
            // "stale element reference: element is not attached to the page document"
            // It occurs if check appears in the middle of changing state on an element
            catch (StaleElementReferenceException)
            {
                if (currentRetry <= 2)
                {
                    ExistsAndDisplayed(webElement, ++currentRetry);
                }

                return false;
            }
        }

        public static bool IsValueEmpty(this IWebElement element) => string.IsNullOrEmpty(element.GetAttribute("value"));

        // ----------------------------------------------- SELENIUM ESENTIALS -----------------------------------------------
        // ----------------------------------------------- SELENIUM ESENTIALS -----------------------------------------------
        // ----------------------------------------------- SELENIUM ESENTIALS -----------------------------------------------

        /// <summary>
        /// Whether element is readonly
        /// </summary>
        /// <param name="element">element on which operation needs to be performed</param>
        /// <returns>true if readonly or else false</returns>
        public static bool IsReadonly(this IWebElement element) => element.Exists() && element.GetAttribute("readonly").HasValue();

        /// <summary>
        /// Whether element is disabled (conditions to satisfy - Exists(), not Enabled(), not Readonly())
        /// </summary>
        /// <param name="element">element on which operation needs to be performed</param>
        /// <returns>true if disabled or else false</returns>
        public static bool IsDisabled(this IWebElement element) => element.Exists() && !(element.IsEnabled() || element.IsReadonly());

        /// <summary>
        /// Whether element is enabled
        /// </summary>
        /// <param name="element">element on which operation needs to be performed</param>
        /// <returns>true if enabled or else false</returns>
        public static bool IsEnabled(this IWebElement element) => element.Exists() && element.Enabled;

        /// <summary>
        /// Whether element is visible
        /// </summary>
        /// <param name="element">element on which operation needs to be performed</param>
        /// <returns>true if visible or else false</returns>
        public static bool IsVisible(this IWebElement element) => element.Exists() && element.Displayed && element.IsCssDisplayed();

        /// <summary>
        /// Whether element is CSS displayed (display: none is not applied to the element)
        /// </summary>
        /// <param name="element">element on which operation needs to be performed</param>
        /// <returns>true if css displayed or else false</returns>
        public static bool IsCssDisplayed(this IWebElement element) => element.Exists() && !element.GetCssValue("display").EqualsIgnoreCase("none");

        /// <summary>
        /// Whether element exists
        /// </summary>
        /// <param name="element">element on which operation needs to be performed</param>
        /// <returns>true if the element exists or else false</returns>
        public static bool Exists(this IWebElement element)
        {
            try
            {
                return element != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the value of the [value] attribute of the element 
        /// </summary>
        /// <param name="element">element on which operation needs to be performed</param>
        /// <returns>attribute:value's content</returns>
        public static string Value(this IWebElement element) => element.GetAttribute("value");

        /// <summary>
        /// Returns the text available on the element. Either text or the attribute("value") is returned which ever is available
        /// </summary>
        /// <param name="element">element on which operation needs to be performed</param>
        /// <returns>Text or Attribute(value) which ever is available</returns>
        public static string Text(this IWebElement element)
        {
            try
            {
                if (element.Text.HasValue())
                {
                    return element.Text;
                }

                try
                {
                    if (element.Value().HasValue())
                    {
                        return element.Value();
                    }
                }
                catch (Exception)
                {
                    // ignored
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns all the class available on the element
        /// </summary>
        /// <param name="element">element on which operation needs to be performed</param>
        /// <returns>class available to the element</returns>
        public static string Class(this IWebElement element) => element.GetAttribute("class");

        /// <summary>
        /// Returns all class available on the element
        /// </summary>
        /// <param name="element">element on which operation needs to be performed</param>
        /// <returns>collection of class as an array available to the element</returns>
        public static string[] Classes(this IWebElement element) => element.Class().SplitAndTrim(" ").ToArray();
    }
}
