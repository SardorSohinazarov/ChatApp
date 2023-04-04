using ChatApp.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Web.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        { }
        public DbSet<Message> Messages { get; set; }
    }
}
