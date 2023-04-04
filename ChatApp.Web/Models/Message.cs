namespace ChatApp.Web.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public int ChatId { get; set; }
    }
}
