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

namespace MasterAutomationFramework.SeleniumAPI.Interfaces
{
    using System.Drawing;
    using OpenQA.Selenium;

    public interface ITakeSreenShot
    {
        void FastElementScreenShot(IWebDriver driver, IWebElement element);
        void TakeFullPageScreenShot();
        Bitmap TakeElementScreenShot(IWebElement webElement);
        Bitmap GetSubImage(Screenshot screen, Rectangle rect);
        void SaveImageAsBmp(Bitmap screen);
    }
}
