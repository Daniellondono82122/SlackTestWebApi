namespace SlackTestWebApi.Domain.Dtos
{
    using System.Text.Json.Serialization;

    public class SlackClientResponseDto
    { 
        [JsonPropertyName("ok")]
        public bool Ok { get; set; }

        [JsonPropertyName("no_op")]
        public bool NoOp { get; set; }

        [JsonPropertyName("already_open")]
        public bool AlreadyOpen { get; set; }

        [JsonPropertyName("channel")]
        public Channel Channel { get; set; }
    }

    public class Channel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
