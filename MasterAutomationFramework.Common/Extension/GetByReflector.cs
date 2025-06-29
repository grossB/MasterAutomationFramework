namespace MasterAutomationFramework.SeleniumAPI.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    public static class GetByReflector
    {
        public static Dictionary<string, By> GetAllElementLocators(Type classType)
        {
            Dictionary<string, By> _dict = new Dictionary<string, By>();
            PropertyInfo[] props = classType.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    FindsByAttribute authAttr = attr as FindsByAttribute;
                    if (authAttr != null)
                    {
                        string propName = prop.Name;
                        string auth = authAttr.ToString();
                        var auth2 = authAttr.How;
                        var auth3 = authAttr.Using;

                        try
                        {
                            By locator = GetLocator(authAttr.How.ToString(), authAttr.Using);
                            _dict.Add(propName, locator);
                        }
                        catch (NotImplementedException)
                        {
                            continue;
                        }

                    }
                }
            }

            return _dict;
        }

        public static By GetElementLocator(this IWebElement element, Type classType, string propertyName)
        {
            By locator = null;
            PropertyInfo[] props = classType.GetProperties();
            foreach (PropertyInfo prop in props.Where(x => x.Name.Equals(propertyName)))
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    FindsByAttribute authAttr = attr as FindsByAttribute;
                    if (authAttr != null)
                    {
                        locator = GetLocator(authAttr.How.ToString(), authAttr.Using);
                    }
                }
            }

            return locator;
        }

        private static By GetLocator(string type, string path)
        {
            By locator = null;

            switch (type.ToLower())
            {
                case "id":
                    locator = By.Id(path);
                    break;
                case "tagname":
                    locator = By.TagName(path);
                    break;
                case "xpath":
                    locator = By.XPath(path);
                    break;
                case "linktext":
                    locator = By.LinkText(path);
                    break;
                case "cssselector":
                    locator = By.CssSelector(path);
                    break;
                case "name":
                    locator = By.Name(path);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return locator;
        }
    }
}
