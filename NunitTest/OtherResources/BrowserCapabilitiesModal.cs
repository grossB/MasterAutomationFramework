using NunitTest.Configuration_ThirdOption;
using Selenium.Essentials;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NunitTest.NewFolder
{
    public class BrowserCapabilitiesModal
    {
        public string CapabilityName { get; set; }
        public string BrowserName { get; set; }
        public string Platform { get; set; }
        public string Version { get; set; }
        public string ScreenResolution { get; set; }
        public string AppiumVersion { get; set; }
        public string DeviceName { get; set; }
        public string DeviceOrientation { get; set; }
        public string PlatformVersion { get; set; }
        public string PlatformName { get; set; }

        public Dictionary<string, string> ToCustomDictionary()
        {
            var resultCollection = new Dictionary<string, string>();
            if (BrowserName.HasValue())
            {
                resultCollection.Add("browserName", BrowserName);
            }

            if (Platform.HasValue())
            {
                resultCollection.Add("platform", Platform);
            }

            if (Version.HasValue())
            {
                resultCollection.Add("version", Version);
            }

            if (ScreenResolution.HasValue())
            {
                resultCollection.Add("screenResolution", ScreenResolution);
            }

            if (AppiumVersion.HasValue())
            {
                resultCollection.Add("appiumVersion", AppiumVersion);
            }

            if (DeviceName.HasValue())
            {
                resultCollection.Add("deviceName", DeviceName);
            }

            if (DeviceOrientation.HasValue())
            {
                resultCollection.Add("deviceOrientation", DeviceOrientation);
            }

            if (PlatformVersion.HasValue())
            {
                resultCollection.Add("platformVersion", PlatformVersion);
            }

            if (PlatformName.HasValue())
            {
                resultCollection.Add("platformName", PlatformName);
            }

            return resultCollection;
        }

        private static List<BrowserCapabilitiesModal> _browserCapabilitiesModals;
        internal static List<BrowserCapabilitiesModal> CurrentBrowserCapabilities
        {
            get
            {
                if (_browserCapabilitiesModals == null)
                {
                    _browserCapabilitiesModals = new List<BrowserCapabilitiesModal>();

                    var capFiles = Directory.EnumerateFiles(StorageHelper.GetAbsolutePath(JsonFileConfiguration.EnvData["PathToBrowserCapabilities"]), "*.json");

                    if (capFiles.Any())
                    {
                        _browserCapabilitiesModals.AddRange(
                            capFiles.SelectMany(f =>
                                SerializationHelper.DeSerializeFromJsonFile<List<BrowserCapabilitiesModal>>(f)).ToList()
                        );
                    }
                }
                return _browserCapabilitiesModals;
            }
        }
    }
}
