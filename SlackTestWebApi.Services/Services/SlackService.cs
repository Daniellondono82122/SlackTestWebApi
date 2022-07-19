namespace SlackTestWebApi.Services.Services
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Domain.Dtos;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using RestSharp;
    using SlackTestWebApi.Domain.Enum;

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
            var slackurl = _configuration.GetSection("SlackUrl").Value;
            var token = _configuration.GetSection("SlackBotToken").Value;
            var url = $"{slackurl}conversations.open?users={string.Join(",", externalIds)}&pretty=1";

            var client = new RestClient(url);
            var request = new RestRequest(url,Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK || response.Content is null)
            {
                throw new NotImplementedException();
            }
            var content = JsonConvert.DeserializeObject<ConversationOpenResponseDto>(response.Content, new JsonSerializerSettings { 
                NullValueHandling = NullValueHandling.Ignore
            });
            if (content is null || !content.Ok)
            {
                throw new NotImplementedException();
            }
            return content.Channel.Id;
        }

        private async Task<string> PostMessage(string channelId, PayloadMessage payloadMessage)
        {
            var slackurl = _configuration.GetSection("SlackUrl").Value;
            var token = _configuration.GetSection("SlackBotToken").Value;
            var url = $"{slackurl}chat.postMessage?channel={channelId}&text={payloadMessage.Message}" +
                $"&username={payloadMessage.Users.First(x => x.UserType == UserTypeEnum.Carrier).Name}&pretty=1";
            if (payloadMessage.ThreadId is not null)
            {
                url += "&thread_ts={payloadMessage.ThreadId}";
            }

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK || response.Content is null)
            {
                throw new NotImplementedException();
            }

            var content = JsonConvert.DeserializeObject<SendMessageResponseDto>(response.Content, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            if (content is null || !content.Ok)
            {
                throw new NotImplementedException();
            }
            return content.Message.ThreadTs;

        }
    }
}
