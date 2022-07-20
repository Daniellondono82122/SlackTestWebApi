namespace SlackTestWebApi.Services.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Dtos;
    using Domain.Enum;
    using Microsoft.Extensions.Configuration;
    using Domain.Dtos.Slack;
    using Utils;
    using SlackTestWebApi.Services.Constants;

    public class SlackService : ISlackService
    {
        private readonly IConfiguration _configuration;
        public SlackService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<BaseResponseDto<SlackResponseDto>> SendMessageAsync(PayloadMessage payloadMessage)
        {
            string channelId = payloadMessage.ChannelId;
            string threadId;

            if (string.IsNullOrEmpty(payloadMessage.ChannelId))
            {
                channelId = await OpenConversation(payloadMessage.Users
                    .Where(x => x.UserType != UserTypeEnum.Carrier).Select(u => u.ExternalId));
            }

            threadId = await PostMessage(channelId, payloadMessage);

            var slackResponseDto = new SlackResponseDto();

            if (payloadMessage.ThreadId is null)
            {
                slackResponseDto.ThreadId = threadId;
                slackResponseDto.ChannelId = channelId;
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

            ConversationOpenResponseDto slackClientResponse = await slackClientUtil.Post<ConversationOpenResponseDto>(SlackMethods.OpenConversation, $"users={string.Join(",", externalIds)}&pretty = 1");

            return slackClientResponse.Channel.Id;
        }

        private async Task<string> PostMessage(string channelId, PayloadMessage payloadMessage)
        {
            SlackClientUtil slackClientUtil = new(_configuration);

            var querystring = $"channel={channelId}&text={payloadMessage.Message}";
            if (!string.IsNullOrEmpty(payloadMessage.ThreadId))
            {
                querystring += "&thread_ts={payloadMessage.ThreadId}";
            }

            SendMessageResponseDto sendMessageResponse = await slackClientUtil.Post<SendMessageResponseDto>(SlackMethods.PostMessage, querystring);

            return sendMessageResponse.Ts;
        }
    }
}
