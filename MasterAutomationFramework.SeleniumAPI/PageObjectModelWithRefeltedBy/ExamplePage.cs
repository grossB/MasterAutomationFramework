namespace MasterAutomationFramework.SeleniumAPI.PageObjectModelWithRefeltedBy
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using System.ComponentModel;

    public class ExamplePage
    {
        [FindsBy(How = How.Id, Using = "account")]
        public IWebElement MyAccount { get; set; }

        [FindsBy(How = How.Id, Using = "account2")]
        public IWebElement MyAccount2 { get; set; }

        [Description("")]
        public IWebElement MyAccountOther { get; set; }
    }
}
