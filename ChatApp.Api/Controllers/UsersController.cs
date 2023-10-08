using ChatApp.Api.Data;
using ChatApp.Api.Models;
using ChatApp.Api.Models.ViewModels;
using ChatApp.Api.Repositories;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ChatDbContext _chatDbContext;
        private readonly UserManager<ChatUser> _userManager;
        private readonly IChatRepository _chatRepository;

        public UsersController(
            UserManager<ChatUser> userManager,
            IChatRepository chatRepository,
            ChatDbContext chatDbContext,
            IUserRepository userRepository
            )
        {
            _userManager = userManager;
            _chatRepository = chatRepository;
            _chatDbContext = chatDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = users.Adapt<List<UserViewModel>>();
            //var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach (var userViewModel in userViewModels)
            {
                userViewModel.ChatLink = await _chatRepository.GetChatLink(userViewModel.Id.ToString(), id);
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
