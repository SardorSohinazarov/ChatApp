using ChatApp.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : BaseController
    {
        private readonly IMessageRepository _messageRepository;

        public MessagesController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpGet("groups/{group}")]
        public async Task<IActionResult> GetAllByChatLink(string group)
        {
            var listOfMessages = await _messageRepository.GetAllByChatId(group);

            if (listOfMessages == null)
                return NotFound();

            return Ok( listOfMessages.ToList());
        }
    }
}
