using ChatApp.Api.Data;
using ChatApp.Api.Models;

namespace ChatApp.Api.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _context;

        public MessageRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetAllByChatId(string chatLink)
        {
            var messages = _context.Messages.Where(x => x.ChatLink == chatLink).ToList();
            return messages;
        }

        public async Task Add(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }
    }
}
