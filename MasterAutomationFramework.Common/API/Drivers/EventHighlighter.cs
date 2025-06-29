namespace MasterAutomationFramework.SeleniumAPI.Drivers
{
    using MasterAutomationFramework.SeleniumAPI.API.DriverFactory;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Events;
    using SeleniumAPI.Enums;
    using System;
    using System.ComponentModel;
    using System.Threading;

    public class EventHighlighter
    {
        public EventFiringWebDriver webDriver;
        private IJavaScriptExecutor _scriptExecutor;

        #region constructors

        public EventHighlighter()
        {
            this.webDriver = (EventFiringWebDriver)DriverFactoryEx.CreateDriver(BrowserType.EventFiringWebDriver);
            this._scriptExecutor = webDriver;
            webDriver.ElementClicking += new EventHandler<WebElementEventArgs>(firingDriver_ElementClicked);
        }

        public EventHighlighter(IWebDriver driver)
        {
            this.webDriver = new EventFiringWebDriver(driver);
            this._scriptExecutor = webDriver;
            webDriver.ElementClicking += new EventHandler<WebElementEventArgs>(firingDriver_ElementClicked);
        }

        #endregion

        private void firingDriver_ElementClicked(object sender, WebElementEventArgs e)
        {
            try
            {
                var backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += (obj, ee) => Unhighlight(e.Element, 2500);
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void Unhighlight(IWebElement nativeElement, int waitBeforeUnhighlightMiliSeconds)
        {
            try
            {
                var originalElementBorder = (string)_scriptExecutor.ExecuteScript("return arguments[0].style.backgroundColor", nativeElement);
                _scriptExecutor.ExecuteScript($"arguments[0].style.backgroundColor='yellow'; return;", nativeElement);
                Thread.Sleep(TimeSpan.FromMilliseconds(waitBeforeUnhighlightMiliSeconds));
                // Restore 
                _scriptExecutor.ExecuteScript("arguments[0].style.backgroundColor='" + originalElementBorder + "'; return;", nativeElement);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}
