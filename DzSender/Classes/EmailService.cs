using DzSender.Interfaces;
namespace DzSender.Classes
{
    public class EmailService : INotificationService
    {
        private readonly ILogger _logger;
        public string Name => "Email Service";

        // Внедряем логгер через конструктор
        public EmailService(ILogger logger)
        {
            _logger = logger;
        }

        public void Send(string message)
        {
            _logger.LogInfo($"Отправка Email: {message}");
            // Имитация задержки
            System.Threading.Thread.Sleep(1000);
        }
    }
}
