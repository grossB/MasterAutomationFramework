using MasterAutomationFramework.SeleniumAPI._00_Functionality._01_AttachToChromeProcess;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Main
{
    public class SeleniumDriver
    {
        private static IWebDriver driver = null;

        public static IWebDriver WebDriver { get
            {
                if (driver == null)
                {
                    var AttachToChromeManager = new AttachToChrome2();
                    driver = AttachToChromeManager.AttachToProcess();
                }

                return driver;
            }
         }

        //public static void SetDriver(IWebDriver driver)
        //{
        //    //WebDriver = driver;
        //}

        public static WebDriverWait Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10));
    }
}
