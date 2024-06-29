using LearnCourse.Models.Aut;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model.General;
using Model.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LearnCourse.Controllers
{
    public abstract class BaseController : Controller
    {
        protected Category GetUserCategory()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var categoryClaim = identity.FindFirst("UserCategory").Value; // this.User.Claims.First(i => i.Type == "UserCategory").Value;

            Category category = (Category)Enum.Parse(typeof(Category), categoryClaim, true);

            return category;
        }

        protected UserRole GetUserRole()
        {
            var roleClaim = this.User.Claims.First(i => i.Type == "Role").Value;

            var userRole = (UserRole)Enum.Parse(typeof(UserRole), roleClaim, true);

            return userRole;
        }

        protected int GetUserId()
        {
            int userId = Convert.ToInt32(this.User.Claims.First(i => i.Type == "UserId").Value);

            return userId;
        }

        protected string GetUserName()
        {
            return this.User.Claims.First(i => i.Type == "UserName").Value;
        }


        public async Task CreateAuthenticationTicket(User user)
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
            HttpContext.Session.SetString("JWToken", token);
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

        public struct Role
        {
            public const string Admin = "Admin";
            public const string Teacher = "Teacher";
        }
    }
}
