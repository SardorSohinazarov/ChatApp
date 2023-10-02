using ChatApp.Api.Data;
using ChatApp.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChatApp.Api.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ChatDbContext _context;

        public ChatRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetChatLink(string user1Id, string user2Id)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync(x => x.UsersKeys.Contains(user1Id) && x.UsersKeys.Contains(user2Id));
           
            if(chat == null)
            {
                chat = new Chat
                {
                    Id = Guid.NewGuid(),
                    UsersKeys = user1Id + user2Id,
                    CreatedDate = DateTime.UtcNow,
                };

                await _context.Chats.AddAsync(chat);
                await _context.SaveChangesAsync();
            }

            return chat.Id.ToString();
        }
    }
}
