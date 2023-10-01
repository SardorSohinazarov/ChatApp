using System.ComponentModel.DataAnnotations;

namespace ChatApp.Api.Models.DTOs
{
    public class UpdateUserDTO
    {
        [Required]
        public string? FirstName { get; set; }
        public string? UserName { get; set; }
        public string? LastName { get; set; }
    }
}
