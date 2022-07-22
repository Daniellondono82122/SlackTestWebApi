using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SlackTestWebApi.Domain.Dtos.Slack.Actions
{
    public class Action
    {
        [JsonPropertyName("confirm")]
        public Confirm Confirm { get; set; }
        [JsonPropertyName("deny")]
        public Confirm Deny { get; set; }

        [JsonPropertyName("action_id")]
        public string ActionId { get; set; }

        [JsonPropertyName("block_id")]
        public string BlockId { get; set; }

        [JsonPropertyName("text")]
        public Text Text { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("style")]
        public string Style { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("action_ts")]
        public string ActionTs { get; set; }
    }

    public class Block
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("block_id")]
        public string BlockId { get; set; }

        [JsonPropertyName("fields")]
        public List<Field> Fields { get; set; }

        [JsonPropertyName("elements")]
        public List<Element> Elements { get; set; }
    }

    public class Channel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Confirm
    {
        [JsonPropertyName("title")]
        public Title Title { get; set; }

        [JsonPropertyName("confirm")]
        public Confirm Confirms { get; set; }

        [JsonPropertyName("deny")]
        public Deny Deny { get; set; }

        [JsonPropertyName("text")]
        public Text Text { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("emoji")]
        public bool Emoji { get; set; }
    }

    public class Container
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("message_ts")]
        public string MessageTs { get; set; }

        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }

        [JsonPropertyName("is_ephemeral")]
        public bool IsEphemeral { get; set; }
    }

    public class Deny
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("emoji")]
        public bool Emoji { get; set; }
    }

    public class Element
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("action_id")]
        public string ActionId { get; set; }

        [JsonPropertyName("text")]
        public Text Text { get; set; }

        [JsonPropertyName("style")]
        public string Style { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("confirm")]
        public Confirm Confirm { get; set; }
    }

    public class Field
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("emoji")]
        public bool Emoji { get; set; }
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

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("bot_id")]
        public string BotId { get; set; }

        [JsonPropertyName("app_id")]
        public string AppId { get; set; }

        [JsonPropertyName("blocks")]
        public List<Block> Blocks { get; set; }
    }

    public class ActionDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("user")]
        public User User { get; set; }

        [JsonPropertyName("api_app_id")]
        public string ApiAppId { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("container")]
        public Container Container { get; set; }

        [JsonPropertyName("trigger_id")]
        public string TriggerId { get; set; }

        [JsonPropertyName("team")]
        public Team Team { get; set; }

        [JsonPropertyName("enterprise")]
        public object Enterprise { get; set; }

        [JsonPropertyName("is_enterprise_install")]
        public bool IsEnterpriseInstall { get; set; }

        [JsonPropertyName("channel")]
        public Channel Channel { get; set; }

        [JsonPropertyName("message")]
        public Message Message { get; set; }

        [JsonPropertyName("state")]
        public State State { get; set; }

        [JsonPropertyName("response_url")]
        public string ResponseUrl { get; set; }

        [JsonPropertyName("actions")]
        public List<Action> Actions { get; set; }
    }

    public class State
    {
        [JsonPropertyName("values")]
        public Values Values { get; set; }
    }

    public class Team
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("domain")]
        public string Domain { get; set; }
    }

    public class Text
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("text")]
        public string TextMsg { get; set; }

        [JsonPropertyName("emoji")]
        public bool Emoji { get; set; }
    }

    public class Title
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("emoji")]
        public bool Emoji { get; set; }
    }

    public class User
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("team_id")]
        public string TeamId { get; set; }
    }

    public class Values
    {
    }




}
