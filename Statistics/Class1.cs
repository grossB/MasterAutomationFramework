using MasterAutomationFramework.SeleniumAPI.API.Drivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KnightOnlinePopulationStatistics
{
    public class Class1
    {

        public IWebDriver Driver;

        public Class1()
        {
            Driver = DriverFactoryWithOptions.BuildWebDriver("Chrome");
        }

        public void StartChrome()
        {
            Driver.Navigate().GoToUrl("https://ko-myko.com/");
            var page = new KoMykoPage(Driver);

            while (true)
            {
                page.GetUserAmount();
                File.AppendAllText(@"C:\ZZ_ Smietnik\Knight Statistics.txt", $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}    {page.GetUserAmount()}" + Environment.NewLine);
                Task.Delay(TimeSpan.FromMinutes(2));
            }
        }
    }

    public class KoMykoPage
    {
        readonly IWebDriver Driver;

        public KoMykoPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebElement KarusOnlineUser => Driver.FindElement(By.XPath("//*/span[contains(text(),'Karus users online')]"));

        public IWebElement ElmoradOnlineUser => Driver.FindElement(By.XPath("//*/span[contains(text(),'Elmorad users online')]"));

        public string GetUserAmount()
        {
            Driver.Navigate().Refresh();

            try
            {
                return KarusOnlineUser.Text + ElmoradOnlineUser.Text;
            }
            catch (Exception e)
            {
                return $"error {e}";
            }
        }
    }
}
