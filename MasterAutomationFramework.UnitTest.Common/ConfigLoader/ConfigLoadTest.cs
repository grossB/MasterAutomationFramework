using MasterAutomationFramework.Common.ConfigLoader.ProjectConfig;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace MasterAutomationFramework.Common.ConfigLoader.Tests
{
    [TestClass()]
    public class ConfigLoadTest
    {
        const string configFilePath = @"C:\XmlConfig.xml";

        [TestInitialize]
        public void SaveConfigTest()
        {
            new SimpleConfig() { StringValue = "abd" }.SaveConfig(configFilePath);
        }

        [TestMethod()]
        public void LoadConfigTest()
        {
            var loadedConfig = ConfigLoaderManager.LoadConfig<SimpleConfig>(configFilePath);

            Assert.AreEqual("abd", loadedConfig.StringValue, "StringValue");
            Assert.AreEqual(0, loadedConfig.IntValue, "IntValue");
            Assert.AreEqual(5, loadedConfig.IntValueWithDefault, "IntWithDefaultValue");
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (File.Exists(configFilePath))
            {
                File.Delete(configFilePath);
            }
        }
    }
}