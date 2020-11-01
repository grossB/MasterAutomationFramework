using NUnit.Framework;
using NunitTest.ParallelTest.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace NunitTest.ParallelTest.Children
{
    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    public class TestChildren1 : BaseFixture
    {
        [Test]
        public void TestMethod3()
        {
            var driver = new ChromeDriver(".");
            //driver.Navigate().GoToUrl("https://www.google.pl/");
            driver.Navigate().GoToUrl("file:///C:/Users/Bartony/Desktop/dddd");
            driver.FindElementByXPath("//*[@id='tsf']/div[2]/div[1]/div[1]/div/div[2]/input").SendKeys("knight online" + Keys.Enter);
            Thread.Sleep(3000);
            var elementCheck = driver.FindElementByXPath("//*[@id='rso']/div[2]/div/div[1]/div/div/div[1]/a[1]/h3/span").Displayed;
            Assert.IsTrue(elementCheck, "Element was not present");
            driver.Quit();
        }

        [Test]
        public void TestMethod4()
        {
            var driver = new ChromeDriver(".");
            //driver.Navigate().GoToUrl("https://www.google.pl/");
            driver.Navigate().GoToUrl("file:///C:/Users/Bartony/Desktop/dddd");
            driver.FindElementByXPath("//*[@id='tsf']/div[2]/div[1]/div[1]/div/div[2]/input").SendKeys("knight online" + Keys.Enter);
            Thread.Sleep(3000);
            var elementCheck = driver.FindElementByXPath("//*[@id='rso']/div[2]/div/div[1]/div/div/div[1]/a[1]/h3/span").Displayed;
            Assert.IsTrue(elementCheck, "Element was not present");
            driver.Quit();
        }
    }
}
    