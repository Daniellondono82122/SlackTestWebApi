using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackTestWebApi.Services.Hubs
{
    public interface ISlackHub
    {
        Task NotifyChannel(string ChannelId, string notificationMessage);
        Task SendAsync(string notificationGroup, string connectionId);
    }
}
