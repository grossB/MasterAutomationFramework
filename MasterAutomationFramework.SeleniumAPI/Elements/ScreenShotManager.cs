// ========================================================================
//          Copyright (C) Innovative Solutions Gross Bartosz
//                      - All Rights Reserved
// ========================================================================
//
// The source code contained or described herein and all documents related
// to the source code are owned by Innovative Solutions Gross Bartosz
// Unauthorized copying of this file, via any medium is strictly prohibited
//
// ========================================================================
//          Developed by Bartosz Gross, grossbartosz@gmail.com
// ========================================================================
// ------------------------------------------------------------------------

namespace MasterAutomationFramework.SeleniumAPI.Elements
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Internal;
    using SeleniumAPI.Interfaces;
    using WDSE;
    using WDSE.Decorators;
    using WDSE.ScreenshotMaker;

    public class ScreenShotManager : ITakeSreenShot
    {
        IWebDriver WebDriver;

        // Add to Reference using System.Drawing; namespace and import it to code.
        // 1600 x 900 - resolution on laptop
        public ScreenShotManager(IWebDriver driver)
        {
            this.WebDriver = driver;
        }

        /// <summary>
        // Install NuGet => Noksa.WebDriver.ScreenshotsExtensions
        // Add those using:
        // using WDSE.Decorators;
        // using WDSE.ScreenshotMaker;
        // using WDSE;
        /// </summary>
        public void TakeFullPageScreenShot()
        {
            var vcs = new VerticalCombineDecorator(new ScreenshotMaker());
            byte[] byteArray = this.WebDriver.TakeScreenshot(vcs);
            Bitmap screenshot = new Bitmap(new MemoryStream(byteArray));
            string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            screenshot.Save($"C:\\img_{fileName}.jpg", ImageFormat.Jpeg);
        }

        public void FastElementScreenShot(IWebDriver driver, IWebElement element)
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            Byte[] byteArray = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            Bitmap screenshot = new Bitmap(new MemoryStream(byteArray));
            Rectangle croppedImage = new Rectangle(element.Location.X, element.Location.Y, element.Size.Width, element.Size.Height);
            screenshot = screenshot.Clone(croppedImage, screenshot.PixelFormat);
            screenshot.Save($"C:\\img_{fileName}.jpg", ImageFormat.Jpeg);
        }

        public Bitmap TakeElementScreenShot(IWebElement webElement)
        {
            var wrappedDriver = ((IWrapsDriver)webElement).WrappedDriver;
            Screenshot screen = ((ITakesScreenshot)this.WebDriver).GetScreenshot();
            return this.GetSubImage(screen, new Rectangle(webElement.Location, webElement.Size));
        }

        public void SaveImageAsBmp(Bitmap screen)
        {
            var name = DateTime.Now.ToString("HH-mm-ss-fff");
            screen.Save($"C:\\img_{name}.bmp", ImageFormat.Bmp);
        }

        public void SaveImageAsBmp()
        {
            var screen = GetImage(((ChromeDriver)WebDriver).GetScreenshot());

            var name = DateTime.Now.ToString("HH-mm-ss-fff");
            screen.Save($"C:\\img_{name}.bmp", ImageFormat.Bmp);
        }

        private Bitmap GetImage(Screenshot screen)
        {
            Bitmap bmp;
            using (var ms = new MemoryStream(screen.AsByteArray))
            {
                bmp = new Bitmap(ms);
            }

            return bmp;
        }

        public Bitmap GetSubImage(Screenshot screen, Rectangle rect)
        {
            Bitmap bmp;
            using (var ms = new MemoryStream(screen.AsByteArray))
            {
                bmp = new Bitmap(ms);
            }

            var output = bmp.Clone(rect, bmp.PixelFormat);
            return output;
        }
    }
}
