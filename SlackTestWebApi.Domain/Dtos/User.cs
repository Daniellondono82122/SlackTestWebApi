namespace SlackTestWebApi.Domain.Dtos
{
    using Domain.Enum;

    public class User
    {
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string AccelerateId { get; set; }
        public UserTypeEnum UserType { get; set; }
    }
}
