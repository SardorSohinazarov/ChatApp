using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatApp.Api.Controllers
{
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        private string _userId;

        public string UserId
        {
            get
            {
                if (_userId is null)
                {
                    _userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                }
                return _userId;
            }
        }

    }
}
