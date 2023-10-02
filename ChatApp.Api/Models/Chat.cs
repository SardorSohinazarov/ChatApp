using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Api.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string UsersKeys { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
