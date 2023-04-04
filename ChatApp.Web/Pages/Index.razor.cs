using ChatApp.Web.Data;
using ChatApp.Web.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Web.Pages
{
    public partial class Index
    {
        private readonly ChatDbContext _context;

        public Index(ChatDbContext context)
        {
            _context = context;
        }

        private string? message { get; set; }
        private List<string>? messages = new List<string>();
        private HubConnection? hubConnection;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7183/chathub")
                .Build();

            messages = _context.Messages.Select(message => $"{message.UserName}:{message.Text}").ToList();

            hubConnection.On<string, string>("NewMessage", GetMessage);
            await hubConnection.StartAsync();
        }

        private void GetMessage(string id, string message)
        {
            messages.Add($"{id}:{message}");
            _context.Messages.Add(new Message
            {
                Text = message,
                UserName = id,
                ChatId = 0,
            }) ;
            StateHasChanged();
        }

        private async Task SendMessage()
        {
            if (hubConnection.State == HubConnectionState.Connected)
            {
                await hubConnection.InvokeAsync(
                    methodName: nameof(SendMessage),
                    message ?? "...Empty Message...");
            }
        }
    }
}
