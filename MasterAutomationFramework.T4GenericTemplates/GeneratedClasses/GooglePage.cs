using MasterAutomationFramework.T4MasterAutomationFramework.T4GenericTemplates.Pages.Google;
using OpenQA.Selenium;
using SelApi = MasterAutomationFramework.SeleniumAPI.SeleniumElementApi;

namespace MasterAutomationFramework.T4GenericTemplates.GeneratedClasses
{
    public class GooglePage : GooglePageBy
    {
        protected SelApi Api;

        protected override IWebDriver WebDriver => this.Api.Driver;

        public GooglePage(SelApi api)
        {
            this.Api = api;
        }
        public override string UrlAdress => throw new System.NotImplementedException();

        public override void Open()
        {
            throw new System.NotImplementedException();
        }
    }
}
