using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Presentation.Context;
using Presentation.Dtos;
using System.Linq;

namespace Presentation.Service
{
    public interface ILevelLogic
    {
        Task<List<LevelDto>> GetLevels(int teacherId);
    }

    public class LevelLogic : ILevelLogic
    {
        private readonly ILogger _logger;   
        private  readonly DBLearnContext _context;

        public LevelLogic(DBLearnContext context, ILogger<LevelLogic> logger)
        {
           _context = context;
            _logger = logger;
        }

        public async Task<List<LevelDto>> GetLevels(int teacherId)
        {
            var levels = await _context.Levels.AsQueryable().Where(x => x.TeacherId == teacherId).ToListAsync();

            var res = levels.Select(x => new LevelDto
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                TeacherId = x.TeacherId,
                Title = x.Title,
                CreationDateTime = x.CreationDateTime,
                Description = x.Description,
                Picture = x.Picture,
                Status = x.Status,
                UpdateDateTime = x.UpdateDateTime,
                Order = x.Order,
            }).ToList();

            return res;
        }
    }
}
