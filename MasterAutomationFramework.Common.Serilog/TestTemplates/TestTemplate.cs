
namespace MasterAutomationFramework.Common.Serilog.TestTemplates
{
    using System;
    using MasterAutomationFramework.SeleniumAPI;
    using OpenQA.Selenium;
    using SeleniumAPI.Interfaces;

    public abstract class TestTemplate : ITestScenario, IExecuteTest
    {
        public SeleniumApi WebSeleniumApi { get; private set; }

        public SeriLogger Logger;


        public TestTemplate(IWebDriver driver, string testName)
        {
            this.WebSeleniumApi = new SeleniumApi(driver);
            this.Logger = new SeriLogger(testName);
        }

        /// <summary>
        /// Test prepare method
        /// </summary>
        public abstract void Prepare();

        /// <summary>
        /// Test run method
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// Test cleanup method
        /// </summary>
        public abstract void Clean();

        /// <summary>
        /// Executes the scenario.
        /// </summary>
        /// <returns>bool</returns>
        public bool ExecuteScenario()
        {
            bool testResult;

            try
            {
                this.Logger.Debug("Test Prepare - Started");
                this.Prepare();
                this.Logger.Debug("Test Prepare - Finished");

                this.Logger.Debug("Test Run - Started");
                this.Run();
                this.Logger.Debug("Test Run - Finished");
                testResult = true;
            }
            catch (Exception exception)
            {
                testResult = false;
                this.Logger.Fatal($"Test Failed.\n{exception}");

            }
            finally
            {
                this.Logger.Debug("Test Cleanup - Started");
                this.Clean();
                this.Logger.Debug("Test Cleanup - Finished");
                this.Logger.Dispose();
            }

            this.Logger.SaveResult(testResult);
            return testResult;
        }
    }
}
