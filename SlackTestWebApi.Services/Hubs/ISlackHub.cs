namespace SlackTestWebApi.Services.Hubs
{
    public interface ISlackHub
    {
        Task NotifyThread(string ThreadId, string notificationMessage);
        Task SendAsync(string notificationGroup, string connectionId);
    }
}
