using System.Security.Claims;
using ChatApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Hubs
{
    public class ChatHub: Hub
    {
        private readonly MessagesService messagesService;

        public ChatHub(MessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        [Authorize]
        public async Task SendMessage(string message)
        {
            string name = Context.User.FindFirst(ClaimTypes.Name).Value;
            await Clients.All.SendAsync(
                "NewMessage",
                name,
                message);
        }
        
        [Authorize]
        public async Task SendMessageToGroup(string group ,string message)
        {
            string name = Context.User.FindFirst(ClaimTypes.Name).Value;

            this.messagesService.Messages[group].Add(new Tuple<string, string>(name,message));

            await Clients.Groups(group).SendAsync(
                "NewMessage",
                name,
                message);

        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }


    }
}
