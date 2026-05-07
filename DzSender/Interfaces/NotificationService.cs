namespace DzSender.Interfaces
{ 
    public interface INotificationService
    {
        string Name { get; }
        void Send(string message);
    }
}
