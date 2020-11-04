using FluentAssertions;
using NunitTest.Configuration_ThirdOption;
using OpenQA.Selenium;
using Selenium.Essentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NunitTest.OtherResources
{
    public class MainPage 
    {
        protected IWebDriver _driver;

        public MainPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate()
        {
            _driver.Navigate().GoToUrl("www.");
            _driver.WaitTillPageLoad();
        }

        private string _path => $"{JsonFileConfiguration.EnvData["WikipediaDomain"]}/wiki/Main_Page";
        

        private UnorderedListControl _tabNavigation => new UnorderedListControl(_driver, By.CssSelector("nav[id$='p-namespaces'] ul.vector-menu-content-list"));
        private TableControl _tableMainContent => new TableControl(_driver, By.Id("mp-upper"));

        public void SelectMainPageTab(string tabText)
        {
            _tabNavigation.TotalItems
                .Should()
                .BeGreaterThan(0, "The tab on the wikipedia main page is missing");

            _tabNavigation.List
                .Any(p => p.Text.Contains(tabText))
                .Should()
                .BeTrue($"The Wikipedia main page is missing with a tab text '{tabText}'");

            _tabNavigation.List
                .Where(p => p.Text.Contains(tabText))
                .FirstOrDefault()
                .Click();
        }
    }
}
