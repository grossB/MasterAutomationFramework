using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Common
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConfigurationProvideTheConfigurationValuesUsingAttributes()
        {
            string configFilePath = @"C:\XmlConfig.xml";
            var defaultConfig = new DummyConfigurationWithAttributes() { StringValue = "abd" };
            var loadedConfig = new DummyConfigurationWithAttributes();

            XmlSerializer serializer = new XmlSerializer(typeof(DummyConfigurationWithAttributes));
            using (TextWriter writer = new StreamWriter(configFilePath))
            {
                serializer.Serialize(writer, defaultConfig);
            }

            using (var fileStream = File.OpenRead(configFilePath))
            {
                loadedConfig = (DummyConfigurationWithAttributes)serializer.Deserialize(fileStream);
            }


            Assert.AreEqual("abd", loadedConfig.StringValue, "StringValue");
            Assert.AreEqual(0, loadedConfig.IntValue, "IntValue");
            Assert.AreEqual(5, loadedConfig.IntValueWithDefault, "IntWithDefaultValue");

        }
    }
}
