using ChatApp.Api.Data;
using ChatApp.Api.Models;
using ChatApp.Api.Models.ViewModels;
using ChatApp.Api.Repositories;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ChatDbContext _chatDbContext;
        private readonly UserManager<ChatUser> _userManager;
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;

        public UsersController(
            UserManager<ChatUser> userManager,
            IChatRepository chatRepository,
            ChatDbContext chatDbContext,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _chatRepository = chatRepository;
            _chatDbContext = chatDbContext;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = users.Adapt<List<UserViewModel>>();
            var user = await _userManager.GetUserAsync(User);

            foreach (var userViewModel in userViewModels)
            {
                userViewModel.ChatLink = await _chatRepository.GetChatLink(userViewModel.Id.ToString(), user?.Id.ToString());
            }

            return Ok(userViewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string userName)
        {
            var user = _chatDbContext.AspNetUsers.FirstOrDefault(u => u.UserName == userName);

            return Ok(user);
        }
    }
}
