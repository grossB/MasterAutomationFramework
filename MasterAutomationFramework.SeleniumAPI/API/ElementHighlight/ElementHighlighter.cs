namespace MasterAutomationFramework.SeleniumAPI.API.ElementHighlight
{
    using MasterAutomationFramework.SeleniumAPI.API.DriverFactory;
    using OpenQA.Selenium;
    using SeleniumAPI.Enums;
    using System;
    using System.ComponentModel;
    using System.Threading;

    public class ElementHighlighter
    {
        public IWebDriver webDriver;

        public IJavaScriptExecutor JavaScriptExecutor;

        public ElementHighlighter(IWebDriver driver = null)
        {
            webDriver = driver ?? DriverFactoryEx.CreateDriver(BrowserType.Chrome);
            JavaScriptExecutor = (IJavaScriptExecutor)webDriver;
        }

        public void Highlight(IWebElement nativeElement, int waitBeforeUnhighlightMiliSeconds = 500, string color = "yellow")
        {
            try
            {
                var originalElementBorder = (string)JavaScriptExecutor.ExecuteScript("return arguments[0].style.background", nativeElement);
                JavaScriptExecutor.ExecuteScript($"arguments[0].style.background='{color}'; return;", nativeElement);
                if (waitBeforeUnhighlightMiliSeconds >= 0)
                {
                    //if (waitBeforeUnhighlightMiliSeconds > 4000)
                    //{
                    var backgroundWorker = new BackgroundWorker();
                    backgroundWorker.DoWork += (obj, e) => Unhighlight(nativeElement, originalElementBorder, waitBeforeUnhighlightMiliSeconds);
                    backgroundWorker.RunWorkerAsync();
                    //}
                    //else
                    //{
                    //    Unhighlight(nativeElement, originalElementBorder, waitBeforeUnhighlightMiliSeconds);
                    //}
                    //for (int i = 0; i < 100000; i++)
                    //{
                    //    Console.WriteLine("sa");
                    //}
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void Unhighlight(IWebElement nativeElement, string border, int waitBeforeUnhighlightMiliSeconds)
        {
            try
            {
                JavaScriptExecutor.ExecuteScript($"arguments[0].style.background='yellow'; return;", nativeElement);
                Thread.Sleep(TimeSpan.FromSeconds(2));
                // Restore 
                JavaScriptExecutor.ExecuteScript("arguments[0].style.background='" + border + "'; return;", nativeElement);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
