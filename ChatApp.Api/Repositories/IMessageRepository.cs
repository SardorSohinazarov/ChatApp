using ChatApp.Api.Models;

namespace ChatApp.Api.Repositories
{
    public interface IMessageRepository
    {

        Task<List<Message>> GetAllByChatId(string chatLink);

        Task Add(Message message);
    }
}
