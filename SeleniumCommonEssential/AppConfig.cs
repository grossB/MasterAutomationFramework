//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Diagnostics;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using NunitTest.Stuff;

//namespace SeleniumCommonEssential
//{
//    /// <summary>
//    /// Application configuration information
//    /// </summary>
//    public static class AppConfig
//    {
//        /// <summary>
//        /// Load the assembly configuration based on the assembly name
//        /// </summary>
//        /// <param name="name">name of the assembly to load the configuration</param>
//        /// <returns>configuration information of the assembly</returns>
//        public static Configuration LoadConfiguration(string name)
//        {
//            return LoadConfiguration(Runtime.GetAssembly(name));
//        }

//        /// <summary>
//        /// Load the assembly configuration based on the assembly
//        /// </summary>
//        /// <param name="assembly">Assembly to load the configuration</param>
//        /// <returns></returns>
//        public static Configuration LoadConfiguration(Assembly assembly)
//        {
//            if (assembly != null)
//            {
//                return ConfigurationManager.OpenExeConfiguration(assembly.Location);
//            }
//            return null;
//        }

//        /// <summary>
//        /// Read the AppSettings from the assembly configuration
//        /// </summary>
//        /// <param name="assemblyName">Assembly name to load the configuration</param>
//        /// <returns>Dictinary with key value pair of the AppSettings</returns>
//        public static Dictionary<string, string> ReadAppSettings(string assemblyName) => ReadAppSettings(Runtime.GetAssembly(assemblyName));
//        public static Dictionary<string, string> ReadAppSettings(Assembly assembly)
//        {
//            var dataDict = new Dictionary<string, string>();

//            var customAssesmblyConfig = LoadConfiguration(assembly);
//            customAssesmblyConfig?.AppSettings.Settings.AllKeys.Iter(k =>
//            {
//                Utility.Runtime.Logger.Log($"Reading AppSettings from custom project level[{assembly.ManifestModule.Name}] : Key: {k}, Value: {customAssesmblyConfig.AppSettings.Settings[k].Value}");
//                dataDict.AddOrUpdate(k, customAssesmblyConfig.AppSettings.Settings[k].Value);
//            });
//            return dataDict;
//        }

//        private static Configuration _configurationCaller;

//        /// <summary>
//        /// Configuration of the caller assembly
//        /// </summary>
//        public static Configuration ConfigurationCaller
//        {
//            get
//            {
//                if (_configurationCaller == null)
//                {
//                    _configurationCaller = LoadConfiguration(Runtime.CallingAssembly);
//                }
//                return _configurationCaller;
//            }
//        }

//        private static Dictionary<string, string> _appsettingsCallerAssembly;

//        /// <summary>
//        /// AppSettings of the caller assembly
//        /// </summary>
//        public static Dictionary<string, string> AppSettingsCallerAssembly
//        {
//            get
//            {
//                if (_appsettingsCallerAssembly == null)
//                {
//                    _appsettingsCallerAssembly = ReadAppSettings(Runtime.CallingAssembly);
//                }
//                return _appsettingsCallerAssembly;
//            }
//        }
//    }
//}
