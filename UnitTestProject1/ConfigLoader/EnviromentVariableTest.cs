using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MasterAutomationFramework.Common.EnviromentVariablesReader;

namespace UnitTestProject1.NewFolder1
{
    /// <summary>
    /// Summary description for EnviromentVariableTest
    /// </summary>
    [TestClass]
    public class EnviromentVariableTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string TestEnviromentVariable = "TestVariable";
            string TestValue = "TestValue";
            EnviromentVariableManager.SetEnviromentVariable(TestEnviromentVariable, TestValue);
            var currentValue = EnviromentVariableManager.GetEnviromentVariable(TestEnviromentVariable);
            Assert.AreEqual(TestValue, currentValue);
        }
    }
}
