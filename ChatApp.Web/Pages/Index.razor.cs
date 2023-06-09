﻿using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Web.Pages
{
    public partial class Index
    {
        private string? message { get; set; }
        private readonly List<string> messages = new List<string>();
        private HubConnection? hubConnection;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7183/chathub")
                .Build();

            hubConnection.On<string, string>("NewMessage", GetMessage);
            await hubConnection.StartAsync();
        }

        private void GetMessage(string id, string message)
        {
            messages.Add($"{id}:{message}");
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
