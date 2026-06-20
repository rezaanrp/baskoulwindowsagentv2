using System;
using System.IO;
using System.Text;

namespace MyWinformsApp
{
    public static class AppLogger
    {
        private static readonly object _lock = new object();
        private static readonly string _logDir =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

        private static string CurrentLogFile =>
            Path.Combine(_logDir, DateTime.Now.ToString("yyyyMMdd") + ".log");

        static AppLogger()
        {
            try
            {
                if (!Directory.Exists(_logDir))
                    Directory.CreateDirectory(_logDir);
            }
            catch
            {
                // اگر حتی لاگ هم نتونه بسازه، دیگه کاری نمی‌کنیم که برنامه کرش نکنه
            }
        }

        public static void Info(string message) => Write("INFO", message);
        public static void Error(string message, Exception? ex = null) =>
            Write("ERROR", message + (ex != null ? Environment.NewLine + ex : ""));

        public static void Debug(string message) => Write("DEBUG", message);

        private static void Write(string level, string message)
        {
            try
            {
                lock (_lock)
                {
                    var line = new StringBuilder();
                    line.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    line.Append(" [").Append(level).Append("] ");
                    line.Append(message);
                    line.AppendLine();

                    File.AppendAllText(CurrentLogFile, line.ToString(), Encoding.UTF8);
                }
            }
            catch
            {
                // اینجا هم هیچ کاری نکنیم که اگر لاگ مشکل داشت، برنامه نخوابه
            }
        }
    }
}
