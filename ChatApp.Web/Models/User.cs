namespace ChatApp.Web.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string? ChatLink { get; set; }
    }
}
