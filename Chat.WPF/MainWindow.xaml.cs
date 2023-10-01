using Chat.WPF.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Chat.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string GroupName { get; set; }

        public string UserName { get; set; }
        private string? message { get; set; }
        private List<Message> messages = new List<Message>();
        private HubConnection? hubConnection;
        private HttpClient http;
        public MainWindow()
        {
            InitializeComponent();
            OnInitialized().Start();
        }

        public async Task OnInitialized()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7183/chathub")
                .Build();

            hubConnection.On<string, Message>("NewMessage", GetMessage);
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("JoinGroup", GroupName);

            messages = await http.GetFromJsonAsync<List<Message>>(
                $"https://localhost:7183/api/Account/groups/{GroupName}");
        }

        private void GetMessage(string id, Message message)
        {
            messages.Add(message);
            UserName = id;

            //Refresh
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtMessage.Text = messages[0].Text;
        }
    }
}
