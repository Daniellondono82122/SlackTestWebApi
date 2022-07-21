namespace SlackTestWebApi.Services.Services
{
    using Microsoft.AspNetCore.SignalR;
    using Domain.Dtos.Slack;
    using Hubs;
    using SlackTestWebApi.Domain.Dtos;
    using Newtonsoft.Json;

    public class EventService : IEventService
    {
        private readonly IHubContext<SlackHub, ISlackHub> _slackHub;
        public EventService(IHubContext<SlackHub, ISlackHub> slackHub)
        {
            _slackHub = slackHub;
        }

        public async Task ProcessUserMessage(SlackEventMessage eventRequest)
        {
            MessageDto messageDto = new MessageDto
            {
                ExternalId = eventRequest.Event.Ts,
                Date = DateTime.UtcNow,
                Message = eventRequest.Event.Text
            };

            await _slackHub.Clients.Group(eventRequest.Event.Ts)
            .NotifyThread(eventRequest.Event.Ts, JsonConvert.SerializeObject(messageDto));
        }
    }
}
