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
        private List<string> messages = new List<string>();
        private HubConnection? hubConnection;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7183/chathub")
                .Build();

            hubConnection.On<string, string>("NewMessage", GetMessage);
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("JoinGroup", GroupName);

            messages = await Http.GetFromJsonAsync<List<string>>(
                $"https://localhost:7183/api/Account/groups/{GroupName}");
        }

        private void GetMessage(string id, string message)
        {
            messages.Add($"{id}:{message}");
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
        }
    }
}
