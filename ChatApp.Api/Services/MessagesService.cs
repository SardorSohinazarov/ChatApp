using ChatApp.Api.Models;

namespace ChatApp.Api.Services
{
    public class MessagesService
    {
        public Dictionary<string, List<Message>> Messages 
            = new Dictionary<string, List<Message>>()
            {
                {"Sinfdoshlar", new List<Message>()},
                {"Kursdoshlar", new List<Message>()},
                {"Hamkasblar", new List<Message>() } 
            };
    }
}
