
namespace SlackTestWebApi.Services.Services
{
    using Microsoft.Extensions.Primitives;
    using SlackTestWebApi.Domain.Dtos;
    using SlackTestWebApi.Domain.Dtos.Slack;
    public interface IEventService
    {
        Task ProcessUserMessage(SlackEventMessage eventRequest);
        Task<BaseResponseDto<bool>> ProcessOfferAnswer(StringValues json);
    }
}
