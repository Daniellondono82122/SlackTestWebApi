namespace SlackTestWebApi.Domain.Dtos
{
    public class SlackEventsRequest
    {
        public string token { get; set; }
        public string challenge { get; set; }
        public string type { get; set; }
    }
}
