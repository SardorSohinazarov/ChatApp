using ChatApp.Api.Data;
using ChatApp.Api.Models.ViewModels;
using ChatApp.Api.Repositories;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly ChatDbContext _chatDbContext;
        private readonly IChatRepository _chatRepository;

        public UsersController(
            IChatRepository chatRepository,
            ChatDbContext chatDbContext
            )
        {
            _chatRepository = chatRepository;
            _chatDbContext = chatDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string id = base.UserId;
            var users = await _chatDbContext.Users.ToListAsync();
            var userViewModels = users.Adapt<List<UserViewModel>>();

            foreach (var userViewModel in userViewModels)
            {
                userViewModel.ChatLink = await _chatRepository.GetChatLink(userViewModel.Id.ToString(), id);
            }

            var listOfUsers = userViewModels.Where(x => x.Id.ToString() != id).ToList();
            return Ok(listOfUsers);
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetById(string userName)
        {
            var user = _chatDbContext.Users.FirstOrDefault(u => u.UserName == userName);

            return Ok(user);
        }
    }
}
