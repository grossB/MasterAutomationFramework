using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterAutomationFramework.Common.Extension
{
    public static class WebDriverExtensions
    {
        public static void CreateSnapShotHtml(this IWebDriver driver, string testName)
        {
            try
            {
                var fileNameBase = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}";
                var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "testresults");

                if (!Directory.Exists(artifactDirectory))
                {
                    Directory.CreateDirectory(artifactDirectory);
                }

                File.WriteAllText(Path.Combine(artifactDirectory, fileNameBase + "_source.html"), driver.PageSource, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: {0}", ex);
            }
        }
    }
}
