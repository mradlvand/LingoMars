using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Data.Context;
using Presentation.Dtos;
using System.Linq;

namespace Presentation.Service
{
    public interface ILessonLogic
    {
        Task<List<LessonDto>> GetLessons(int levelId);
        Task<LessonDto> GetLesson(int lessonId);
    }

    public class LessonLogic : ILessonLogic
    {
        private readonly ILogger _logger;
        private readonly DBLearnContext _context;

        public LessonLogic(DBLearnContext context, ILogger<LessonLogic> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<LessonDto>> GetLessons(int levelId)
        {
            try
            {
                if (levelId < 0)
                    throw new BadRequestException("آیدی اشتباه می باشد.");

                var lists = await _context.Lessons.AsQueryable().Where(x => x.LevelId == levelId).ToListAsync();

                var res = lists.Select(x => new LessonDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    CreationDateTime = x.CreationDateTime,
                    Description = x.Description,
                    Video = x.Video,
                    Status = x.Status,
                    UpdateDateTime = x.UpdateDateTime,
                    Order = x.Order,
                    StatusId = x.StatusId,
                    LevelId = levelId
                }).ToList();

                return res;
            }
            catch (Exception ex)
            {
                throw new ServerException(ex);
            }
        }

        public async Task<LessonDto> GetLesson(int lessonId)
        {
            if (lessonId < 0)
                throw new BadHttpRequestException("levelId is wrong.");

            var resData = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == lessonId);

            if (resData != null)
            {
                return new LessonDto()
                {
                    Id = resData.Id,
                    CreationDateTime = resData.CreationDateTime,
                    Description = resData.Description,
                    Video = resData.Video,
                    Status = resData.Status,
                    UpdateDateTime = resData.UpdateDateTime,
                    Order = resData.Order,
                    Title = resData.Title,
                    LevelId = resData.LevelId,
                    StatusId = resData.StatusId,
                };
            }
            else
            {
                throw new NotFoundException("level not found!.");
            }
        }
    }
}
