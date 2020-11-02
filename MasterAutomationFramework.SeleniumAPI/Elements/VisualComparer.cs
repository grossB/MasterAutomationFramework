namespace MasterAutomationFramework.SeleniumAPI.Elements
{
    using MasterAutomationFramework.SeleniumAPI.Enums;
    using MasterAutomationFramework.SeleniumAPI.Interfaces;
    using System;
    using System.Drawing;

    public class VisualComparer : IVisualTesting
    {
        public void Example()
        {
            //var googlePage = new GooglePage(this.WebSeleniumApi);
            //googlePage.Open();

            //var image1 = this.WebSeleniumApi.ScreenShotManager.TakeElementScreenShot(googlePage.SearchInput);
            //this.WebSeleniumApi.ScreenShotManager.SaveImageAsBmp(image1, this.Logger.LogPath, "image1");

            //googlePage.SearchInput.SendKeys("sdsd");
            //var image2 = this.WebSeleniumApi.ScreenShotManager.TakeElementScreenShot(googlePage.SearchInput);
            //var result = this.WebSeleniumApi.VisualComparer.CompareImages(image2, image1);

            //this.Logger.Information(result == VisualTestingResult.PixelMismatch ? $"Correct {result.ToString().ToUpper()}" : $"Incorect Result {result}");

            //var image3 = this.WebSeleniumApi.ScreenShotManager.TakeElementScreenShot(googlePage.SearchInput);
            //result = this.WebSeleniumApi.VisualComparer.CompareImages(image2, image3);
            //this.WebSeleniumApi.ScreenShotManager.SaveImageAsBmp(image2, this.Logger.LogPath, "image2");
            //this.WebSeleniumApi.ScreenShotManager.SaveImageAsBmp(image3, this.Logger.LogPath, "image3");
            //this.Logger.Information(result == VisualTestingResult.Matched ? $"Correct {result.ToString().ToUpper()}" : $"Incorect Result {result}");
        }

        public void Example2()
        {
            //var microsoftPage = new MicrosoftPage(this.Driver);
            //this.screenShotManager.TakeFullPageScreenShot();

            //var element1 = microsoftPage.MicrosoftImage;
            //var element2 = microsoftPage.NoobImage;
            //Bitmap screen1 = this.screenShotManager.TakeElementScreenShot(element1);
            //Bitmap screen3 = this.screenShotManager.TakeElementScreenShot(element1);
            //Bitmap screen2 = this.screenShotManager.TakeElementScreenShot(element2);
            //this.screenShotManager.SaveImageAsBmp(screen1);
            //this.screenShotManager.SaveImageAsBmp(screen2);
            //this.screenShotManager.SaveImageAsBmp(screen3);
            //this.CompareImages(screen1, screen3);
            //this.CompareImages(screen1, screen1);
            //this.CompareImages(screen1, screen2);
            //this.CompareImages(@"C:\img_12-51-02-576.bmp", @"C:\imgsdsdsdd.bmp");
        }

        public VisualTestingResult CompareImages(Bitmap baseImage, Bitmap actualImage)
        {
            var result = this.CheckCondition(baseImage, actualImage);
            this.PrintResult(result);
            return result;
        }

        public VisualTestingResult CompareImages(string baseFilePath, string fileToComparePath)
        {
            Bitmap baseImage = (Bitmap)Image.FromFile(baseFilePath);
            Bitmap actualImage = (Bitmap)Image.FromFile(fileToComparePath);
            var result = this.CheckCondition(baseImage, actualImage);
            this.PrintResult(result);
            return result;
        }

        private VisualTestingResult CheckCondition(Bitmap baseImage, Bitmap actualImage)
        {
            if (baseImage.Size != actualImage.Size)
            {
                return VisualTestingResult.SizeMismatch;
            }

            for (int x = 0; x <= baseImage.Width - 1; x++)
            {
                for (int y = 0; y <= baseImage.Height - 1; y++)
                {
                    if (!baseImage.GetPixel(x, y).Equals(actualImage.GetPixel(x, y)))
                    {
                        // Pixel are different
                        return VisualTestingResult.PixelMismatch;
                    }
                }
            }
            return VisualTestingResult.Matched;
        }

        private void PrintResult(VisualTestingResult result)
        {
            switch (result)
            {
                case VisualTestingResult.Matched:
                    Console.WriteLine("Images are the same!");
                    break;
                case VisualTestingResult.SizeMismatch:
                    Console.WriteLine("Images have different sizes.");
                    break;
                case VisualTestingResult.PixelMismatch:
                    Console.WriteLine("Pixels color are different.");
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
