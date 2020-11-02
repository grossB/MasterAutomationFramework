using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumAPI.Elements;
using MasterAutomationFramework.SeleniumAPI.Enums;
using MasterAutomationFramework.SeleniumAPI.API.DriverFactory;
using MasterAutomationFramework.SeleniumAPI.Helper;
using MasterAutomationFramework.SeleniumAPI.API.ElementHighlight;
using MasterAutomationFramework.SeleniumAPI.Drivers;
using GenericTemplates.GeneratedClasses;
using MasterAutomationFramework.SeleniumAPI.Elements;

namespace MasterAutomationFramework.SeleniumAPI.Tests.UnitTests
{
    public class BasicSeleniumFunctionality
    {
        static IWebDriver driver;
        static JsExecutor ScriptExecutor;

        [TestInitialize]
        public void BeforeEachTest()
        {
            try
            {
                var url = driver.Url;
            }
            catch (Exception)
            {
                driver = DriverFactoryEx.CreateDriver(BrowserType.Chrome);
                ScriptExecutor = new JsExecutor(driver);
            }
        }

        [TestMethod]
        public void BasicWait()
        {
            // Implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            // Asynchronous Js, could be useful with AJAX 
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
            // Create object instance and pass default timeout
            WebDriverWait webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            // Interval between iteration
            webDriverWait.PollingInterval = TimeSpan.FromMilliseconds(500);
            // Ignore exceptions of type
            webDriverWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException), typeof(NullReferenceException));
            // Override default timeout
            webDriverWait.Timeout = TimeSpan.FromSeconds(15);
            // Condition
            webDriverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='someId']")));
            // Custom condition
            webDriverWait.Until(driver => driver.FindElement(By.XPath("//*[@id='someId']")).Text != null);
            // Complex condition
            webDriverWait.Until(driver =>
            {
                bool result = driver.FindElement(By.XPath("//*[@id='someId']")).Text != null;
                return result;
            });
        }

        [TestMethod]
        [TestCategory("Option Elements")]
        [Description("ElementNotInteractableException - Element not click-able, not intractable")]
        public void CheckBox()
        {
            driver.Url = "https://www.toolsqa.com/automation-practice-form/";
            var checkbox = driver.FindElement(By.Id("sex-0"));
            Assert.AreEqual(true, checkbox.Enabled);
            Assert.AreEqual(false, checkbox.Selected);
            try
            {
                checkbox.Click();
            }
            catch (ElementNotInteractableException e)
            {
                Console.WriteLine(e.ToString());
            }

            ScriptExecutor.ScrollIntoView(checkbox);
            checkbox.Click();
            Assert.AreEqual(true, checkbox.Selected);
        }

        [TestMethod]
        [TestCategory("Option Elements")]
        public void SelectElement()
        {
            driver.Url = "https://www.toolsqa.com/automation-practice-form/";
            SelectElement selectElement = new SelectElement(driver.FindElement(By.Id("continentsmultiple")));
            Assert.AreEqual(true, selectElement.IsMultiple);
            selectElement.SelectByIndex(0);
            selectElement.SelectByIndex(1);
            Assert.AreEqual(2, selectElement.AllSelectedOptions.Count);
            selectElement.DeselectAll();
            Assert.AreEqual(0, selectElement.AllSelectedOptions.Count);
        }

        [TestMethod]
        [TestCategory("Driver")]
        public void DriverLevelProperties()
        {
            driver.Navigate().GoToUrl("https://selenium.dev/");
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open('https://selenium.dev/support/')");
            var id_CurrentOverlap = driver.CurrentWindowHandle;
            var overlapIds = driver.WindowHandles;
            var pageSource = driver.PageSource;
            var windowTitle = driver.Title;
            var currentUrl = driver.Url;

            Assert.AreEqual("https://selenium.dev/", currentUrl);
            Assert.AreEqual(2, overlapIds.Count);
            driver.Quit();
        }

        [TestMethod]
        [TestCategory("Element Properties")]
        public void FindElements()
        {
            driver.Url = "https://selenium.dev/";
            driver.Manage().Window.Maximize();
            //1. find button by Xpath
            var input = driver.FindElement(By.XPath("//*[@class='getting-started-topic']"));
            var articles = driver.FindElements(By.XPath("//*[@class='getting-started-topic']"));

            Assert.AreEqual(typeof(RemoteWebElement), input.GetType());
            Assert.AreEqual(3, articles.Count);
        }

        [TestMethod]
        [TestCategory("Element Properties")]
        public void ElementMethods()
        {
            const string TextToSearch = "Some text";
            By searchElemenetSelector = By.Id("gsc-i-id1");
            driver.Url = "https://selenium.dev/";
            driver.Manage().Window.Maximize();
            var input = driver.FindElement(searchElemenetSelector);

            input.SendKeys(TextToSearch);
            Assert.AreEqual(TextToSearch, driver.FindElement(searchElemenetSelector).GetAttribute("value"));
            input.Clear();
            Assert.AreEqual(string.Empty, driver.FindElement(searchElemenetSelector).GetAttribute("value"));
            input.SendKeys(TextToSearch);
            input.Submit();
            Assert.AreNotEqual(driver.Url, "https://selenium.dev/");
        }

        [TestMethod]
        [TestCategory("Element Properties")]
        public void ElementInterrogationTest()
        {
            driver.Url = "http://seleniumhq.pl/";
            driver.Manage().Window.Maximize();
            //1. find button by TagName
            var button = driver.FindElement(By.TagName("Button"));
            //2. GetAttribute("type") and assert that it equals the right value
            Assert.AreEqual("submit", button.GetAttribute("type"));
            //3. GetCssValue("background-color") and assert that it equals the correct value
            Assert.AreEqual("rgba(38, 139, 79, 1)", button.GetCssValue("background-color"));
            //4. Assert that it's Displayed
            Assert.IsTrue(button.Displayed);
            //5. Assert that it's Enabled
            Assert.IsTrue(button.Enabled);
            //6. Assert that it's NOT selected
            //Element is an input element with a type attribute, in the Checkbox-or Radio Button state
            //element is an "option element". For Definition look on => https://html.spec.whatwg.org/#the-option-element
            //Otherwise
            //False.
            Assert.IsFalse(button.Selected);
            //7. Assert that the Text is correct
            Assert.AreEqual(button.Text, "SEARCH");
            Assert.AreEqual(button.GetAttribute("innerText"), "SEARCH");
            //8. Assert that the TagName is correct
            Assert.AreEqual(button.TagName, "button");
            //9. Assert that the size height is 50
            Assert.AreEqual(50, button.Size.Height);
            Assert.AreEqual(60, button.Size.Width);
            //10. Assert that the location is x=190, y=330
            Assert.AreEqual(1280, button.Location.X);
            Assert.AreEqual(194, button.Location.Y);
        }

        [TestMethod]
        [TestCategory("Navigation")]
        [Description("Two possible ways to load WWW address. It is also possible to pass it as ChromeOption Parameter")]
        public void UrlNavigation()
        {
            driver.Navigate().GoToUrl("https://selenium.dev/support/");
            Assert.AreEqual("https://selenium.dev/support/", driver.Url);
            driver.Url = "https://selenium.dev/";
            Assert.AreEqual("https://selenium.dev/", driver.Url);
        }

        [TestMethod]
        [TestCategory("Navigation")]
        [Description("Methods of Interface INavigate")]
        public void SeleniumNavigation()
        {
            driver.Navigate().GoToUrl("https://selenium.dev/");
            driver.Navigate().GoToUrl("https://selenium.dev/support/");
            driver.Navigate().Back();
            driver.Navigate().Forward();
            driver.Navigate().Refresh();
            driver.Manage().Window.FullScreen();
            driver.Manage().Window.Minimize();
            driver.Manage().Window.Maximize();
            Assert.AreEqual("https://selenium.dev/support/", driver.Url);
        }

        //[TestMethod]
        //[TestCategory("Locator's")]
        //public void ReflectedByAttribute()
        //{
        //    // --------------------- Reflected By attribute --------------------------------
        //    var page = new ExamplePage();
        //    var locator = page.MyAccount.GetElementLocator(typeof(ExamplePage), "MyAccount");
        //    Assert.AreEqual(By.Id("account"), locator);
        //}

        //[TestMethod]
        //[TestCategory("Locator's")]
        //public void ReflectedByPageElement()
        //{
        //    // --------------------- Reflected By Element --------------------------------
        //    var googlePage = new GooglePage(new SeleniumAPI.SeleniumApi(driver));
        //    By googleElementSelector = googlePage.Selectors[nameof(googlePage.NoobImageElement)];
        //    // For Testing Purpose
        //    By NoobImageBy = By.XPath("//*[@id='main']/ul[1]/li[3]/a/ul/li[1]/img");
        //    Assert.AreEqual(NoobImageBy.ToString(), googleElementSelector.ToString());
        //}

        [TestMethod]
        [TestCategory("Element Highlight")]
        [Description("Element is highlighted for yellow for 2500 milliseconds")]
        public void ElementHighlightEvent()
        {
            // --------------------- DriverEvent Element Highlighter --------------------------------
            EventHighlighter eventHighlighter = new EventHighlighter(driver);

            driver.Navigate().GoToUrl("https://www.automatetheplanet.com/framework-extensibility-highlight-elements/#tab-con-2");
            var elementHighlight = eventHighlighter.webDriver.FindElement(By.XPath("//*[@id='tve_editor']/div[28]"));
            var originalElementBorder = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].style.backgroundColor", elementHighlight);
            elementHighlight.Click();
            Thread.Sleep(1000);
            var changedElementBorder = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].style.backgroundColor", elementHighlight);
            Thread.Sleep(3000);
            var RestoredElementBorder = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].style.backgroundColor", elementHighlight);

            driver.Navigate().GoToUrl("https://www.google.pl");
            Assert.AreEqual(driver.Url, "https://www.google.pl/");
            Assert.AreNotEqual(originalElementBorder, changedElementBorder);
            Assert.AreEqual(originalElementBorder, RestoredElementBorder);
        }

        [TestMethod]
        [TestCategory("Element Highlight")]
        public void ElementHighlight()
        {
            // --------------------- DriverEvent Element Highlighter --------------------------------
            var highLihghter = new ElementHighlighter(driver);
            highLihghter.webDriver.Url = "https://selenium.dev/";
            var element = highLihghter.webDriver.FindElement(By.XPath("/html/body/section[1]"));
            highLihghter.Highlight(element, 1500);
            Thread.Sleep(5000);
        }

        [ClassCleanup]
        public static void CleanupAfterTestClassPackage()
        {
            driver?.Quit();
            var browserProcessCleaner = new BrowserProcessCleaner();
            browserProcessCleaner.CleanBrowserProcesses();
        }
    }
}
