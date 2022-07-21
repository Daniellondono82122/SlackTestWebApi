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
    using System;
    using Newtonsoft.Json.Linq;
    using System.Web;

    public class SlackService : ISlackService
    {
        private readonly IConfiguration _configuration;
        public SlackService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<BaseResponseDto<List<ExternalUserDto>>> GetUsersByEmailAsync(List<string> emails)
        {
            List<ExternalUserDto> externalUserList = new();

            SlackClientUtil slackClientUtil = new(_configuration);

            foreach (var email in emails)
            {
                SlackUserProfile slackUserProfile = await slackClientUtil.Post<SlackUserProfile>(SlackConstants.LookupUserByEmail, $"email={email}");
                externalUserList.Add(new ExternalUserDto { Email = email, ExternalId = slackUserProfile.User.Id });
            }

            return new BaseResponseDto<List<ExternalUserDto>>
            {
                Message = "Sucessful",
                Result = externalUserList
            };
        }

        public async Task<BaseResponseDto<SlackResponseDto>> SendMessageAsync(PayloadMessage payloadMessage)
        {
            string? channelId = payloadMessage.ChannelId;
            string threadId;

            if (string.IsNullOrEmpty(payloadMessage.ChannelId))
            {
                channelId = await OpenConversation(payloadMessage.Users
                    .Where(x => x.UserType != UserTypeEnum.Carrier).Select(u => u.ExternalId));
            }

            threadId = await PostMessage(channelId, payloadMessage, payloadMessage.Users.FirstOrDefault(u => u.UserType == UserTypeEnum.Carrier).Name);

            var slackResponseDto = new SlackResponseDto();

            slackResponseDto.ThreadId = threadId;
            slackResponseDto.ChannelId = channelId;

            return new BaseResponseDto<SlackResponseDto>
            {
                Message = "Message sent!",
                Result = slackResponseDto
            };
        }

        private async Task<string> OpenConversation(IEnumerable<string> externalIds)
        {
            SlackClientUtil slackClientUtil = new(_configuration);

            ConversationOpenResponseDto slackClientResponse = await slackClientUtil.Post<ConversationOpenResponseDto>(SlackConstants.OpenConversation, $"users={string.Join(",", externalIds)}&pretty = 1");

            return slackClientResponse.Channel.Id;
        }

        private async Task<string> PostMessage(string? channelId, PayloadMessage payloadMessage, string alias)
        {
            SlackClientUtil slackClientUtil = new(_configuration);

            var querystring = $"channel={channelId}&text={payloadMessage.Message}&icon_url={SlackConstants.ArriveIconUrl}";
            if (!string.IsNullOrEmpty(payloadMessage.ThreadId))
            {
                querystring += $"&thread_ts={payloadMessage.ThreadId}";
            }

            if (!string.IsNullOrEmpty(alias))
            {
                querystring += $"&username={alias}";
            }

            if (payloadMessage.ShowButtons)
            {
                var attachmentSerialized = "[{\"text\":\"Accept offer?\",\"fallback\":\"You are unable to choose an offer\",\"callback_id\":\"offer_choose\",\"color\":\"#2AAAE2\",\"attachment_type\":\"default\",\"actions\":[{\"name\":\"choose\",\"text\":\"Accept\",\"type\":\"button\",\"style\":\"primary\",\"value\":\"yes_button\",\"confirm\":{\"title\":\"Are you sure?\",\"text\":\"Accept offer?\",\"ok_text\":\"Yes\",\"dismiss_text\":\"No\"}},{\"name\":\"choose\",\"text\":\"Decline\",\"type\":\"button\",\"value\":\"no_button\",\"style\":\"danger\"}]}]";
                try
                {
                    querystring += $"&attachments={HttpUtility.UrlEncode(attachmentSerialized)}";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }


            }

            SendMessageResponseDto sendMessageResponse = await slackClientUtil.Post<SendMessageResponseDto>(SlackConstants.PostMessage, querystring);

            return sendMessageResponse.Message.ThreadTs ?? sendMessageResponse.Ts;
        }

    }
}
