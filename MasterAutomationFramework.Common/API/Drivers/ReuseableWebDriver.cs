namespace MasterAutomationFramework.SeleniumAPI.API.Drivers
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public class ReuseableWebDriver : RemoteWebDriver
    {
        private static Uri _localUri;
        private static string _localSesionIdValue;
        private string _sessionId;
        public const string PathToSaveDriver = "C:\\Driver_SessionKey.txt";

        #region Private Properties
        private static Uri LocalUri
        {
            get
            {
                if (_localUri == null)
                {
                    var savedProcessData = File.ReadAllText(PathToSaveDriver);
                    _localSesionIdValue = savedProcessData.Substring(savedProcessData.IndexOf(' ') + 1);
                    _localUri = new Uri(savedProcessData.Substring(0, savedProcessData.IndexOf(' ')));
                }

                return _localUri;
            }
            set => _localUri = value;
        }

        private static string LocalSesionId
        {
            get
            {
                if (string.IsNullOrEmpty(_localSesionIdValue))
                {
                    var savedProcessData = File.ReadAllText(PathToSaveDriver);
                    _localSesionIdValue = savedProcessData.Substring(savedProcessData.IndexOf(' ') + 1);
                    _localUri = new Uri(savedProcessData.Substring(0, savedProcessData.IndexOf(' ')));
                }

                return _localSesionIdValue;
            }
            set => _localSesionIdValue = value;
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ReuseRemoteWebDriver"/> class.
        /// </summary>
        public ReuseableWebDriver() : base(LocalUri, new DesiredCapabilities())
        {
            this._sessionId = LocalSesionId;
            var sessionIdBase = this.GetType().BaseType.GetField("sessionId", BindingFlags.Instance | BindingFlags.NonPublic);
            sessionIdBase.SetValue(this, new SessionId(LocalSesionId));
        }

        #endregion

        protected override Response Execute(string driverCommandToExecute, Dictionary<string, object> parameters)
        {
            if (driverCommandToExecute == DriverCommand.NewSession)
            {
                var resp = new Response();
                resp.Status = WebDriverResult.Success;
                resp.SessionId = this._sessionId;
                resp.Value = new Dictionary<string, object>();
                return resp;
            }

            this.PrintParameterInfo(driverCommandToExecute, parameters);

            Response respBase = base.Execute(driverCommandToExecute, parameters);
            return respBase;
        }

        public static void SaveSessionKey(IWebDriver driver)
        {
            var driverChrome = (RemoteWebDriver)driver;
            var remoteUri = GetExecutorUrlFromDriver(driver);
            if (File.Exists(PathToSaveDriver))
            {
                File.Delete(PathToSaveDriver);
            }
            File.WriteAllText(PathToSaveDriver, remoteUri + " " + driverChrome.SessionId);
        }

        #region Private Methods

        private void PrintParameterInfo(string driverCommandToExecute, Dictionary<string, object> parameters)
        {
            try
            {
                Console.WriteLine(driverCommandToExecute);
                foreach (var keyValuePair in parameters)
                {
                    // Logging Command To Execute parameters
                    Console.WriteLine($"Key: {keyValuePair.Key}  Value:{keyValuePair.Value}");
                }
            }
            catch (Exception) { }
        }

        private static Uri GetExecutorUrlFromDriver(IWebDriver driver)
        {
            var executorField = typeof(RemoteWebDriver).GetField("executor", BindingFlags.NonPublic | BindingFlags.Instance);
            object executor = executorField.GetValue((RemoteWebDriver)driver);
            var internalExecutorField = executor.GetType().GetField("internalExecutor", BindingFlags.NonPublic | BindingFlags.Instance);
            object internalExecutor = internalExecutorField.GetValue(executor);
            var remoteServerUriField = internalExecutor.GetType().GetField("remoteServerUri", BindingFlags.NonPublic | BindingFlags.Instance);
            var remoteServerUri = remoteServerUriField.GetValue(internalExecutor) as Uri;
            return remoteServerUri;
        }

        #endregion
    }
}

