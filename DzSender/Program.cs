using DzSender.Classes;
using DzSender.Infrastructures;
using DzSender.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace DzSender
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Container container = new Container();
            ILogger logger = new AppLogger();
            List<INotificationService> services = new List<INotificationService>
            {
                new EmailService(logger),
                new SmsService(logger),
                new PushNotificationService(logger)
            };
            container.Register<ILogger>(logger);
            container.Register<List<INotificationService>>(services);
            MainForm mainform = new MainForm(
                container.Get<List<INotificationService>>(),
                container.Get<ILogger>()
            );
            Application.Run(mainform);
        }
    }
}
