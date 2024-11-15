using Microsoft.AspNetCore.SignalR;

namespace DO_AN.Helpers
{
    public class MessageHub : Hub
    {
        public async Task SendMessageToAll(string senderId, string receiverId, string content)
        {
            await Clients.All.SendAsync("ReceiveMessage", senderId, receiverId, content);
        }
    }
}
