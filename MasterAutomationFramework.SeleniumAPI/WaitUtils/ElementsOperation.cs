using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterAutomationFramework.SeleniumAPI.WaitUtils
{
    public class ElementsOperation
    {
        IWebDriver driver; 

        public ElementsOperation(IWebDriver webDriver)
        {
            this.driver = webDriver;
        }

        /// <summary>
        /// The default timeout that is used to wait for elements to appear or disappear
        /// </summary>
        public const int DefaultWaitTimeout = 30;

        /// <summary>
        /// Waits for the current element to disappear. That is, either become invisible or completely removed from the DOM
        /// </summary>
        /// <param name="timeout">Timeout to wait for the element to disappear</param>
        /// <exception cref="TimeoutException">The current element hasn't been disappeared for the specified period</exception>
        public void WaitToDisappear(TimeSpan timeout)
        {
            WebDriverWait Wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(20));
            //Wait.Until(driver => return Displayed);
        }
    }
}
