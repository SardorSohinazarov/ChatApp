namespace ChatApp.Api.Models.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
