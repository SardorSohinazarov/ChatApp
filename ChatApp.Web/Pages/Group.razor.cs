using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;

namespace ChatApp.Web.Pages
{
    public partial class Group
    {
        [Parameter]
        public string GroupName { get; set; }

        public string UserName { get; set; }
        private string? message { get; set; }
        private List<Message> messages = new List<Message>();
        private HubConnection? hubConnection;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:7183/chathub")
                    .Build();

                hubConnection.On<string, Message>("NewMessage", GetMessage);
                await hubConnection.StartAsync();
                await hubConnection.InvokeAsync("JoinGroup", GroupName);

                messages = await Http.GetFromJsonAsync<List<Message>>(
                    $"https://localhost:7183/api/Messages/groups/{GroupName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void GetMessage(string id, Message message)
        {
            messages.Add(message);
            UserName = id;
            StateHasChanged();
        }

        private async Task SendMessage()
        {
            if (hubConnection.State == HubConnectionState.Connected)
            {
                await hubConnection.InvokeAsync(
                    methodName: "SendMessageToGroup",
                    GroupName,
                    message ?? "...Empty message...");
            }
            else
            {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:7183/chathub")
                    .Build();

                await hubConnection.InvokeAsync(
                    methodName: "SendMessageToGroup",
                    GroupName,
                    message ?? "...Empty message...");
            }
        }
    }
}
