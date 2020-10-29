using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NunitTest.DriverFactory;
using NunitTest.NewFolder;
using NunitTest.OtherResources;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Tests
{
    [TestFixture]
    public class RunMultipleScenarioByConfiguration
    {
        protected IWebDriver _driver;

        // Second parameter is for method name.
        [TestCaseSource(typeof(CaseCommonDataSource), "BrowserCapabilities")]
        public void NavigateToWikipedia(string browserType)
        {
            var _wikiMainPage = new MainPage(ManyDriverFactory.InitializeDriver(browserType));
        }

        [OneTimeTearDown]
        public static void Cleanup()
        {
            foreach (var driver in ManyDriverFactory.SessionDrivers)
            {
                driver.Value.Close();
            }
        }
    }
}