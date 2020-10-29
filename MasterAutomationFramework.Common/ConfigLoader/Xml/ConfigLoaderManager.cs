using System.IO;
using System.Xml.Serialization;

namespace MasterAutomationFramework.Common.ConfigLoader
{

    /// <summary>
    /// Used to - serialize & deserialize config object.
    /// </summary>
    static public class ConfigLoaderManager
    {
        public static void SaveConfig<T>(this T configToSave, string configFilePath = @"C:\XmlConfig.xml") where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SimpleConfig));
            using (TextWriter writer = new StreamWriter(configFilePath))
            {
                serializer.Serialize(writer, configToSave);
            }
        }

        public static T LoadConfig<T>(string configFilePath = @"C:\XmlConfig.xml") where T : class
        {
            T loadedConfig;

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var fileStream = File.OpenRead(configFilePath))
            {
                loadedConfig = (T)serializer.Deserialize(fileStream);
            }

            return loadedConfig;
        }
    }
}
