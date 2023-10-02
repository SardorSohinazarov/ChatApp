using ChatApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Api.Repositories
{
    public interface IUserRepository
    {
        Task<ChatUser> GetByUserName(string userName);
    }
}
