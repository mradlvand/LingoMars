using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Data.Context;
using Presentation.Dtos;
using System.Linq;
using Model.Models;

namespace Presentation.Service
{
    public interface IUserProgressLogic
    {
        Task<GetUserProgressDtoResponce> GetUserProgress(GetUserProgressDtoRequest dto);
        Task AddUserProgress(UserProgressDto dto);
    }

    public class UserProgressLogic : IUserProgressLogic
    {
        private readonly ILogger _logger;
        private readonly DBLearnContext _context;

        public UserProgressLogic(DBLearnContext context, ILogger<LevelLogic> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GetUserProgressDtoResponce> GetUserProgress(GetUserProgressDtoRequest dto)
        {
            try
            {
                if (dto.UserId < 0 || dto.UserId == null)
                    throw new BadRequestException("آیدی اشتباه می باشد.");

                var userProgresses = await _context.UserProgresses.AsQueryable().Where(x => x.UserId == dto.UserId).ToListAsync();

                var res = userProgresses.Select(x => new UserProgressDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserProgressType = x.UserProgressType,
                    Time = x.Time,
                    Description = x.Description,
                    DateTime = x.DateTime
                }).ToList();

                return new GetUserProgressDtoResponce { UserProgress = res };
            }
            catch (Exception ex)
            {
                throw new ServerException(ex);
            }
        }

        public async Task AddUserProgress(UserProgressDto dto)
        {

            try
            {
                var findUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);

                if (findUser == null)
                    throw new BadRequestException("کاربر وجود دارد لطفا ورود فرمایید.");

                var model = new UserProgress()
                {
                    UserId = findUser.Id,
                    UserProgressType = dto.UserProgressType,
                    Time = dto.Time,
                    DateTime = dto.DateTime,
                    Description = dto.Description,
                };

                _context.UserProgresses.Add(model);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new ServerException(ex);
            }
        }
    }
}
