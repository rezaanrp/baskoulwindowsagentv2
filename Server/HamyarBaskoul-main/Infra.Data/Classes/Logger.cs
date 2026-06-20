using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Classes
{
    public static class Logger
    {
        public static string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "APILogs", "soap_errors_log.txt");

        public static void LogToFile(string message)
        {
            try
            {
                string logDirectory = Path.GetDirectoryName(logFilePath);

                if (!Directory.Exists(logDirectory))
                    Directory.CreateDirectory(logDirectory);
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}";
                File.AppendAllText(logDirectory, logEntry);
            }
            catch
            {
                // Optional: fallback if logging fails (avoid throwing inside logging)
            }
        }
    }

}
