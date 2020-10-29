//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Drawing;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Interactions;
//using OpenQA.Selenium.Internal;
//using OpenQA.Selenium.Support.UI;

//namespace MasterAutomationFramework.SeleniumAPI.NewFolder1
//{
//    // Consider to add logging capability for each Element Action



//    /// <summary>
//    /// Represents a single DOM element in a browser
//    /// </summary>
//    /// <remarks>
//    /// This class wraps Selenium's <see cref="IWebElement"/> to provide additional capabilities for logging, automatic waiting and more.
//    /// </remarks>
//    public class BrowserElement : IWebElement, IWrapsElement
//    {
//        private readonly Func<IWebElement> _getWebElement;

//        internal /*private */BrowserElement(Func<IWebElement> getWebElement, string description)
//        {
//            _getWebElement = getWebElement;
//        }

//        /// <summary>
//        /// Constructs a new instance of <see cref="BrowserElement"/> by copying its properties, except of its description from another element
//        /// </summary>
//        /// <param name="otherElement">The existing element to copy its properties from</param>
//        /// <param name="description">The new description to use for the new object</param>
//        /// <exception cref="ArgumentNullException"><paramref name="otherElement"/> is null</exception>
//        protected BrowserElement(BrowserElement otherElement, string description)
//               : this( () => otherElement.WebElement, description)
//        {
//        }

//        /// <summary>
//        /// Copy constructor
//        /// </summary>
//        /// <param name="otherElement">Other object from which to copy the properties into the new object</param>
//        protected BrowserElement(BrowserElement otherElement)
//            : this(otherElement.Description)
//        {
//        }

//        /// <summary>
//        /// Initializes a new instance of <see cref="BrowserElement"/> given its container, a 'By' filter a selector and description
//        /// </summary>
//        /// <param name="container">The container that contains the relevant element. Typically this is a <see cref="Browser"/>, <see cref="BrowserWindow"/>, <see cref="Frame"/> or a containing <see cref="BrowserElement"/></param>
//        /// <param name="by">A filter mechanism used to filter the matching elements</param>
//        /// <param name="selector">A delegate that is used to select the sepecific element from the filtered elements</param>
//        /// <param name="description">The description of the new element</param>
//        /// <exception cref="ArgumentNullException">Any of the arguments is null</exception>
//        //public BrowserElement(ElementsContainer container, By by, Func<IReadOnlyCollection<IWebElement>, IWebElement> selector, string description)
//        //    : this(() => GetWebElement(container, by, selector, description), description)
//        //{
//        //}

//        /// <summary>
//        /// Initialized a new instance of <see cref="BrowserElement"/> given its container, a specific 'By' filter and description
//        /// </summary>
//        /// <param name="container">The container that contains the relevant element. Typically this is a <see cref="Browser"/>, <see cref="BrowserWindow"/>, <see cref="Frame"/> or a containing <see cref="BrowserElement"/></param>
//        /// <param name="by">A filter mechanism used to find the element. If multiple elements match the filter, the first one is used</param>
//        /// <param name="description">The description of the new element</param>
//        /// <exception cref="ArgumentNullException">Any of the arguments is null</exception>
//        //public BrowserElement(ElementsContainer container, By by, string description)
//        //    : this(SafeGetDOMRoot(container, "container"), () => container.GetSearchContext().FindElement(by), description)
//        //{
//        //}

//        private static IWebElement GetWebElement(ElementsContainer elementsContainer, By by, Func<IReadOnlyCollection<IWebElement>, IWebElement> selector, string description)
//        {
//            if (elementsContainer == null)
//                throw new ArgumentNullException("elementsContainer");
//            if (by == null)
//                throw new ArgumentNullException("by");
//            if (selector == null)
//                throw new ArgumentNullException("selector");

//            var searchContext = elementsContainer.GetSearchContext();
//            var matchingElements = searchContext.FindElements(by);
//            var selectedElement = selector(matchingElements);
//            if (selectedElement == null)
//                throw new NoSuchElementException(string.Format("Element '{0}' could not be found or is not available", description));

//            return selectedElement;
//        }

//        void IWebElement.Clear()
//        {
//            WebElement.Clear();
//        }

//        void IWebElement.SendKeys(string text)
//        {
//            WebElement.SendKeys(text);
//        }

//        void IWebElement.Submit()
//        {
//            WebElement.Submit();
//        }

//        /// <inheritdoc />
//        public void Click()
//        {
//            WebElement.Click();
//        }

//        /// <inheritdoc />
//        public string GetAttribute(string attributeName)
//        {
//            return WebElement.GetAttribute(attributeName);
//        }

//        string IWebElement.GetProperty(string propertyName)
//        {
//            return WebElement.GetProperty(propertyName);
//        }

//        string IWebElement.GetCssValue(string propertyName)
//        {
//            return WebElement.GetCssValue(propertyName);
//        }

//        string IWebElement.TagName
//        {
//            get { return WebElement.TagName; }
//        }

//        /// <summary>
//        ///  Gets or sets the text of this element
//        /// </summary>
//        /// <exception cref="StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM</exception>
//        /// <remarks>The setter of this property can be used instead of <see cref="IWebElement.SendKeys"/>. However, in addition to <see cref="IWebElement.SendKeys"/>
//        /// it first clears the current content of the element and also logs the typeing to the Log using <see cref="Logger"/></remarks>
//        public string Text
//        {
//            get { return WebElement.Text; }
//            set
//            {
//                Logger.WriteLine("Type '{0}' in '{1}'", value, Description);
//                WebElement.Clear();
//                WebElement.SendKeys(value);
//            }
//        }

//        /// <inheritdoc />
//        public bool Enabled
//        {
//            get
//            {
//                return WebElement.Enabled;
//            }
//        }

//        /// <inheritdoc />
//        public bool Selected
//        {
//            get
//            {
//                return WebElement.Selected;
//            }
//        }

//        /// <inheritdoc />
//        public Point Location
//        {
//            get
//            {
//                return WebElement.Location;
//            }
//        }

//        /// <inheritdoc />
//        public Size Size
//        {
//            get
//            {
//                return WebElement.Size;
//            }
//        }

//        /// <inheritdoc />
//        public bool Displayed
//        {
//            get
//            {
//                try
//                {
//                    var webElement = WebElement;
//                    // WebElement may return null if the element is not displayed
//                    return webElement != null && webElement.Displayed;
//                }
//                catch (StaleElementReferenceException)
//                {
//                    return false; // the element is removed from the DOM and therefore it's not displayed.
//                }
//            }
//        }

//        private IWebElement WebElement
//        {
//            get
//            {
//                DOMRoot.Activate();
//                try
//                {
//                    return _getWebElement();
//                }
//                catch (Exception ex)
//                // we catch all exceptions here, because _getWebElement may throw different exceptions if the element could not be found after the first time.
//                {
//                    // If we can't find the element now, even though we first found it (when the constructor is called), it most probably means that it was removed from the DOM
//                    throw new StaleElementReferenceException(
//                        string.Format("Failed to locate element '{0}' (after it was already found before)", Description), ex);
//                }
//            }
//        }

//        private bool IsReady()
//        {
//            var result = WebElement.Displayed && WebElement.Enabled;
//            return result;
//        }

//        IWebElement ISearchContext.FindElement(By by)
//        {
//            return WebElement.FindElement(by);
//        }

//        ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by)
//        {
//            return WebElement.FindElements(by);
//        }

//        /// <inheritdoc />
//        protected internal sealed override ISearchContext GetSearchContext()
//        {
//            return WebElement;
//        }

//        internal static bool IsAvailable(IWebElement el)
//        {
//            try
//            {
//                return el.Displayed;
//            }
//            catch (StaleElementReferenceException)
//            {
//                //Logger.WriteLine("Warning: Stale element is caught");
//                return false;
//            }
//        }

//        /// <summary>
//        /// Hovers the mouse over the element
//        /// </summary>
//        public void Hover()
//        {
//            var action = CreateActionsSequence();
//            var moveToElement = action.MoveToElement(WebElement).Build();
//            moveToElement.Perform();
//        }

//        /// <summary>
//        /// Returns the immediate parent element containing the current element
//        /// </summary>
//        /// <param name="description">The description to give to the parent element</param>
//        /// <returns>A <see cref="BrowserElement"/> that represents the parent element</returns>
//        public BrowserElement GetParent(string description)
//        {
//            return new BrowserElement(this, GetParentLocator(), description);
//        }

//        internal static By GetParentLocator()
//        {
//            return By.XPath("..");
//        }

//        // TODO: check if this method can be removed?
//        internal IWebElement GetWebElement()
//        {
//            return WebElement;
//        }

//        #region IWrapsElement Members

//        IWebElement IWrapsElement.WrappedElement
//        {
//            get { return WebElement; }
//        }

//        #endregion
//    }
//}
