
namespace SlackTestWebApi.Services.Services
{
    using SlackTestWebApi.Domain.Dtos;
    using SlackTestWebApi.Domain.Dtos.Slack;
    public interface IEventsService
    {
        Task<BaseResponseDto<SlackResponseDto>> Handle(SlackEventMessage eventRequest);
    }
}
