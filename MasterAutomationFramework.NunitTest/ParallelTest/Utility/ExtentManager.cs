using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using System;
using System.Net;

namespace NunitTest.ParallelTest.Utility
{
    public class ExtentManager
    {
        private static readonly Lazy<AventStack.ExtentReports.ExtentReports> _lazy = new Lazy<AventStack.ExtentReports.ExtentReports>(() => new AventStack.ExtentReports.ExtentReports());

        public static AventStack.ExtentReports.ExtentReports Instance { get { return _lazy.Value; } }

        static ExtentManager()
        {
            AventStack.ExtentReports.ExtentReports Reporter = new AventStack.ExtentReports.ExtentReports();
            var htmlReporter = new ExtentV3HtmlReporter("C:\\htmlReport.html");
            htmlReporter.Config.DocumentTitle = "Extent/NUnit Samples";
            htmlReporter.Config.ReportName = "Extent/NUnit Samples";
            htmlReporter.Config.Theme = Theme.Standard;
            Instance.AddSystemInfo("Operating System", Environment.OSVersion.ToString());
            Instance.AddSystemInfo("HostName", Dns.GetHostName());
            Instance.AddSystemInfo("Browser", "Chrome");

            Instance.AttachReporter(htmlReporter);
        }
    }
}
