using System;
using System.IO;
using MasterAutomationFramework.SeleniumAPI._00_Functionality._01_AttachToChromeProcess;
using MasterAutomationFramework.SeleniumAPI._00_Functionality.DownloadFIle;
using MasterAutomationFramework.SeleniumAPI.ConstanceVariables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Selenium.Essentials.Utility;

namespace MasterAutomationFramework.SeleniumAPI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [TestCategory("Driver")]
        public void AttachToProcess()
        {
            var AttachToChromeManager = new AttachToChrome();
            AttachToChromeManager.AttachToProcess();
        }

        [TestMethod]
        [TestCategory("FileDownload")]
        public void DownloadFile()
        {
            const string FileName = "htmlReport.rar";

            if (!Directory.Exists(Const.ResourcesDirectoryPath))
            {
                Directory.CreateDirectory(Const.ResourcesDirectoryPath);
                File.Copy(Path.Combine(Runtime.CallingAssemblyFolder, FileName), Path.Combine(Const.ResourcesDirectoryPath, FileName));
            }

            new DownloadFileVerify().DonwloadFiles();
            Assert.IsTrue(File.Exists(Path.Combine(Const.DownloadDirectoryPath, FileName)));
        }
    }
}
