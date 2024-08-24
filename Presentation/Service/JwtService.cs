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
    public interface IJwtService
    {
        Task<string> CreateToken(User user);
    }

    public class JwtService : IJwtService
    {
        private readonly ILogger _logger;

        public JwtService(ILogger<GrammerLogic> logger)
        {
            _logger = logger;
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

            _claim = new Claim("UserName", user.FirstName.ToString() + " " + user.LastName.ToString());
            claims.Add(_claim);

            return claims.AsEnumerable<Claim>();
        }
    }
}
