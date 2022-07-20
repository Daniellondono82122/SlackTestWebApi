namespace SlackTestWebApi.Services.Hubs
{
    public interface ISlackHub
    {
        Task NotifyThread(string ChannelId, string notificationMessage);
        Task SendAsync(string notificationGroup, string connectionId);
    }
}
