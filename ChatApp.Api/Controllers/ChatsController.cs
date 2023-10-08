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
    }
}
