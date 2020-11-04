using NUnit.Framework;
using NunitTest.Configuration_ThirdOption;
using NunitTest.Extensions;
using NunitTest.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NunitTest.OtherResources
{
    public static class CaseCommonDataSource
    {
        /// <summary>
        /// Returns all the browser capabilities as test case source. It also includes the additional parameters that is 
        /// required by the test and will be injected into the test case as parameter
        /// </summary>
        /// <param name="additionalParams"></param>
        /// <returns></returns>
        public static IEnumerable<TestCaseData> BrowserCapabilitiesWithAdditionalParams(string additionalParams)
        {
            if (BrowserCapabilitiesModal.CurrentBrowserCapabilities.Any())
            {
                return BrowserCapabilitiesModal.CurrentBrowserCapabilities
                    .Select(b =>
                        new TestCaseData(
                            (new string[] { b.CapabilityName }.Union(additionalParams.SplitAndTrim(","))).ToArray()
                            ));
            }
            else
            {
                return new List<TestCaseData>
                {
                    new TestCaseData(
                        ((new string[] { JsonFileConfiguration.EnvData["DefaultDebugBrowser"] ?? "Chrome" })
                        .Union(additionalParams.SplitAndTrim(",")))
                        .ToArray())
                };
            }
        }

        /// <summary>
        /// Returns all the browser capabilities as test case source.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TestCaseData> BrowserCapabilities()
        {
            if (BrowserCapabilitiesModal.CurrentBrowserCapabilities.Any())
            {
                var gg = BrowserCapabilitiesModal.CurrentBrowserCapabilities.Select(b => new TestCaseData(b.CapabilityName));
                return gg;
            }
            else
            {
                return new List<TestCaseData> { new TestCaseData(JsonFileConfiguration.EnvData["DefaultDebugBrowser"] ?? "Chrome") };
            }
        }
    }
}
