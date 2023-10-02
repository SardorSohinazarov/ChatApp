using ChatApp.Api.Data;
using ChatApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _context;

        public UserRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<ChatUser> GetByUserName(string userName)
        {
            var user = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.UserName == userName);

            return user;
        }
    }
}
