using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using WindowsFormsApp1.Main;

namespace WindowsFormsApp1.Pages
{
    public class LoginPage
    {
        private const string GameWorldXpath = "//*[@id=\"home\"]//span[contains(text(), \"Świat {0}\")]";

        IWebDriver Driver;
        WebDriverWait Wait;

        #region constructor
        public LoginPage()
        {
            Wait = SeleniumDriver.Wait;
            Driver = SeleniumDriver.WebDriver;
        }

        #endregion

        #region Elements
        private IWebElement UserNameButton => Driver.FindElement(By.Id("user"));
        private IWebElement PasswordButton => Driver.FindElement(By.Id("password"));
        private IWebElement AlwaysLogInButton => ElementsHelper.FindElementSafe(By.Id("remember-me"));
        private IWebElement SubmitButton => ElementsHelper.FindElementSafe(By.XPath("//*[@id=\"login_form\"]//a[contains(text(), \"Logowanie\")]"));
        private IWebElement LogOutButton => ElementsHelper.FindElementSafe(By.XPath("//*[@id='home']//a[@href='/page/logout'][contains(text(),'Wylogowanie')]"));
        private IWebElement World144_Button => Driver.FindElement(By.XPath("//*[@id=\"home\"]//span[contains(text(), \"Świat 144\")]"));

        #region Other
        private IWebElement DailyBonus => Driver.FindElement(By.Id("popup_box_daily_bonus"));
        private IWebElement RegisterButton => Driver.FindElement(By.XPath("//*[@id=\"home\"]//a[contains(text(), \"Bezpłatna rejestracja!\")]"));
        private IWebElement RegisterUserNameInput => Driver.FindElement(By.Id("register_username"));
        private IWebElement RegisterPasswordInput => Driver.FindElement(By.Id("register_password"));
        private IWebElement RegisterEmailInput => Driver.FindElement(By.Id("register_email"));
        private IWebElement AcceptTermsCheckbox => Driver.FindElement(By.Id("terms"));
        private IWebElement ErrorMsgBox => Driver.FindElement(By.XPath("//*[@id=\"form-element-name\"]/div"));
        private IWebElement SubmitRegister => Driver.FindElement(By.XPath("//*[@id=\"register_form\"]//a[contains(text(), \"Stwórz konto\")]"));

        private IWebElement ErrorMsgBoxCustom => Driver.FindElement(By.XPath("//*[@id=\"form-element-name\"]/div"));

        private IWebElement JoinToWorldNow => ElementsHelper.FindElementSafe(By.XPath("//*[@id=\"join\"]//span[contains(text(), \"Dołącz teraz\")]"));
        private IWebElement Daily_bonus_content => ElementsHelper.FindElementSafe(By.XPath("//*[@id=\"daily_bonus_content\"]//a[contains(text(), \"Otwórz\")]"));
        private IWebElement MailAnnouncement => ElementsHelper.FindElementSafe(By.XPath("//*[@id=\"content_value\"]/a[2]"));

        #endregion

        #endregion

        #region Public Methods

        private void Relog()
        {
            LogOut();
            Open();
            Thread.Sleep(TimeSpan.FromSeconds(30));
            LogIn();
        }

        /// <summary>
        /// Temporary default login, password and world info.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="world"></param>
        public void LogIn(string username = "kornq123", string password = "Horrus", string world = "169")
        {
            try
            {
                LogOut();
            }
            catch (Exception e)
            {}

            //new Actions(Driver)
            //    .SendKeys(this.UserNameButton, username)
            //    .SendKeys(this.PasswordButton, password)
            //    .Build()
            //    .Perform();
            this.SubmitButton.Click();

            Wait.Until(x => null != this.LogOutButton);
            var gameWorldXpath = By.XPath(string.Format(GameWorldXpath, world));

            var moreWorlds = By.XPath("//*[@id=\"home\"]/div[3]/div[4]/div[10]/div[3]/div[2]/a");
            ElementsHelper.FindElementSafe(moreWorlds)?.Click();
            ElementsHelper.FindElementSafe(gameWorldXpath);
            Driver.FindElement(gameWorldXpath).Click();
        }

        private void Open()
        {
            Driver.Url = "https://www.plemiona.pl/";
        }

        #endregion

        public void LogOut()
        {
            Driver.Url = "https://www.plemiona.pl/page/logout";
            ElementsHelper.WaitForElementNotExist(this.LogOutButton);
        }
    }
}
