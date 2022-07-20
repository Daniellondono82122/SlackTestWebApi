namespace SlackTestWebApi.Services.Services
{
    using SlackTestWebApi.Domain.Dtos;
    using SlackTestWebApi.Domain.Dtos.Slack;
    public class EventsService : IEventsService
    {
        public Task<BaseResponseDto<SlackResponseDto>> Handle(SlackEventMessage eventRequest)
        {
            throw new NotImplementedException();
        }
    }
}
