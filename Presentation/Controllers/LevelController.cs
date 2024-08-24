using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dtos;
using Presentation.Framework;
using Presentation.Models.Aut;
using Presentation.Service;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LevelController : ControllerBase
    {
        private readonly ILevelLogic _levelLogic;
        public LevelController(ILevelLogic logic)
        {
            _levelLogic = logic;
        }

        [HttpPost]
        public async Task<ApiResult<List<LevelDto>>> GetLevels(GetLevelsDtoRequest dto)
        {
            var res = await _levelLogic.GetLevels(dto.TeacherId);

            return Ok(res);
        }

        [HttpPost]
        public async Task<ApiResult<LevelDto>> GetLevel(GetLevelDtoRequest dto)
        {
            var res = await _levelLogic.GetLevel(dto.LevelId);

            return Ok(res);
        }
    }
}
