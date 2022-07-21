namespace SlackTestWebApi.Domain.Dtos.Slack
{
    using System.Text.Json.Serialization;
    using Newtonsoft.Json;

    public class Action
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("style")]
        public string Style { get; set; }

        [JsonPropertyName("confirm")]
        public Confirm Confirm { get; set; }
    }

    public class Attachment
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("fallback")]
        public string Fallback { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("callback_id")]
        public string CallbackId { get; set; }

        [JsonPropertyName("actions")]
        public List<Action> Actions { get; set; }
    }

    public class Confirm
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("ok_text")]
        public string OkText { get; set; }

        [JsonPropertyName("dismiss_text")]
        public string DismissText { get; set; }
    }

    public class Icons
    {
        [JsonPropertyName("image_48")]
        public string Image48 { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("subtype")]
        public string Subtype { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("ts")]
        public string Ts { get; set; }

        [JsonProperty("thread_ts")]
        public string ThreadTs { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("icons")]
        public Icons Icons { get; set; }

        [JsonPropertyName("bot_id")]
        public string BotId { get; set; }

        [JsonPropertyName("app_id")]
        public string AppId { get; set; }

        [JsonPropertyName("attachments")]
        public List<Attachment> Attachments { get; set; }
    }

    public class SendMessageResponseDto
    {
        [JsonPropertyName("ok")]
        public bool Ok { get; set; }

        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        [JsonPropertyName("ts")]
        public string Ts { get; set; }

        [JsonPropertyName("message")]
        public Message Message { get; set; }
    }
}
