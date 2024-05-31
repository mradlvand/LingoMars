using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Data.Context;
using Presentation.Dtos;
using System.Linq;

namespace Presentation.Service
{
    public interface ILevelLogic
    {
        Task<List<LevelDto>> GetLevels(int teacherId);
        Task<LevelDto> GetLevel(int levelId);
    }

    public class LevelLogic : ILevelLogic
    {
        private readonly ILogger _logger;
        private readonly DBLearnContext _context;

        public LevelLogic(DBLearnContext context, ILogger<LevelLogic> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<LevelDto>> GetLevels(int teacherId)
        {
            try
            {
                if (teacherId < 0)
                    throw new BadRequestException("آیدی اشتباه می باشد.");

                var levels = await _context.Levels.AsQueryable().Where(x => x.TeacherId == teacherId).ToListAsync();

                var res = levels.Select(x => new LevelDto
                {
                    Id = x.Id,
                    CategoryId = x.CategoryId,
                    TeacherId = x.TeacherId,
                    Title = x.Title,
                    CreationDateTime = x.CreationDateTime,
                    Description = x.Description,
                    Video = x.Picture,
                    Status = x.Status,
                    UpdateDateTime = x.UpdateDateTime,
                    Order = x.Order,
                }).ToList();

                return res;
            }
            catch (Exception ex)
            {
                throw new ServerException(ex);
            }
        }

        public async Task<LevelDto> GetLevel(int levelId)
        {
            if (levelId < 0)
                throw new BadHttpRequestException("levelId is wrong.");

            var level = await _context.Levels.FirstOrDefaultAsync(x => x.Id == levelId);

            if (level != null)
            {
                return new LevelDto()
                {
                    Id = level.Id,
                    CategoryId = level.CategoryId,
                    CreationDateTime = level.CreationDateTime,
                    Description = level.Description,
                    Video = level.Picture,
                    Status = level.Status,
                    UpdateDateTime = level.UpdateDateTime,
                    Order = level.Order,
                    TeacherId = level.TeacherId,
                    Title = level.Title,
                };
            }
            else
            {
                throw new NotFoundException("level not found!.");
            }
        }
    }
}
