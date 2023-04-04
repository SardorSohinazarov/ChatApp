using System.Security.Claims;
using ChatApp.Api.Data;
using ChatApp.Api.Models;
using ChatApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Hubs
{
    public class ChatHub: Hub
    {
        private readonly ChatDbContext chatDbContext;
        private readonly MessagesService messagesService;

        public ChatHub(
            ChatDbContext chatDbContext, 
            MessagesService messagesService)
        {
            this.chatDbContext = chatDbContext;
            this.messagesService = messagesService;
        }

        [Authorize]
        public async Task SendMessage(Message message)
        {
            string name = Context.User.FindFirst(ClaimTypes.Name).Value;
            await Clients.All.SendAsync(method:"NewMessage", name, message.Text);
        }
        
        [Authorize]
        public async Task SendMessageToGroup(string group ,Message message)
        {
            string name = Context.User.FindFirst(ClaimTypes.Name).Value;
            message.UserName = name;
            this.chatDbContext.Messages.Add(message);
            this.messagesService.Messages[group].Add(new Tuple<string, string>(name,message.Text));
            await Clients.Groups(group).SendAsync(method:"NewMessage", name,message.Text);
        }

        public async Task JoinGroup(string groupName) =>
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }
}
