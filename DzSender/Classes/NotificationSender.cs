using DzSender.Interfaces;
using System;
namespace DzSender.Classes
{
    public class NotificationSender
    {
        // Поля могут быть private (это правильно для инкапсуляции)
        private readonly INotificationService _service;
        private readonly ILogger _logger;

        // 2️⃣ Конструктор ОБЯЗАТЕЛЬНО public
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
                throw; // Пробрасываем дальше, чтобы форма показала MessageBox
            }
        }
    }
}
