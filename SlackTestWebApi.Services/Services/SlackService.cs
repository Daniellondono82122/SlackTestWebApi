﻿namespace SlackTestWebApi.Services.Services
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

            var querystring = $"channel={channelId}&icon_url={SlackConstants.ArriveIconUrl}";
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
                var msg = SlackConstants.BasePayloadMsg.Replace("PayloadMessage", payloadMessage.Message);
                querystring += $"&blocks={HttpUtility.UrlEncode(msg.Replace("AddElement", SlackConstants.AddButtons))}";
            }

            SendMessageResponseDto sendMessageResponse = await slackClientUtil.Post<SendMessageResponseDto>(SlackConstants.PostMessage, querystring);

            return sendMessageResponse.Message.ThreadTs ?? sendMessageResponse.Ts;
        }

    }
}
