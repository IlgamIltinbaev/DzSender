using DzSender.Interfaces;
namespace DzSender.Classes
{
    public class SmsService : INotificationService
    {
        private readonly ILogger _logger;
        public string Name => "SMS Service";

        public SmsService(ILogger logger)
        {
            _logger = logger;
        }

        public void Send(string message)
        {
            _logger.LogInfo($"Отправка SMS: {message}");
        }
    }
}
