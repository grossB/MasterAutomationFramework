namespace MasterAutomationFramework.SimpleFramework
{
    using MasterAutomationFramework.T4GenericTemplates.GeneratedClasses;
    using MasterAutomationFramework.Common.Serilog.TestTemplates;
    using OpenQA.Selenium;

    public class FirstTest : TestTemplate
    {
        public FirstTest(IWebDriver driver) : base(driver, nameof(FirstTest))
        {
        }

        /// <summary>
        /// Test prepare method
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Prepare()
        {
            this.Logger.Debug("Opem page: https://www.google.com/");

            this.WebSeleniumApi.Driver.Url = "https://www.google.com/";
        }

        /// <summary>
        /// Test run method
        /// </summary>
        public override void Run()
        {
            var googlePage = new GooglePage(this.WebSeleniumApi);
            googlePage.Open();
            googlePage.SearchInputElement.SendKeys("sdsd");

            var searchInput = this.WebSeleniumApi.Driver.FindElement(By.XPath("//*[@id='tsf']/div[2]/div[1]/div[1]/div/div[2]/input"));
            searchInput.SendKeys("ggg");
        }

        /// <summary>
        /// Test cleanup method
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Clean()
        {
        }
    }
}
