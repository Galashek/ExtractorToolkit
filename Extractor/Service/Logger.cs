using System;
using System.IO;

namespace Extractor.Service
{
    public static class Logger
    {
        public static readonly string LogFile;

        static Logger()
        {
            var cur = Environment.CurrentDirectory;
            var logDir = Path.Combine(cur, "log");
            LogFile = Path.Combine(logDir, "system.log");
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);
        }

        public static void Log(string message)
        {
            var logEntry = $"[{DateTime.Now}] {message}{Environment.NewLine}";
            File.AppendAllText(LogFile, logEntry);
        }
    }
}
