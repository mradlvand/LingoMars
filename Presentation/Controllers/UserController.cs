using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dtos;
using Presentation.Framework;
using Presentation.Service;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userLogic;
        public UserController(IUserLogic logic)
        {
            _userLogic = logic;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResult<LoginDtoResponce>> Login(LoginDto dto)
        {
            var res = await _userLogic.Login(dto);

            return Ok(res);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResult<RegisterDtoResponce>> Register(RegisterDto dto)
        {
            var res = await _userLogic.Register(dto);

            return Ok(res);
        }
    }
}
