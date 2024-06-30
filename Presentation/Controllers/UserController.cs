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
        private readonly IUserLogic _userLogic;
        public UserController(IUserLogic logic)
        {
            _userLogic = logic;
        }

        [HttpPost]
        public async Task<ApiResult> Login(LoginDto dto)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ApiResult> Register(RegisterDto dto)
        {
            await _userLogic.Register(dto);

            return Ok();
        }
    }
}
