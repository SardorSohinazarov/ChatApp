using System.Security.Claims;
using ChatApp.Api.Models;
using ChatApp.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Hubs
{
    [Authorize]
    public class ChatHub: Hub
    {
        private readonly IMessageRepository _messageRepository;

        public ChatHub(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [Authorize]
        public async Task SendMessageToGroup(string groupLink ,string message)
        {
            string name = Context.User?.FindFirst(ClaimTypes.Name)?.Value;

            var messageObject = new Message
            {
                Text = message,
                SenderName = name ?? "Unknown user",
                ChatLink = groupLink,
            };

            //aslida service bo'lishi kerak lekin erindim
            await _messageRepository.Add(messageObject);

            await Clients.Groups(groupLink).SendAsync(method:"NewMessage",name,messageObject);
        }

        public async Task JoinGroup(string groupName) =>
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }
}
