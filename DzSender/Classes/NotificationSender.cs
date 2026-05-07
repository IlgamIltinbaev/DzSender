using DzSender.Interfaces;
using System;
namespace DzSender.Classes
{
    public class NotificationSender
    {
        private readonly INotificationService _service;
        private readonly ILogger _logger;
        public NotificationSender(INotificationService service, ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        public void ExecuteSend(string message)
        {
            try
            {
                _service.Send(message);
                _logger.LogInfo($"Успешно отправлено через {_service.Name}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка отправки через {_service.Name}: {ex.Message}");
                throw;
            }
        }
    }
}
