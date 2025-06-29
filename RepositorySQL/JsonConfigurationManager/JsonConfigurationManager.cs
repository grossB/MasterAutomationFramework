using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;

namespace RepositorySQL.JsonConfigurationManager
{
    /// <summary>
    /// Object to handle Json File configuration pass for test execution maintenance purpose.
    /// </summary>
    public static class JsonConfigurationManager
    {
        /// <summary>
        /// Convert json content to dictionary object.
        /// </summary>
        /// <param name="content">json file content</param>
        /// <returns><![CDATA[<Dictionary<string, string>]]></returns>
        public static Dictionary<string, string> JsonToDictionary(string content) => _JsonReadToDict(content);

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
                    throw new NullReferenceException("Json dictionary has not been initialized. Load json file before usage.");
                }

                return _envData;
            }
        }

        public static void JsonObjectSerilizer(object obj, string pathToSaveFile = @"c:\testConfig.json")
        {
            File.WriteAllText(pathToSaveFile, JsonConvert.SerializeObject(obj));

            using (StreamWriter file = File.CreateText(pathToSaveFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, obj);
            }
        }

        public static T DeserializeConfigurationModel<T>(string pathToFile)
        {
            TryToLoadJsonCollectionDictionary(pathToFile);
            return JsonConvert.DeserializeObject<T>(new PayloadDataJson(pathToFile).FileContent);
        }

        public static T DeserializeConfigurationModel<T>()
        {
            const string FolderName = "Properties";
            const string DefaultJsonConfigFile = "testConfig.json";
            string assemblyExecutionFolderLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var configurationFilePath = Path.Combine(assemblyExecutionFolderLocation, FolderName, DefaultJsonConfigFile);
            TryToLoadJsonCollectionDictionary(configurationFilePath);

            return JsonConvert.DeserializeObject<T>(new PayloadDataJson(configurationFilePath).FileContent);
        }

        #region Json Content Reader - Private Methods
        private static void TryToLoadJsonCollectionDictionary(string pathToFile)
        {
            try
            {
                if (File.Exists(pathToFile))
                {
                    _envData = JsonToDictionary(new PayloadDataJson(pathToFile).FileContent);
                }
            }
            catch (Exception e)
            {
                // TODO implement logger info
            }
        }

        private static Dictionary<string, string> _JsonInnerCollectionPropertyNameDuplicateNumberingLogic(Dictionary<string, string> primaryDictionary,
            Dictionary<string, string> innerCollectionDictionary,
            JProperty currentProperty,
            string parentPropertyName)
        {
            if (innerCollectionDictionary.Any())
            {
                innerCollectionDictionary.Iter(d =>
                {
                    var calculatedKey = _CalculateJsonKeyPropertyName(parentPropertyName, d.Key);
                    if (primaryDictionary.ContainsKey(calculatedKey))
                    {
                        calculatedKey = $"{calculatedKey}_{Guid.NewGuid()}";
                    }
                    primaryDictionary.Add(calculatedKey, d.Value);
                });
            }
            else
            {
                if (currentProperty != null)
                {
                    var calculatedKey = _CalculateJsonKeyPropertyName(parentPropertyName, currentProperty.Name);
                    if (primaryDictionary.ContainsKey(calculatedKey))
                    {
                        calculatedKey = $"{calculatedKey}_{Guid.NewGuid()}";
                    }
                    primaryDictionary.Add(calculatedKey, currentProperty?.Value.ToString());
                }
            }
            return primaryDictionary;
        }

        private static string _CalculateJsonKeyPropertyName(string parent, string currentPropertyName)
        {
            return string.IsNullOrEmpty(parent) || string.IsNullOrWhiteSpace(parent) ? currentPropertyName : $"{parent} - {currentPropertyName}";
        }

        private static Dictionary<string, string> _JsonReadToDict(string content, string parent = "")
        {
            var dicCollector = new Dictionary<string, string>();
            try
            {
                if (content.Trim().StartsWith("[") && content.Trim().EndsWith("]"))
                {
                    var jarr = JArray.Parse(content);
                    jarr.Children().OfType<JObject>().Iter(c =>
                    {
                        var inner = _JsonReadToDict(c.ToString(), parent);
                        dicCollector = _JsonInnerCollectionPropertyNameDuplicateNumberingLogic(dicCollector, inner, null, parent);
                    });
                }
                else
                {
                    var jobj = JObject.Parse(content);
                    jobj.Children()
                        .OfType<JProperty>()
                        .Iter(jProp =>
                        {
                            if (jProp.Value.ToString().Contains("{"))
                            {
                                //Read all inner properties
                                var innerProps = _JsonReadToDict(jProp.Value.ToString(), jProp.Name);
                                dicCollector = _JsonInnerCollectionPropertyNameDuplicateNumberingLogic(dicCollector, innerProps, jProp, parent);
                            }
                            else if (jProp.Value.ToString().Trim().StartsWith("[") && jProp.Value.ToString().Trim().EndsWith("]"))
                            {
                                dicCollector.Add(_CalculateJsonKeyPropertyName(parent, jProp.Name),
                                            string.Join(",", jProp
                                                .Value
                                                .ToString()
                                                .SplitAndTrim(",")
                                                .Select(s => s.ReplaceMultiple(string.Empty,
                                                                    Environment.NewLine,
                                                                    "[",
                                                                    "]",
                                                                    "\"").Trim())));
                            }
                            else
                            {
                                dicCollector.Add(_CalculateJsonKeyPropertyName(parent, jProp.Name), jProp.Value.ToString());
                            }
                        });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dicCollector;
        }

        #endregion
    }

    public class PayloadDataJson
    {
        /// <summary>
        /// Relative path to the json file
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Content of the json file, which will be loaded during initialization
        /// </summary>
        public string FileContent { get; private set; }

        /// <summary>
        /// Read the json file and exposed through FileContent property
        /// </summary>
        /// <param name="filePath">path to the json file (relative path)</param>
        public PayloadDataJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                FilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filePath);
            }
            else
            {
                FilePath = filePath;
            }

            try
            {
                FileContent = File.ReadAllText(FilePath);
            }
            catch (Exception exception)
            {
                throw new FileNotFoundException($"The Json file trying to load is unavailable in the location { FilePath }.\n {exception}");
            }
        }
    }

    public static class CollectionExtension
    {
        public static void Iter<TItem>(this IEnumerable<TItem> items, Action<TItem> action)
        {
            items.Iteri((item, _) => action(item));
        }

        public static void Iteri<TItem>(this IEnumerable<TItem> items, Action<TItem, int> action)
        {
            var index = 0;
            foreach (var item in items.EmptyIfNull())
                action(item, index++);
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> items) => items ?? Enumerable.Empty<T>();
    }

    public static class StringExtension
    {
        public static bool HasValue(this string value) => !IsEmpty(value);

        public static bool IsEmpty(this string text) => string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);

        public static IEnumerable<string> SplitAndTrim(this string value, string splitString) =>
            value.Split(new[] { splitString }, StringSplitOptions.RemoveEmptyEntries)
            .Select(d => d.Trim())
            .Where(d => d.HasValue());

        public static string ReplaceMultiple(this string value, string replaceWith, params string[] replaceContents)
        {
            replaceContents.Iter(r => value = value.Replace(r, replaceWith));
            return value;
        }
    }
}
