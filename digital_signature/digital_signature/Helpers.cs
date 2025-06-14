using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digital_signature
{
    public static class Helpers
    {
        public static string InitLogFile(string LogFolder, string logName, string timestamp)
        {
            if (!Directory.Exists(LogFolder))
                Directory.CreateDirectory(LogFolder);

            string logFilePath = Path.Combine(LogFolder, $"{logName}_{timestamp}.txt");

            if (!File.Exists(logFilePath))
                File.WriteAllText(logFilePath, "", Encoding.UTF8);
            return logFilePath;
        }

        public static void WriteLog(string logFilePath, string logContent)
        {
            /*string logContent = $"Ký thành công: {fileName}{Environment.NewLine}";*/
            File.AppendAllText(logFilePath, logContent, Encoding.UTF8);

            Console.WriteLine(logContent.Trim());
        }
    }
}
