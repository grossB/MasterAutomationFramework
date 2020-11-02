using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MasterAutomationFramework.SeleniumAPI.PageObjectModelWithRefeltedBy
{
    public static class GetByReflectorSolution2
    {

        private static By GetSelector(string type, string path)
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
            }

            return locator;
        }

        public static Dictionary<string, By> LoadElements(string pageName)
        {
            Dictionary<string, By> elementsLocators = new Dictionary<string, By>();
            string startupPath2 = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + "\\..\\GenericTemplates\\Pages\\" + pageName + ".xml";

            var doc = new XmlDocument();
            doc.Load(startupPath2);

            XmlNode nodes = doc.SelectSingleNode("//PageClass");
            var nodesElements = nodes.SelectNodes("ElementSelector");
            foreach (System.Xml.XmlElement node in nodesElements)
            {
                var elementName = node.Attributes["elementName"].Value;
                var by = node.Attributes["selector"].Value;
                var path = node.Attributes["path"].Value;
                By locator = GetSelector(by, path);
                elementsLocators.Add(elementName, locator);
            }

            return elementsLocators;
        }
    }
}
