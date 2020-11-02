namespace MasterAutomationFramework.SeleniumAPI.Elements
{
    using OpenQA.Selenium;

    public class JsExecutor
    {
        private readonly IJavaScriptExecutor javaScriptExecutor;

        public void ScrollIntoView(IWebElement element)
        {
            javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView({block: 'center'})", element);
        }

        public void SetElementAttribute(IWebElement element, string attribute, string value)
        {
            javaScriptExecutor.ExecuteScript($"arguments[0].setAttribute('{attribute}', '{value}')", element);
        }

        public void SetElementAttribute(By locator, string attribute, string value)
        {
            var element = ((IWebDriver)javaScriptExecutor).FindElement(locator);
            javaScriptExecutor.ExecuteScript($"arguments[0].setAttribute('{attribute}', '{value}')", element);
        }

        public JsExecutor(IWebDriver driver)
        {
            this.javaScriptExecutor = (IJavaScriptExecutor)driver;
        }

        public string ExecuteScriptString(string script)
        {
            return (string)this.ExecuteScript(script);
        }

        public long ExecuteScriptInt(string script)
        {
            long result = (long)this.ExecuteScript(script);
            return result;
        }

        public object ExecuteScript(string script)
        {
            return this.javaScriptExecutor.ExecuteScript(script);
        }

        public object ExecuteScriptWithArguments(string script, object arguments)
        {
            return this.javaScriptExecutor.ExecuteScript(script, arguments);
        }
    }
}
