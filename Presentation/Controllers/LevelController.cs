using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dtos;
using Presentation.Service;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LevelController : ControllerBase
    {
        private readonly ILevelLogic _levelLogic;
        public LevelController(ILevelLogic logic)
        {
            _levelLogic = logic;
        }

        [HttpPost]
        public async Task<List<LevelDto>> GetLevels(GetLevelDtoRequest dto)
        {
            var res = await _levelLogic.GetLevels(dto.TeacherId);

            return res;
        }
    }
}
