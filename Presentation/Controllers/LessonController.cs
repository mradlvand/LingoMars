using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dtos;
using Presentation.Framework;
using Presentation.Service;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonLogic _lessonLogic;
        public LessonController(ILessonLogic logic)
        {
            _lessonLogic = logic;
        }

        [HttpPost]
        public async Task<ApiResult<List<LessonDto>>> GetLessons(GetLessonsDtoRequest dto)
        {
            var res = await _lessonLogic.GetLessons(dto.LevelId);

            return Ok(res);
        }

        [HttpPost]
        public async Task<ApiResult<LessonDto>> GetLesson(GetLessonDtoRequest dto)
        {
            var res = await _lessonLogic.GetLesson(dto.LessonId);

            return Ok(res);
        }
    }
}
