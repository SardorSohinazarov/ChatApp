using System.Security.Claims;
using ChatApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(SignInManager<IdentityUser> signInManager) =>
            this.signInManager = signInManager;

        [HttpPost]
        public async Task<IActionResult> Login(string username)
        {
            var user = new IdentityUser(username);
            await this.signInManager.SignInAsync(user, isPersistent: true);

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetProfile() =>
            Ok($"Username : {User.FindFirst(ClaimTypes.Name)?.Value}");

        [HttpGet("groups")]
        public IActionResult GetGroups() => 
        Ok(new List<string>() {
            "Sinfdoshlar",
            "Kursdoshlar",
            "Hamkasblar"
        });


        [HttpGet("groups/{group}")]
        public IActionResult GetGroupMessages(
            string group,
            [FromServices] MessagesService messagesService)
        {
            return Ok(messagesService.Messages[group]);
        }
    }
}
