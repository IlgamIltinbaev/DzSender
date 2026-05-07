using DzSender.Interfaces;
using System;
namespace DzSender.Classes
{
    public class PushNotificationService : INotificationService
    {
        private readonly ILogger _logger;
        private readonly Random _random = new Random();
        public string Name => "Push Service";

        public PushNotificationService(ILogger logger)
        {
            _logger = logger;
        }

        public void Send(string message)
        {
            // Имитируем ошибку с вероятностью 30%
            if (_random.Next(0, 10) < 3)
            {
                throw new Exception("Случайный сбой сервера Push-уведомлений!");
            }

            _logger.LogInfo($"Отправка Push: {message}");
        }
    }
}
