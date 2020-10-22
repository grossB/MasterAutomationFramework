using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MasterAutomationFramework.Common.Extension;

namespace UnitTestProject1.CollectionExtension
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ExtensionCollectionTest
    {
        [TestMethod]
        public void FindElementInCollection_Pass()
        {
            var list =  new List<int> { 1, 2, 3, 4 }.Select(x => new { Value = x, StringValue = x.ToString() });
            Assert.AreEqual(new { Value =  4, StringValue = "4" }, list.FindElement(x => x.StringValue, "4"));
        }
    }
}
