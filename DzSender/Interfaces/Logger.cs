using System;

namespace DzSender.Interfaces
{
    public interface ILogger
    {
        event Action<string> OnLogUpdate;
        void Log(string message);
        void LogError(string message);
        void LogInfo(string message);
    }
}
