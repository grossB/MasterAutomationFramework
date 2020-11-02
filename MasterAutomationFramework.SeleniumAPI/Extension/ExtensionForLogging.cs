namespace MasterAutomationFramework.SeleniumAPI.API.Drivers
{
    using AventStack.ExtentReports;
    using System;

    public static class ExtensionForLogging
    {
        public static void ExecuteSimpleAssertion(this ExtentTest extentTest, Action assertion, string nodeDescription)
        {
            bool result = true;
            var nodeLEvel = extentTest.CreateNode(nodeDescription);

            try
            {
                assertion.Invoke();
            }
            catch (Exception)
            {
                result = false;
            }

            if (result)
                nodeLEvel.Pass("Passed");
            else
                nodeLEvel.Fail("Failed");
        }

        public static void ExecuteAssertion(this ExtentTest extentTest, Func<ExtentTest, bool> assertion, string nodeDescription)
        {
            bool result = true;
            var nodeLEvel = extentTest.CreateNode(nodeDescription);

            try
            {
                result = assertion.Invoke(nodeLEvel);
            }
            catch (Exception)
            {
                result = false;
            }

            if (result)
                nodeLEvel.Pass("Passed");
            else
                nodeLEvel.Fail("Failed");
        }
    }
}

