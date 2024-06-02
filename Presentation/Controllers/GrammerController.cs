using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dtos;
using Presentation.Framework;
using Presentation.Service;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GrammerController : ControllerBase
    {
        private readonly IGrammerLogic _grammerLogic;
        public GrammerController(IGrammerLogic logic)
        {
            _grammerLogic = logic;
        }

        [HttpPost]
        public async Task<ApiResult<List<GrammerDto>>> GetGrammers(GetSpeakingsDtoRequest dto)
        {
            var res = await _grammerLogic.GetGrammers(dto.LessonId);

            return Ok(res);
        }

        [HttpPost]
        public async Task<ApiResult<GrammerDto>> GetGrammer(GetSpeakingDtoRequest dto)
        {
            var res = await _grammerLogic.GetGrammer(dto.SpeakingId);

            return Ok(res);
        }
    }
}
