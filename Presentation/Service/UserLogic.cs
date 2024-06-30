using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Data.Context;
using Presentation.Dtos;
using System.Linq;
using Model.Models;

namespace Presentation.Service
{
    public interface IUserLogic
    {
        Task Register(RegisterDto dto);
    }

    public class UserLogic : IUserLogic
    {
        private readonly ILogger _logger;
        private readonly DBLearnContext _context;

        public UserLogic(DBLearnContext context, ILogger<GrammerLogic> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Register(RegisterDto dto)
        {
            try
            {
                bool findUser = _context.Users.Any(x => x.UserName == dto.PhoneNumber && x.UserCategory == dto.Category);

                if (findUser)
                    throw new BadRequestException("کاربر وجود دارد لطفا ورود فرمایید.");

                var model = new User()
                {
                    UserName = dto.PhoneNumber,
                    UserCategory = dto.Category,
                    Status = true,
                    Role = Model.General.UserRole.User,
                    UpdateTime = DateTime.Now,
                    Password = "123456"
                };

                _context.Users.Add(model);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ServerException(ex);
            }
        }

    }
}
