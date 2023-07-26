using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SportWave.Services.Contracts;

namespace SportWave.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService chatService;

        public ChatHub(IChatService chatService)
        {
            this.chatService = chatService;
        }

        [Authorize(Roles = "Admin, Employee")]
        public async Task SendMessage(string message)
        {
            var userName = Context.User.Identity.Name.Split("@").ToArray()[0];
            await Clients.All.SendAsync("RecieveMessage", userName, message);

            await chatService.SaveMsgAsync(userName, message);
        }
    }
}
