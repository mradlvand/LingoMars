using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Data.Context;
using Presentation.Dtos;
using System.Linq;

namespace Presentation.Service
{
    public interface ISpeakingLogic
    {
        Task<List<SpeakingDto>> GetSpeakings(int lessonId);
        Task<SpeakingDto> GetSpeaking(int speakingId);
    }

    public class SpeakingLogic : ISpeakingLogic
    {
        private readonly ILogger _logger;
        private readonly DBLearnContext _context;

        public SpeakingLogic(DBLearnContext context, ILogger<SpeakingLogic> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<SpeakingDto>> GetSpeakings(int lessonId)
        {
            try
            {
                if (lessonId < 0)
                    throw new BadRequestException("آیدی اشتباه می باشد.");

                var lists = await _context.Speakings.AsQueryable().Where(x => x.LessonId == lessonId).ToListAsync();

                var res = lists.Select(x => new SpeakingDto
                {
                    Id = x.Id,
                    Context = x.Context,
                    Header = x.Header,
                    CreationDateTime = x.CreationDateTime,
                    Description = x.Description,
                    Video = x.Video,
                    Status = x.Status,
                    UpdateDateTime = x.UpdateDateTime,
                }).ToList();

                return res;
            }
            catch (Exception ex)
            {
                throw new ServerException(ex);
            }
        }

        public async Task<SpeakingDto> GetSpeaking(int speakingId)
        {
            if (speakingId < 0)
                throw new BadHttpRequestException("levelId is wrong.");

            var resData = await _context.Speakings.FirstOrDefaultAsync(x => x.Id == speakingId);

            if (resData != null)
            {
                return new SpeakingDto()
                {
                    Id = resData.Id,
                    CreationDateTime = resData.CreationDateTime,
                    Description = resData.Description,
                    Video = resData.Video,
                    Status = resData.Status,
                    UpdateDateTime = resData.UpdateDateTime,
                    Header = resData.Header,
                    Context = resData.Context,
                };
            }
            else
            {
                throw new NotFoundException("level not found!.");
            }
        }
    }
}
