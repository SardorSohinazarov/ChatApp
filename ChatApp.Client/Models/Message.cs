namespace ChatApp.Client.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public string SenderName { get; set; }
        public string ChatLink { get; set; }
    }
}
