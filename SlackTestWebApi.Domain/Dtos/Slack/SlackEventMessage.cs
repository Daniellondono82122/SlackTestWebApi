namespace SlackTestWebApi.Domain.Dtos.Slack
{
    using System.Text.Json.Serialization;

    public class Authorization
    {
        [JsonPropertyName("enterprise_id")]
        public object EnterpriseId { get; set; }

        [JsonPropertyName("team_id")]
        public string TeamId { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("is_bot")]
        public bool IsBot { get; set; }

        [JsonPropertyName("is_enterprise_install")]
        public bool IsEnterpriseInstall { get; set; }
    }

    public class Block
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("block_id")]
        public string BlockId { get; set; }

        [JsonPropertyName("elements")]
        public List<Element> Elements { get; set; }
    }

    public class Element
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("elements")]
        public List<Element> Elements { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class Event
    {
        [JsonPropertyName("client_msg_id")]
        public string ClientMsgId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("user")]
        public string User { get; set; }

        [JsonPropertyName("ts")]
        public string Ts { get; set; }

        [JsonPropertyName("team")]
        public string Team { get; set; }

        [JsonPropertyName("blocks")]
        public List<Block> Blocks { get; set; }

        [JsonPropertyName("thread_ts")]
        public string ThreadTs { get; set; }

        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        [JsonPropertyName("event_ts")]
        public string EventTs { get; set; }

        [JsonPropertyName("channel_type")]
        public string ChannelType { get; set; }
    }

    public class SlackEventMessage
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("team_id")]
        public string TeamId { get; set; }

        [JsonPropertyName("api_app_id")]
        public string ApiAppId { get; set; }

        [JsonPropertyName("event")]
        public Event Event { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("event_id")]
        public string EventId { get; set; }

        [JsonPropertyName("event_time")]
        public int EventTime { get; set; }

        [JsonPropertyName("authorizations")]
        public List<Authorization> Authorizations { get; set; }

        [JsonPropertyName("is_ext_shared_channel")]
        public bool IsExtSharedChannel { get; set; }

        [JsonPropertyName("event_context")]
        public string EventContext { get; set; }
    }


}
