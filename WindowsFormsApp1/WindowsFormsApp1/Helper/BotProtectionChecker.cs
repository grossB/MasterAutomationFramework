using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Main;
using WindowsFormsApp1.Pages;

namespace WindowsFormsApp1.Helper
{
    public static class BotProtectionChecker
    {
        public static void CheckForAntiBotCaptcha()
        {
            var areYouHumanFrame = ElementsHelper.FindElement(By.XPath("//*[@id='bot_check']//iframe"), timeout: 300);
            //var areYouHumanFrame2 = SeleniumDriver.WebDriver.FindElement(By.XPath("//*[@id='bot_check']//iframe"));

            if (areYouHumanFrame != null)
            {
                Console.WriteLine("Sikuli WINDOW MOUSE CLICK");
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = @"C:\Games\01_Sikulli\q1_sikuli.jar";
                process.Start();
                System.Threading.Thread.Sleep(15000);

                Screenshot ss = ((ITakesScreenshot)SeleniumDriver.WebDriver).GetScreenshot();
                ss.SaveAsFile($"C://Image{new Random().Next()}.png", ScreenshotImageFormat.Png);

                //SeleniumDriver.WebDriver.SwitchTo().Frame(areYouHumanFrame);
                //By captcha = By.Id("anchor-state");
                //ReadOnlyCollection<IWebElement> captchas = SeleniumDriver.WebDriver.FindElements(captcha);
                //if (captchas.Count != 0)
                //{
                //    SeleniumDriver.WebDriver.FindElement(By.XPath("//*[@id='checkbox']")).Click();
                //    ElementsHelper.WaitForElementNotExist(captchas.First(), TimeSpan.FromSeconds(6));
                //    SeleniumDriver.WebDriver.SwitchTo().DefaultContent();
                //}
            }
        }
    }
}
