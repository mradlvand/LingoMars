using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dtos;
using Presentation.Framework;
using Presentation.Service;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
        }

        [HttpPost]
        public async Task<ApiResult> Login(LoginDto dto)
        {
            return Ok();
        }
    }
}
