using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DzSender.Classes;
using DzSender.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DzSender
{

    public partial class MainForm : Form
    {
        private readonly ILogger _logger;
        // Храним список всех сервисов для ComboBox
        private readonly IEnumerable<INotificationService> _allServices;

        // ПАСХАЛКА: Счетчик отправлений
        private int _sendCount = 0;

        // Внедрение зависимостей через конструктор
        public MainForm(IEnumerable<INotificationService> services, ILogger logger)
        {
            InitializeComponent();

            _allServices = services;
            _logger = logger;

            // Подписываемся на событие логгера, чтобы писать в ListBox формы
            // ВАЖНО: Логгер пишет из другого потока (иногда), поэтому используем Invoke
            _logger.OnLogUpdate += (msg) =>
            {
                if (listLogs.InvokeRequired)
                {
                    listLogs.Invoke((Action)(() => listLogs.Items.Add(msg)));
                }
                else
                {
                    listLogs.Items.Add(msg);
                }
            };

            // Заполняем ComboBox
            foreach (var service in _allServices)
            {
                cmbServices.Items.Add(service); // В качестве объекта храним сам сервис
                cmbServices.DisplayMember = "Name"; // Отображаем свойство Name
            }

            // Выбираем первый по умолчанию
            if (cmbServices.Items.Count > 0)
                cmbServices.SelectedIndex = 0;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text;

            // 1. Проверка пустого сообщения
            if (string.IsNullOrWhiteSpace(message))
            {
                _logger.LogError("Пользователь попытался отправить пустое сообщение!");
                MessageBox.Show("Сообщение не может быть пустым!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем выбранный сервис
            var selectedService = cmbServices.SelectedItem as INotificationService;
            if (selectedService == null) return;

            // 2. ПАСХАЛКА
            _sendCount++;
            if (message.ToLower() == "секрет" && _sendCount == 3)
            {
                // Если 3 раза отправить слово "секрет"
                MessageBox.Show(" Пасхалка найдена! Вы взломали матрицу! 🎉");
                _logger.LogInfo("АКТИВИРОВАНА ПАСХАЛКА!");
            }

            // 3. Создание Sender (как в задании: Sender принимает Service через конструктор)
            // Мы передаем логгер, чтобы Sender тоже мог логировать
            var senderObj = new NotificationSender(selectedService, _logger);

            try
            {
                // Вызываем Send
                senderObj.ExecuteSend(message);
            }
            catch (Exception ex)
            {
                // Если внутри Sender возникла ошибка (случайное исключение)
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Сбой отправки", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
