using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Classes
{
    public static class FileLogger
    {
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        public static void Log(string message, string logFileName = "log.txt")
        {
            try
            {
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }

                string logPath = Path.Combine(LogDirectory, logFileName);

                using (StreamWriter writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + message);
                }
            }
            catch (Exception ex)
            {
                // You may optionally swallow or throw/log elsewhere
                Console.WriteLine("Logging error: " + ex.Message);
            }
        }
    }
}

