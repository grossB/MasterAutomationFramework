namespace MasterAutomationFramework.Common.Serilog
{
    using System;
    using System.IO;
    using global::Serilog;
    using global::Serilog.Core;
    using global::Serilog.Events;

    public class SeriLogger
    {
        public readonly string LogPath;

        private readonly string fileName;

        private readonly Logger logger;

        /// <summary>
        /// Get now time stamp as string value.
        /// </summary>
        /// <returns>Time stamp with _ prefix.</returns>
        private string GetNowTimeStamp()
        {
            return DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss");
        }

        /// <summary>
        /// Get test name text with timespan postfix.
        /// </summary>
        /// <param name="text">Test name</param>
        /// <returns>Text with date postfix.</returns>
        private string AppendDateTimeStamp(string text)
        {
            return $"{text}_{this.GetNowTimeStamp()}";
        }

        public void SaveResult(bool testResult)
        {
            File.Create($"{this.LogPath}\\{testResult}");
        }

        public SeriLogger(string testName)
        {
            const string HistoryPath = "c:\\History";
            this.LogPath = $"{HistoryPath}\\{testName}\\{testName}{this.GetNowTimeStamp()}";
            this.CreateFolderHistory(HistoryPath);
            this.CreateFolderHistory(this.LogPath);
            this.fileName = this.AppendDateTimeStamp($"{this.LogPath}\\{testName}_");

            this.logger = new LoggerConfiguration()
                .WriteTo
                .File($"{this.fileName}.txt")
                .MinimumLevel.Debug()
                .CreateLogger();
        }

        private void CreateFolderHistory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void Dispose()
        {
            this.logger.Dispose();
        }

        public void Debug(string msg)
        {
            this.logger.Write(LogEventLevel.Debug, msg);

        }
        public void Error(string msg)
        {
            this.logger.Write(LogEventLevel.Error, msg);

        }
        public void Fatal(string msg)
        {
            this.logger.Write(LogEventLevel.Fatal, msg);

        }
        public void Information(string msg)
        {
            this.logger.Write(LogEventLevel.Information, msg);

        }
        public void Verbose(string msg)
        {
            this.logger.Write(LogEventLevel.Verbose, msg);

        }
        public void Warning(string msg)
        {
            this.logger.Write(LogEventLevel.Warning, msg);
        }
    }
}
