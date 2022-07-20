namespace SlackTestWebApi.Domain.Dtos.Slack
{
    using Newtonsoft.Json;

    public class ValidationMessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("challenge")]
        public string Challenge { get; set; }
    }
}
