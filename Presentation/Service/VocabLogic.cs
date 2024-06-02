using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Data.Context;
using Presentation.Dtos;
using System.Linq;

namespace Presentation.Service
{
    public interface IVocabLogic
    {
        Task<List<VocabularyDto>> GetVocabs(int lessonId);
        Task<VocabularyDto> GetVocab(int lessonId);
    }

    public class VocabLogic : IVocabLogic
    {
        private readonly ILogger _logger;
        private readonly DBLearnContext _context;

        public VocabLogic(DBLearnContext context, ILogger<VocabLogic> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<VocabularyDto>> GetVocabs(int lessonId)
        {
            try
            {
                if (lessonId < 0)
                    throw new BadRequestException("آیدی اشتباه می باشد.");

                var lists = await _context.Vocabularies.AsQueryable().Where(x => x.LessonId == lessonId).ToListAsync();

                var res = lists.Select(x => new VocabularyDto
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

        public async Task<VocabularyDto> GetVocab(int vocabId)
        {
            if (vocabId < 0)
                throw new BadHttpRequestException("levelId is wrong.");

            var resData = await _context.Vocabularies.FirstOrDefaultAsync(x => x.Id == vocabId);

            if (resData != null)
            {
                return new VocabularyDto()
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
