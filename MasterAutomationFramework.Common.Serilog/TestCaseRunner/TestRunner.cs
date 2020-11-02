namespace MasterAutomationFramework.Common.Serilog.TestCaseRunner
{
    using MasterAutomationFramework.SeleniumAPI.Interfaces;

    public static class TestRunner
    {
        public static bool Run(IExecuteTest test)
        {
            return test.ExecuteScenario();
        }
    }
}
