namespace SlackTestWebApi.Services.Constants
{
    public static class SlackConstants
    {
        //Methods
        public static readonly string OpenConversation = "conversations.open";
        public static readonly string PostMessage = "chat.postMessage";
        public static readonly string UpdateMessage = "chat.update";
        public static readonly string LookupUserByEmail = "users.lookupByEmail";     
        
        //Constants
        public static readonly string ArriveIconUrl = "https://tscscreencastliveeast.blob.core.windows.net/uploads/g0003020mGRD9BsW6pE5mrIA0Bi5j/2022-07-[…]T19%3A25%3A13Z&se=2022-07-22T19%3A30%3A13Z&sp=r";

        //PayloadMessages
        public static readonly string BasePayloadMsg = "[{\"type\":\"section\",\"fields\":[{\"type\":\"plain_text\",\"text\":\"PayloadMessage\",\"emoji\":true}]},AddElement]";
        public static readonly string AddButtons = "{\"type\":\"actions\",\"elements\":[{\"type\":\"button\",\"text\":{\"type\":\"plain_text\",\"emoji\":true,\"text\":\"Accept\"},\"style\":\"primary\",\"value\":\"click_me_123\",\"confirm\":{\"title\":{\"type\":\"plain_text\",\"text\":\"Are you sure?\"},\"confirm\":{\"type\":\"plain_text\",\"text\":\"Do it\"},\"deny\":{\"type\":\"plain_text\",\"text\":\"Stop, I've changed my mind!\"}}},{\"type\":\"button\",\"text\":{\"type\":\"plain_text\",\"emoji\":true,\"text\":\"Decline\"},\"style\":\"danger\",\"value\":\"click_me_123\"}]}";
        public static readonly string AddAcceptedMsg = "{\"type\":\"section\",\"text\":{\"type\":\"plain_text\",\"text\":\":white_check_mark: Accepted! \",\"emoji\":true}}";
        public static readonly string AddDeclinedMsg = "{\"type\":\"section\",\"text\":{\"type\":\"plain_text\",\"text\":\":x: Declined! \",\"emoji\":true}}";
    }
}
