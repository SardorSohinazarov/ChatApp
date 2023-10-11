using ChatApp.Api.Data;
using ChatApp.Api.Models.ViewModels;
using ChatApp.Api.Repositories;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
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
            var users = await _chatDbContext.Users.ToListAsync();
            var userViewModels = users.Adapt<List<UserViewModel>>();

            string jwtToken = HttpContext.Request.Headers[HeaderNames.Authorization];

            // Parse the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtTokenObject = tokenHandler.ReadJwtToken(jwtToken[7..]);
            var id = jwtTokenObject.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            foreach (var userViewModel in userViewModels)
            {
                userViewModel.ChatLink = await _chatRepository.GetChatLink(userViewModel.Id.ToString(), id);
            }

            return Ok(userViewModels.Where(x => x.Id.ToString() != id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string userName)
        {
            var user = _chatDbContext.Users.FirstOrDefault(u => u.UserName == userName);

            return Ok(user);
        }
    }
}
