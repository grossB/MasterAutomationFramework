using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using Description = System.ComponentModel.DescriptionAttribute;
using MasterAutomationFramework.Common.Extension;

namespace MasterAutomationFramework.UnitTest.Common.EnumExtension
{
    [TestClass]
    public class EnumExtensionTest
    {
        #region Test Data
        private const string DescriptionEnum1 = "Test description 1";
        private const string DescriptionEnum2 = "Test description 2";
        private enum TestEnum
        {
            [Description(DescriptionEnum1)]
            Field1,

            [Description(DescriptionEnum2)]
            Field2,

            // When set:
            // Field3 = 0  is equivalent to Field1
            // Console result == "Test description 1"
            Field3
        }
        #endregion

        [TestMethod]
        public void GetDescriptionValueFromTheAttribute_Pass()
        {
            Assert.AreEqual(DescriptionEnum1, TestEnum.Field1.GetDescription());
            Assert.AreEqual(DescriptionEnum2, TestEnum.Field2.GetDescription());
        }

        [TestMethod]
        public void GetDescriptionIfTNoDescriptionAttribute_Pass()
        {
            Assert.AreEqual("Field3", TestEnum.Field3.GetDescription());
        }
    }
}
