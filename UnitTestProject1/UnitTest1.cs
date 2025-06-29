using System;
using System.IO;
using MasterAutomationFramework.Common.Helper;
using MasterAutomationFramework.SeleniumAPI._00_Functionality._01_AttachToChromeProcess;
using MasterAutomationFramework.SeleniumAPI._00_Functionality.DownloadFIle;
using MasterAutomationFramework.SeleniumAPI.ConstanceVariables;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
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
            ProcessCleanupHelper.KillChromeDriverTreeProcessGracefully();
        }

        [TestMethod]
        [TestCategory("FileDownload")]
        public void DownloadFile()
        {
            const string FileName = "htmlReport.rar";

            if (!Directory.Exists(Const.ResourcesDirectoryPath))
            {
                Directory.CreateDirectory(Const.ResourcesDirectoryPath);
                //File.Copy(Path.Combine(Runtime.CallingAssemblyFolder, FileName), Path.Combine(Const.ResourcesDirectoryPath, FileName));
            }

            new DownloadFileVerify().DonwloadFiles();
            Assert.IsTrue(File.Exists(Path.Combine(Const.DownloadDirectoryPath, FileName)));
        }
    }
}
