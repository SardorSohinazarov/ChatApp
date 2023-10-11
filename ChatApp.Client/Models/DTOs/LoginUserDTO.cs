using System.ComponentModel.DataAnnotations;

namespace ChatApp.Client.Models.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
