namespace ChatApp.Api.Services
{
    public class MessagesService
    {
        public Dictionary<string, List<Tuple<string, string>>> Messages 
            = new Dictionary<string, List<Tuple<string, string>>>()
            {
                {"Sinfdoshlar", new List<Tuple<string, string>>()},
                {"Kursdoshlar", new List<Tuple<string, string>>()},
                {"Hamkasblar", new List<Tuple<string, string>>() } 
            };
    }
}
