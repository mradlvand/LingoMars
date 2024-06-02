using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dtos;
using Presentation.Framework;
using Presentation.Service;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SpeakingController : ControllerBase
    {
        private readonly ISpeakingLogic _speakingLogic;
        public SpeakingController(ISpeakingLogic logic)
        {
            _speakingLogic = logic;
        }

        [HttpPost]
        public async Task<ApiResult<List<VocabularyDto>>> GetVocabs(GetSpeakingsDtoRequest dto)
        {
            var res = await _speakingLogic.GetSpeakings(dto.LessonId);

            return Ok(res);
        }

        [HttpPost]
        public async Task<ApiResult<VocabularyDto>> GetVocab(GetSpeakingDtoRequest dto)
        {
            var res = await _speakingLogic.GetSpeaking(dto.SpeakingId);

            return Ok(res);
        }
    }
}
