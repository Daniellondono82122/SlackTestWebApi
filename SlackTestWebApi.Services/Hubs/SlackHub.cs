namespace SlackTestWebApi.Services.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using NewRelic.Api.Agent;

    public class SlackHub : Hub<ISlackHub>
    {
        private readonly IHubContext<SlackHub> _context;
        public SlackHub(IHubContext<SlackHub> context)
        {
            _context = context;
        }

        [Transaction]
        public async Task SendMessageToGroup(string groupid, string message)
        {
            await _context.Clients.Group(groupid).SendAsync("MessageHandler", message);
        }

        [Transaction]
        public async Task JoinThreadGroup(string notificationGroup)
        {
            await _context.Groups.AddToGroupAsync(Context.ConnectionId, notificationGroup);
        }

        [Transaction]
        public async Task JoinThreadGroups(HashSet<string> notificationGroups)
        {
            foreach (var lt in notificationGroups)
            {
                await _context.Groups.AddToGroupAsync(Context.ConnectionId, lt);
            }
        }

        [Transaction]
        public async Task LeaveThreadGroup(string notificationGroup)
        {
            await _context.Groups.RemoveFromGroupAsync(Context.ConnectionId, notificationGroup);
        }

        [Transaction]
        public async Task LeaveThreadGroups(HashSet<string> notificationGroups)
        {
            foreach (var lt in notificationGroups)
            {
                await _context.Groups.RemoveFromGroupAsync(Context.ConnectionId, lt);
            }
        }

        [Transaction]
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("notifyConnectionId", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        [Transaction]
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
