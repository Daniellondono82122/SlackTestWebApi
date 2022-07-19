namespace SlackTestWebApi.Domain.Enum
{
    using System.ComponentModel;
    public enum UserTypeEnum
    {
        [Description("Carrier")]
        Carrier = 1,
        [Description("User")]
        User = 2
    }
}
