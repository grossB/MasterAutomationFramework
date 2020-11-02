namespace MasterAutomationFramework.SeleniumAPI.Interfaces
{
    public interface ITestScenario
    {
        #region Public Methods

        /// <summary>
        /// Cleanup after test
        /// </summary>
        void Clean();

        /// <summary>
        /// Test prepare
        /// </summary>
        void Prepare();

        /// <summary>
        /// Test main execution
        /// </summary>
        void Run();

        #endregion
    }
}
