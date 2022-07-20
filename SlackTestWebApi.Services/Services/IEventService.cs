
namespace SlackTestWebApi.Services.Services
{
    using SlackTestWebApi.Domain.Dtos.Slack;
    public interface IEventService
    {
        Task ProcessUserMessage(SlackEventMessage eventRequest);
    }
}
