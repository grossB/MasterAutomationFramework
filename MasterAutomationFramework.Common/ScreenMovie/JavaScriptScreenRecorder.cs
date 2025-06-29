using MasterAutomationFramework.Common.ConfigurationHelper;
using MasterAutomationFramework.Common.Helper;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterAutomationFramework.Common.ScreenMovie
{

    // Usage
    // File Will be saved in ConfigHelper.RecordVideosPath file path
    public class Usage
    {
        public void StartRecording()
        {
            if (ConfigHelper.RecordScenarioExecution)
            {
                JavaScriptScreenRecorder.StartRecording();
            }
        }

        public void StopRecording()
        {
            JavaScriptScreenRecorder.StopRecording($"ScenarioTitle_{DateTime.UtcNow:yyy-MM-dd-HH-mm-ss-fffff}");
        }
    }


    public static class JavaScriptScreenRecorder
    {
        public static IWebDriver driver { get; set; }

        public static void StartRecording()
        {
            var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\TestData\\JsScript\\ScenarioRecordScript.js";
            var recordScript = File.ReadAllText(filePath);

            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Url = "https://www.google.com/";

            ((IJavaScriptExecutor)driver).ExecuteScript($"{recordScript}");
            driver.SwitchTo().Window(driver.WindowHandles.First());
        }

        public static void StopRecording(string fileName)
        {
            var recordingsDirectoryPath = ConfigHelper.RecordVideosPath;
            var handles = driver.WindowHandles;
            driver.SwitchTo().Window(driver.WindowHandles.Last());

            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("window.mediaRecorder.stop();");
                var fileNameAlert = WaitUntilAlertDisplayed();
                fileNameAlert.SendKeys(fileName);
                fileNameAlert.Accept();

                if (Directory.Exists(recordingsDirectoryPath))
                {
                    Directory.CreateDirectory(recordingsDirectoryPath);
                }

                RetryActionHelper.WaitUntil(() => { return File.Exists($@"{recordingsDirectoryPath}\{fileName}.webm"); }, 15);
            }
            catch (Exception)
            {
            }
        }

        private static IAlert WaitUntilAlertDisplayed()
        {
            return RetryActionHelper.WaitUntil(
                driverr =>
                {
                    var alert = driver.SwitchTo().Alert();

                    if (alert != null)
                    {
                        return alert;
                    }

                    throw new Exception("No alert yet");
                },
                30,
                ignoreExceptionTypes: new[] { typeof(Exception) });
        }
    }
}
