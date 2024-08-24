using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Data.Context;
using Presentation.Dtos;
using System.Linq;
using Model.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Presentation.Models.Aut;

namespace Presentation.Service
{
    public interface IUserLogic
    {
        Task<LoginDtoResponce> Login(LoginDto dto);
        Task<RegisterDtoResponce> Register(RegisterDto dto);
    }

    public class UserLogic : IUserLogic
    {
        private readonly DBLearnContext _context;

        public UserLogic(DBLearnContext context)
        {
            _context = context;
        }

        public async Task<LoginDtoResponce> Login(LoginDto dto)
        {
            try
            {
                var findUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == dto.PhoneNumber &&
                    x.UserCategory == dto.ApplicationType &&
                    x.Role == Model.General.UserRole.User);

                if (findUser == null)
                    throw new BadRequestException("کاربر وجود ندارد لطفا ثبت نام فرمایید.");

                if (!string.IsNullOrEmpty(findUser.MacAddress) && findUser.MacAddress != dto.MacAddress)
                    throw new BadRequestException("از دستگاه قبلی خود خارج شوید.");

                var res = new LoginDtoResponce();
                res.Token = await CreateToken(findUser);

                return res;
            }
            catch (Exception ex)
            {
                throw new ServerException(ex);
            }
        }

        public async Task<RegisterDtoResponce> Register(RegisterDto dto)
        {
            try
            {
                bool findUser = _context.Users.Any(x => x.UserName == dto.PhoneNumber &&
                    x.UserCategory == dto.ApplicationType &&
                    x.Role == Model.General.UserRole.User);

                if (findUser)
                    throw new BadRequestException("کاربر وجود دارد لطفا ورود فرمایید.");

                var model = new User()
                {
                    UserName = dto.PhoneNumber,
                    UserCategory = dto.ApplicationType,
                    Status = true,
                    Role = Model.General.UserRole.User,
                    UpdateTime = DateTime.Now,
                    Password = "123456",
                    FirstName = "dfdf",
                    LastName = "dsds"
                };

                var res = new RegisterDtoResponce();
                res.Token = await CreateToken(model);

                _context.Users.Add(model);
                _context.SaveChanges();

                return res;
            }
            catch (Exception ex)
            {
                throw new ServerException(ex);
            }
        }


        public async Task<string> CreateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(SiteKeys.Token);
            var JWToken = new JwtSecurityToken(
            issuer: SiteKeys.WebSiteDomain,
            audience: SiteKeys.WebSiteDomain,
            claims: GetUserClaims(user),
            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
            expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(JWToken);

            return token;
            //HttpContext.Session.SetString("JWToken", token);
        }


        private IEnumerable<Claim> GetUserClaims(User user)
        {
            List<Claim> claims = new List<Claim>();
            Claim _claim;
            _claim = new Claim(ClaimTypes.Name, user.UserName);
            claims.Add(_claim);

            _claim = new Claim("UserId", user.Id.ToString());
            claims.Add(_claim);

            _claim = new Claim("UserCategory", user.UserCategory.ToString());
            claims.Add(_claim);

            _claim = new Claim("Role", user.Role.ToString());
            claims.Add(_claim);

            _claim = new Claim("UserName", user.UserName);
            claims.Add(_claim);

            _claim = new Claim("MacAddress", user.MacAddress);
            claims.Add(_claim);

            return claims.AsEnumerable<Claim>();
        }

    }
}
