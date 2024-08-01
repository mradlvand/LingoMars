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
        Task<RegisterDtoResponce> Register(RegisterDto dto);
    }

    public class UserLogic : IUserLogic
    {
        private readonly ILogger _logger;
        private readonly DBLearnContext _context;
        private readonly JwtService _jwtService;

        public UserLogic(DBLearnContext context, ILogger<GrammerLogic> logger, JwtService jwtService)
        {
            _context = context;
            _logger = logger;
            _jwtService = jwtService;
        }

        public async Task<RegisterDtoResponce> Register(RegisterDto dto)
        {
            try
            {
                bool findUser = _context.Users.Any(x => x.UserName == dto.PhoneNumber &&
                    x.UserCategory == dto.ApplicationType && 
                    x.Role==Model.General.UserRole.User);

                if (findUser)
                    throw new BadRequestException("کاربر وجود دارد لطفا ورود فرمایید.");

                var model = new User()
                {
                    UserName = dto.PhoneNumber,
                    UserCategory = dto.ApplicationType,
                    Status = true,
                    Role = Model.General.UserRole.User,
                    UpdateTime = DateTime.Now,
                    Password = "123456"
                };

                var res = new RegisterDtoResponce();
                res.Token = await _jwtService.CreateToken(model);

                _context.Users.Add(model);
                _context.SaveChanges();

                return res;
            }
            catch (Exception ex)
            {
                throw new ServerException(ex);
            }
        }

    }
}
