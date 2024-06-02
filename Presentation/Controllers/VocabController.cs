using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dtos;
using Presentation.Framework;
using Presentation.Service;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VocabController : ControllerBase
    {
        private readonly IVocabLogic _vocabLogic;
        public VocabController(IVocabLogic logic)
        {
            _vocabLogic = logic;
        }

        [HttpPost]
        public async Task<ApiResult<List<VocabularyDto>>> GetVocabs(GetVocabsDtoRequest dto)
        {
            var res = await _vocabLogic.GetVocabs(dto.LessonId);

            return Ok(res);
        }

        [HttpPost]
        public async Task<ApiResult<VocabularyDto>> GetVocab(GetVocabDtoRequest dto)
        {
            var res = await _vocabLogic.GetVocab(dto.VocabId);

            return Ok(res);
        }
    }
}
