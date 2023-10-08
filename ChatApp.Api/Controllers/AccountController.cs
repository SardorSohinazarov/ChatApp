using ChatApp.Api.Models;
using ChatApp.Api.Models.DTOs;
using ChatApp.Api.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ChatUser> _signInManager;
        private readonly UserManager<ChatUser> _userManager;
        private readonly IUserRepository _userRepository;

        public AccountController(
            SignInManager<ChatUser> signInManager,
            UserManager<ChatUser> userManager,
            IUserRepository userRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var taskSignResult = await _signInManager.PasswordSignInAsync(loginUserDTO.UserName, loginUserDTO.Password, true, true);

            if (!taskSignResult.Succeeded)
            {
                return BadRequest();
            }

            var user = await _userRepository.GetByUserName(userName: loginUserDTO.UserName);

            return Ok(user);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(RegisterUserDTO registerUserDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = registerUserDTO.Adapt<ChatUser>();

            var registeredUser = await _userManager.CreateAsync(user, registerUserDTO.Password);

            if (!registeredUser.Succeeded)
                return BadRequest();

            await _signInManager.SignInAsync(user, true);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _userManager.GetUserAsync(User);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDTO updateUserDTO)
        {
            var user = await _userManager.GetUserAsync(User);

            user.UserName = updateUserDTO.UserName;
            user.FirstName = updateUserDTO.FirstName;
            user.LastName = updateUserDTO.LastName;

            await _userManager.UpdateAsync(user);

            return Ok();
        }
    }
}
