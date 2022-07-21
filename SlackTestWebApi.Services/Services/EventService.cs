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
            if (!string.IsNullOrEmpty(eventRequest.Event.ThreadTs))
            {
                MessageDto messageDto = new MessageDto
                {
                    ExternalId = eventRequest.Event.ThreadTs,
                    Date = DateTime.UtcNow,
                    Message = eventRequest.Event.Text
                };

                await _slackHub.Clients.Group(eventRequest.Event.ThreadTs)
                .NotifyThread(eventRequest.Event.ThreadTs, JsonConvert.SerializeObject(messageDto));
            }
        }
    }
}
