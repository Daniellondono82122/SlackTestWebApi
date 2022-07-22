namespace SlackTestWebApi.Services.Services
{
    using Microsoft.AspNetCore.SignalR;
    using Domain.Dtos.Slack;
    using Hubs;
    using SlackTestWebApi.Domain.Dtos;
    using Newtonsoft.Json;
    using Microsoft.Extensions.Primitives;
    using SlackTestWebApi.Domain.Dtos.Slack.Actions;
    using SlackTestWebApi.Services.Utils;
    using Microsoft.Extensions.Configuration;
    using SlackTestWebApi.Services.Constants;
    using System.Web;

    public class EventService : IEventService
    {
        private readonly IHubContext<SlackHub, ISlackHub> _slackHub;
        private readonly IConfiguration _configuration;
        public EventService(IHubContext<SlackHub, ISlackHub> slackHub, IConfiguration configuration)
        {
            _slackHub = slackHub;
            _configuration = configuration;
        }

        public async Task<BaseResponseDto<bool>> ProcessOfferAnswer(StringValues json)
        {
            var actionDto = JsonConvert.DeserializeObject<ActionDto>(json);
            if (!string.IsNullOrEmpty(actionDto?.Message.Ts))
            {
                MessageDto messageDto = new MessageDto
                {
                    ExternalId = actionDto.Message.Ts,
                    Date = DateTime.UtcNow,
                    OfferAccept = actionDto.Actions[0].Confirm != null
                };

                await UpdateMessage(actionDto.Channel.Id, actionDto.Message.Ts, messageDto.OfferAccept, 
                        actionDto.Message.Blocks.First(x => x.Type == "section").Fields[0].Text);

                await _slackHub.Clients.Group(actionDto.Message.Ts)
                    .NotifyThread(actionDto.Message.Ts, JsonConvert.SerializeObject(messageDto));
            }
            return new BaseResponseDto<bool> { Result = true };
        }


        private async Task UpdateMessage(string? channelId, string threadId, bool offerAccept, string payloadMessage)
        {
            SlackClientUtil slackClientUtil = new(_configuration);

            var querystring = $"channel={channelId}&ts={threadId}&pretty=1";
            querystring += $"&blocks={HttpUtility.UrlEncode(GetMessage(payloadMessage, offerAccept))}";
            await slackClientUtil.Post<object>(SlackConstants.UpdateMessage, querystring);
        }


        private static string GetMessage(string payloadMessage, bool offerAccept)
        {
            var msg = SlackConstants.BasePayloadMsg.Replace("PayloadMessage", payloadMessage);
            if (offerAccept)
            {
                return msg.Replace("AddElement", SlackConstants.AddAcceptedMsg); 
            }
            return msg.Replace("AddElement", SlackConstants.AddDeclinedMsg);
        }

        public async Task ProcessUserMessage(SlackEventMessage eventRequest)
        {
            if (!string.IsNullOrEmpty(eventRequest.Event.ThreadTs))
            {
                MessageDto messageDto = new MessageDto
                {
                    ExternalId = eventRequest.Event.ThreadTs,
                    Date = DateTime.UtcNow,
                    Message = eventRequest.Event.Text
                };

                await _slackHub.Clients.Group(eventRequest.Event.ThreadTs)
                .NotifyThread(eventRequest.Event.ThreadTs, JsonConvert.SerializeObject(messageDto));
            }
        }
    }
}
