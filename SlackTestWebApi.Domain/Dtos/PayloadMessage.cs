namespace SlackTestWebApi.Domain.Dtos
{
    using System.Collections.Generic;
    public class PayloadMessage
    {
        public List<User> Users { get; set; }
        public string? ChannelId { get; set; }
        public string? ThreadId { get; set; }
        public string Message { get; set; }
        public int? OfferId { get; set; }
        public int? LoadNumber { get; set; }
    }
}