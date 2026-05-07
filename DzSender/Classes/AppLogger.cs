using DzSender.Interfaces;
using System;
using System.IO;
namespace DzSender.Classes
{
    public class AppLogger : ILogger
    {
        public event Action<string> OnLogUpdate;

        public void LogInfo(string message)
        {
            Log($"[INFO] {message}");
        }

        public void LogError(string message)
        {
            Log($"[ERROR] {message}");
        }

        public void Log(string message) 
        {
            //Запись в файл
            string path = "logs.txt";
            File.AppendAllText(path, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}");

            //Уведомление UI 
            OnLogUpdate?.Invoke(message);
        }
    }
}
