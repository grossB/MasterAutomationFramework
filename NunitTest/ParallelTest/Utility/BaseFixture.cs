using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NunitTest.ParallelTest.Utility
{
    public class BaseFixture
    {
        [OneTimeSetUp]
        public void Setup()
        {
            //Uncomment it for TestScope Logging with each testMethod as a parent node
            //ExtentTestManager.CreateParentTest(GetType().Name);
        }

        [OneTimeTearDown]
        protected void TearDown()
        {
            AventStack.ExtentReports.ExtentReports Reporter = new AventStack.ExtentReports.ExtentReports();
            ExtentManager.Instance.Flush();
        }

        [SetUp]
        public void BeforeTest()
        {
            //ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.MethodName);
            var className = TestContext.CurrentContext.Test.ClassName;
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name, className.Substring(className.LastIndexOf(".")));
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            ExtentTestManager.GetTest().Log(logstatus, "Test ended with " + logstatus + stacktrace);
        }
    }
}

