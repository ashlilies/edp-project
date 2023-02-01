using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.SignalR;

namespace GrowGreenWeb.Pages.Giving.Chat
{
    [HubName("chathub")]
    public class SignalRServer : Hub
    {
        public async Task SendMessage(string user , string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
