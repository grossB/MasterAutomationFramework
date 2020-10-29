using Selenium.Essentials;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NunitTest.Configuration_ThirdOption
{
    public class JsonFileConfiguration
    {
        private static Dictionary<string, string> _envData;

        /// <summary>
        /// Loads the environment information from the json and stores as Dictionary
        /// </summary>
        public static Dictionary<string, string> EnvData
        {
            get
            {
                if (_envData == null)
                {
                    var envDataFilePath = Path.Combine(Utility.Runtime.ExecutingFolder, "EnvironmentData", "EnvData.json");
                    if (File.Exists(envDataFilePath))
                    {
                        _envData = SerializationHelper.JsonToDictionary(new PayloadDataJsonAttribute(envDataFilePath).FileContent);
                    }
                    else
                    {
                        _envData = new Dictionary<string, string>();
                    }
                }
                return _envData;
            }
        }
    }
}
