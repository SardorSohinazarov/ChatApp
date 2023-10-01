using ChatApp.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Api.Data
{
    public class ChatDbContext : IdentityDbContext<ChatUser, UserRole, Guid>
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
            Database.Migrate();
        }
    }
}
