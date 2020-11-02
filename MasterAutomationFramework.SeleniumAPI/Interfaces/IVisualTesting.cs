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
    using MasterAutomationFramework.SeleniumAPI.Enums;
    using System.Drawing;

    public interface IVisualTesting
    {
        VisualTestingResult CompareImages(Bitmap baseImage, Bitmap actualImage);
        VisualTestingResult CompareImages(string baseFilePath, string fileToComparePath);
    }
}
