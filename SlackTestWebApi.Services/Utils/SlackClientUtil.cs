namespace SlackTestWebApi.Services.Utils
{
    using System.Net;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using RestSharp;

    public class SlackClientUtil
    {
        private readonly IConfiguration _configuration;

        public SlackClientUtil(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<T> Post<T>(string method, string querystring = "")
        {
            var slackUrl = _configuration.GetSection("SlackUrl").Value;
            var token = $"{_configuration.GetSection("TokenPrefix").Value}-{_configuration.GetSection("SlackBotToken").Value}";

            var resultUrl = $"{slackUrl}/{method}?{querystring}";

            var client = new RestClient(resultUrl);
            var request = new RestRequest(resultUrl, Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK || response.Content is null)
            {
                throw new NotImplementedException();
            }

            var content = JsonConvert.DeserializeObject<T>(response.Content, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            return content;
        }
    }
}
