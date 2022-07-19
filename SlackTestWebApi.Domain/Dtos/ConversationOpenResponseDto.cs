//namespace SlackTestWebApi.Domain.Dtos
//{
//    using Newtonsoft.Json;

//    public class ConversationOpenResponseDto
//    {
//        [JsonProperty("ok")]
//        public bool Ok { get; set; }

//        [JsonProperty("channel")]
//        public Channel Channel { get; set; }
//    }
//    public class Channel
//    {
//        [JsonProperty("id")]
//        public string Id { get; set; }

//        [JsonProperty("name")]
//        public string Name { get; set; }

//        [JsonProperty("is_channel")]
//        public bool IsChannel { get; set; }

//        [JsonProperty("is_group")]
//        public bool IsGroup { get; set; }

//        [JsonProperty("is_im")]
//        public bool IsIm { get; set; }

//        [JsonProperty("is_mpim")]
//        public bool IsMpim { get; set; }

//        [JsonProperty("is_private")]
//        public bool IsPrivate { get; set; }

//        [JsonProperty("created")]
//        public int Created { get; set; }

//        [JsonProperty("is_archived")]
//        public bool IsArchived { get; set; }

//        [JsonProperty("is_general")]
//        public bool IsGeneral { get; set; }

//        [JsonProperty("unlinked")]
//        public int Unlinked { get; set; }

//        [JsonProperty("name_normalized")]
//        public string NameNormalized { get; set; }

//        [JsonProperty("is_shared")]
//        public bool IsShared { get; set; }

//        [JsonProperty("is_org_shared")]
//        public bool IsOrgShared { get; set; }

//        [JsonProperty("is_pending_ext_shared")]
//        public bool IsPendingExtShared { get; set; }

//        [JsonProperty("pending_shared")]
//        public List<object> PendingShared { get; set; }

//        [JsonProperty("context_team_id")]
//        public string ContextTeamId { get; set; }

//        [JsonProperty("parent_conversation")]
//        public object ParentConversation { get; set; }

//        [JsonProperty("creator")]
//        public string Creator { get; set; }

//        [JsonProperty("is_ext_shared")]
//        public bool IsExtShared { get; set; }

//        [JsonProperty("shared_team_ids")]
//        public List<string> SharedTeamIds { get; set; }

//        [JsonProperty("pending_connected_team_ids")]
//        public List<object> PendingConnectedTeamIds { get; set; }

//        [JsonProperty("is_member")]
//        public bool IsMember { get; set; }

//        [JsonProperty("last_read")]
//        public string LastRead { get; set; }

//        [JsonProperty("is_open")]
//        public bool IsOpen { get; set; }

//        [JsonProperty("topic")]
//        public Topic Topic { get; set; }

//        [JsonProperty("purpose")]
//        public Purpose Purpose { get; set; }

//        [JsonProperty("priority")]
//        public int Priority { get; set; }
//    }

//    public class Purpose
//    {
//        [JsonProperty("value")]
//        public string Value { get; set; }

//        [JsonProperty("creator")]
//        public string Creator { get; set; }

//        [JsonProperty("last_set")]
//        public int LastSet { get; set; }
//    }


//    public class Topic
//    {
//        [JsonProperty("value")]
//        public string Value { get; set; }

//        [JsonProperty("creator")]
//        public string Creator { get; set; }

//        [JsonProperty("last_set")]
//        public int LastSet { get; set; }
//    }
//}
