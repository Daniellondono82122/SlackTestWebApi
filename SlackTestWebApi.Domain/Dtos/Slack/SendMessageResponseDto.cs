namespace SlackTestWebApi.Domain.Dtos.Slack
{
    using Newtonsoft.Json;

    public class SendMessageResponseDto
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("ts")]
        public string Ts { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }

    }

    public class Message
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("subtype")]
        public string Subtype { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("ts")]
        public string Ts { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("bot_id")]
        public string BotId { get; set; }

        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [JsonProperty("thread_ts")]
        public string ThreadTs { get; set; }
    }
}
