using ChatApp.Api.Data;
using ChatApp.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatApp.Api.Models.DTOs;
using ChatApp.Api.Models;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ChatDbContext _chatDbContext;
        private readonly TokenService _tokenService;

        public AccountController(ChatDbContext chatDbContext, TokenService tokenService)
        {
            _chatDbContext = chatDbContext;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO registerDTO, CancellationToken cancellationToken)
        {
            var userNameExists = await _chatDbContext.Users
                .AsNoTracking()
                .AnyAsync(user => user.UserName == registerDTO.UserName, cancellationToken);

            if (userNameExists)
                return BadRequest($"{nameof(registerDTO.UserName)} already exists");

            var user = new User
            {
                UserName = registerDTO.UserName,
                Password = registerDTO.Password,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                PhoneNumber = registerDTO.PhoneNumber
            };

            await _chatDbContext.Users.AddAsync(user, cancellationToken);
            await _chatDbContext.SaveChangesAsync(cancellationToken);

            return Ok(GenerateJWT(user, cancellationToken));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO loginDTO, CancellationToken cancellationToken)
        {
            var user = await _chatDbContext.Users
                .FirstOrDefaultAsync(user => user.Password == loginDTO.Password && user.UserName == loginDTO.UserName, cancellationToken);

            if (user == null)
                return BadRequest("Incorrect credentials");

            return Ok(GenerateJWT(user, cancellationToken));
        }

        private AuthResponseDTO GenerateJWT(User user, CancellationToken cancellationToken)
        {
            var token = _tokenService.GenerateJWT(user);

            return new AuthResponseDTO
            {
                Name = user.UserName,
                Token = token
            };
        }
    }

    public class AuthResponseDTO
    {
        public string Name { get; set; }
        public string Token { get; set; }
    };
}
