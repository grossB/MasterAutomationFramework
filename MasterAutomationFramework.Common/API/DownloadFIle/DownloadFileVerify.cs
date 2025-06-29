namespace MasterAutomationFramework.SeleniumAPI._00_Functionality.DownloadFIle
{
    using MasterAutomationFramework.SeleniumAPI.ConstanceVariables;
    using MasterAutomationFramework.SeleniumAPI.Enums;
    using MasterAutomationFramework.SeleniumAPI.Extension;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using MasterAutomationFramework.SeleniumAPI.API.DriverFactory;

    // Add Reference System.IO.Compression.FileSystem
    // Add Reference System.IO.Compression
    public class DownloadFileVerify
    {
        public void DonwloadFiles()
        {
            try
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.SetOptions(ChromeDriverOptions.downloadDirectory, Const.DownloadDirectoryPath);
                ChromeDriver driver = (ChromeDriver)DriverFactoryEx.CreateDriver(chromeOptions);

                driver.FindElementsByClassName("className");
                driver.FindElementsByCssSelector("css");
                driver.FindElementsById("someId");
                driver.FindElementsByName("nameAttribute");
                driver.FindElementsByLinkText("linkText");
                driver.FindElementsByPartialLinkText("partialText");
                driver.FindElementsByTagName("button");
                driver.FindElementsByXPath("//*[@id='someId']");

                driver.Url = Const.ResourcesPath;
                var zipFile = driver.FindElementByXPath("//*[@id='tbody']//a[contains(text(),'html')]");
                zipFile.Click();

                if (!Directory.Exists(Const.DownloadDirectoryPath))
                {
                    Directory.CreateDirectory(Const.DownloadDirectoryPath);
                }

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var files = new DirectoryInfo(Const.DownloadDirectoryPath);

                if (files.GetFiles().Count() > 0)
                {
                    wait.Until(d =>
                    {
                        long length = files.GetFiles().First().Length;
                        Thread.Sleep(TimeSpan.FromSeconds(3));
                        return length == files.GetFiles().First().Length && length > 0;
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
