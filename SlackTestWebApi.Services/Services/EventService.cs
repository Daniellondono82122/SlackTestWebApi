namespace SlackTestWebApi.Services.Services
{
    using Microsoft.AspNetCore.SignalR;
    using Domain.Dtos.Slack;
    using Hubs;

    public class EventService : IEventService
    {
        private readonly IHubContext<SlackHub, ISlackHub> _slackHub;
        public EventService(IHubContext<SlackHub, ISlackHub> slackHub)
        {
            _slackHub = slackHub;
        }

        public async Task ProcessUserMessage(SlackEventMessage eventRequest)
        {
            await _slackHub.Clients.Group(eventRequest.Event.ThreadTs)
            .NotifyThread(eventRequest.Event.ThreadTs, eventRequest.Event.Text);
        }
    }
}
