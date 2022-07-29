namespace SlackTestWebApi.Tests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using SlackTestWebApi.Services.Hubs;

    public class SlackHubTests
    {
        [Fact]
        public async Task SlackHubTests_SignalRTest()
        {
            // Arrange
            var connection = await StartConnectionAsync();
            var notificationMessage = "Test Message";
            var groupid = "1";

            var handler = new Mock<Action<string>>();
            connection.On("MessageHandler", handler.Object);

            // Act

            await connection.StartAsync();
            await connection.InvokeAsync("JoinThreadGroups", new string[] { groupid });
            await connection.InvokeAsync("SendMessageToGroup", groupid, notificationMessage);

            // Assert

            handler.Verify(x => x(It.Is<string>(n => n == notificationMessage)), Times.Once());

            await connection.DisposeAsync();
        }

        private static Task<HubConnection> StartConnectionAsync()
        {
            var webHostBuilder = new WebHostBuilder()
            .ConfigureServices(services =>
            {
                services.AddSignalR(o => { o.EnableDetailedErrors = true; });
            })
            .Configure(app =>
            {
                app.UseRouting();
                app.UseEndpoints(config => config.MapHub<SlackHub>("/slackhub"));
            });

            var server = new TestServer(webHostBuilder);

            var connection = new HubConnectionBuilder()
                                .WithUrl(
                                    $"http://localhost/slackhub",
                                    o => o.HttpMessageHandlerFactory = _ => server.CreateHandler())
                                .AddJsonProtocol()
                                .Build();
            return Task.FromResult(connection);
        }
    }
}
