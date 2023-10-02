using ChatApp.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;

        public ChatsController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetChatLink(string user1Id,string user2Id)
        {
            var chatId = await _chatRepository.GetChatLink(user1Id,user2Id);
            return Ok(chatId);
        }
    }
}
