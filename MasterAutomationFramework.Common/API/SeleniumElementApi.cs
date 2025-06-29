namespace MasterAutomationFramework.SeleniumAPI
{
    using Elements;
    using OpenQA.Selenium;

    public class SeleniumElementApi
    {
        /// <summary>
        /// Gets or set web Driver.
        /// </summary>
        public IWebDriver Driver { get; private set; }

        /// <summary>
        /// Gets or sets the java script executor.
        /// </summary>
        /// <value>
        /// The java script executor.
        /// </value>
        public JsExecutor JavaScriptExecutor { get; set; }

        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements API.
        /// </value>
        public ElementsWaitConditions Elements { get; set; }

        /// <summary>
        /// Gets or sets the visual comparer.
        /// </summary>
        /// <value>
        /// The visual comparer.
        /// </value>
        public VisualComparer VisualComparer { get; set; }

        /// <summary>
        /// Gets or sets the screen shot manager.
        /// </summary>
        /// <value>
        /// The screen shot manager.
        /// </value>
        public ScreenShotManager ScreenShotManager { get; set; }


        public SeleniumElementApi(IWebDriver driver)
        {
            this.Driver = driver;
            this.Elements = new ElementsWaitConditions(driver);
            this.JavaScriptExecutor = new JsExecutor(driver);
            this.VisualComparer = new VisualComparer();
            this.ScreenShotManager = new ScreenShotManager(driver);
        }
    }
}

