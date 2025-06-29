using OpenQA.Selenium;

namespace MasterAutomationFramework
{
    public static class RetriveElementXPath
    {
        /// <summary>
        /// Returns the Xpath selector of the element.
        /// </summary>
        /// <param name="e">web element</param>
        /// <param name="driver">IWebDriver associated with the element</param>
        /// <param name="excludeIdCheck">Exclude ID based xpath selector</param>
        /// <returns></returns>
        public static string GetElementXPath(this IWebElement e, IWebDriver driver, bool excludeIdCheck = false)
        {
            var scriptWithId = "if(c.id!==''){return'//*[@id=\"'+c.id+'\"]'}";
            var scriptToGetXpath = "gPt=function(c){" + (excludeIdCheck ? string.Empty : scriptWithId) + "if(c===document.body){return c.tagName}var a=0;var e=c.parentNode.childNodes;for(var b=0;b<e.length;b++){var d=e[b];if(d===c){return gPt(c.parentNode)+'/'+c.tagName+'['+(a+1)+']'}if(d.nodeType===1&&d.tagName===c.tagName){a++}}};return gPt(arguments[0]);";

            var path = (string)driver.ExecuteJavaScript(scriptToGetXpath, e);

            if (!path.StartsWith("//"))
            {
                path = "//" + path;
            }
            return path;
        }

        public static object ExecuteJavaScript(this IWebDriver driver, string script, params object[] args) => (driver as IJavaScriptExecutor).ExecuteScript(script, args);

    }
}
