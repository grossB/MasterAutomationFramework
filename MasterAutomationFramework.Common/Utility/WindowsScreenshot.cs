using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MasterAutomationFramework.Common.Utility
{
    public class WindowsScreenshot
    {
        private void TakeScreenshot(string path, string name)
        {
            var filename = string.Format(path, name, ".jpg");
            var screenLeft = SystemInformation.VirtualScreen.Left;
            var screenTop = SystemInformation.VirtualScreen.Top;
            var screenWidth = SystemInformation.VirtualScreen.Width;
            var screenHeight = SystemInformation.VirtualScreen.Height;

            // Create a bitmap of the appropriate size to receive the screenshot.
            using (var bmp = new Bitmap(screenWidth, screenHeight))
            {
                // Draw the screenshot into our bitmap.
                using (var g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(screenLeft, screenTop, 0, 0, bmp.Size);
                }

                bmp.Save(filename, ImageFormat.Jpeg);
            }
        }
    }
}
