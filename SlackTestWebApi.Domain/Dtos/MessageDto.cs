namespace SlackTestWebApi.Domain.Dtos
{
    public class MessageDto
    {
        public string ExternalId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool OfferAccept { get; set; }
    }
}
