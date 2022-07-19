namespace SlackTestWebApi.Services.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Dtos;
    using Domain.Enum;
    using Microsoft.Extensions.Configuration;
    using Utils;

    public class SlackService : ISlackService
    {
        private readonly IConfiguration _configuration;
        public SlackService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<BaseResponseDto<SlackResponseDto>> SendMessageAsync(PayloadMessage payloadMessage)
        {
            var channelId = await OpenConversation(payloadMessage.Users
                .Where(x=>x.UserType != UserTypeEnum.Carrier).Select(u => u.ExternalId));
            var threadId = await PostMessage(channelId, payloadMessage);
            var slackResponseDto = new SlackResponseDto();
            if (payloadMessage.ThreadId is null)
            {
                slackResponseDto.ThreadId = threadId;
            }
            return new BaseResponseDto<SlackResponseDto>
            {
                Message = "Message sent!",
                Result = slackResponseDto
            };
        }

        private async Task<string> OpenConversation(IEnumerable<string> externalIds)
        {
            SlackClientUtil slackClientUtil = new(_configuration);

            SlackClientResponseDto slackClientResponse = await slackClientUtil.Post<SlackClientResponseDto>(Constants.SlackMethods.OpenConversation, $"users={string.Join(",", externalIds)}&pretty = 1");

            return slackClientResponse.Channel.Id;
        }

        private async Task<string> PostMessage(string channelId, PayloadMessage payloadMessage)
        {
            SlackClientUtil slackClientUtil = new(_configuration);

            var querystring = $"channel={channelId}&text={payloadMessage.Message}";
            if (payloadMessage.ThreadId is not null)
            {
                querystring += "&thread_ts={payloadMessage.ThreadId}";
            }

            await slackClientUtil.Post<object>(Constants.SlackMethods.PostMessage, querystring);

            return String.Empty;
        }
    }
}
