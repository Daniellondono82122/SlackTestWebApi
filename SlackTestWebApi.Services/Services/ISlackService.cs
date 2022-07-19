namespace SlackTestWebApi.Services.Services
{
    using Domain.Dtos;
    public interface ISlackService
    {
        Task<BaseResponseDto<SlackResponseDto>> SendMessageAsync(PayloadMessage payloadMessage);
    }
}
