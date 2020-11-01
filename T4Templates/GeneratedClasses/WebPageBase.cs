using OpenQA.Selenium;
using System.Collections.Generic;

namespace GenericTemplates.GeneratedClasses
{
    public abstract class WebPageBase
    {
        public abstract void Open();

        public abstract string UrlAdress { get; }

        public abstract Dictionary<string, By> Selectors { get; }
        //protected abstract void LoadElements();
    }
}
