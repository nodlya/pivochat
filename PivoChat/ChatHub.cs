using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace PivoChat
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> _users = new Dictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            //var userId = Context.GetHttpContext().Request.Query["userId"].ToString();
            if (!string.IsNullOrEmpty(Context.ConnectionId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "111");
            }
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _users.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string recipient, string message, string user)
        {
            await Clients.Group(recipient).SendAsync("SendMessage", message, user);
            
            /*string recipientConnectionId = _users.FirstOrDefault(u => u.Value == recipient).Key;

            if (!string.IsNullOrEmpty(recipientConnectionId))
            {
                await Clients.Client(recipientConnectionId).SendAsync("ReceiveMessage", 
                    Context.GetHttpContext()!.Request.Query["userName"], message);
            }*/
        }
    }
}