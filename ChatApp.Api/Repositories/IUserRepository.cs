using ChatApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUserName(string userName);
    }
}
