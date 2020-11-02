using Microsoft.Expression.Encoder.ScreenCapture;
using System;

namespace MasterAutomationFramework.Common.Elements
{
    public class ScreenRecorder
    {
        private const string PathToLog = @"C:\\Driver_Logs\TestVideo.avi";
        private static ScreenCaptureJob _screenCapture = new ScreenCaptureJob();

        // Pomogło zainstalowanie Microsoft Expression Encoder 4 Screen Capture.exe
        // Nuget => Microsoft.Expression.Encoder
        private static ScreenCaptureJob ScreenCapture
        {
            get
            {
                if (_screenCapture == null)
                {
                    _screenCapture = new ScreenCaptureJob();
                    _screenCapture.CaptureLayeredWindow = true;
                    _screenCapture.DisableZoom();
                }

                return _screenCapture;
            }
        }

        public static void StartRecording(string pathToSave = null)
        {
            var defaultPath = $@"C:\\Driver_Logs\TestVideo_{DateTime.Now.ToString().Replace(":", "_").Replace(".", "_")}.avi";
            _screenCapture.OutputScreenCaptureFileName = pathToSave ?? defaultPath;
            _screenCapture.Start();
        }

        public static void SaveVideo()
        {
            _screenCapture.Stop();
        }
    }
}
