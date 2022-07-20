namespace SlackTestWebApi.Services.Services
{
    using System.Collections.Generic;
    using Domain.Dtos;
    public interface ISlackService
    {
        Task<BaseResponseDto<SlackResponseDto>> SendMessageAsync(PayloadMessage payloadMessage);
        Task<BaseResponseDto<List<ExternalUserDto>>> GetUsersByEmailAsync(List<string> emails);
    }
}
