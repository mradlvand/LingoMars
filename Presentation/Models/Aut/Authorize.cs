using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Model.General;
using Common.Exceptions;
using Data.Context;
using Microsoft.VisualBasic;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Models.Aut
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(params string[] claim) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] { claim };
        }
    }
    public class AuthorizeFilter : IAuthorizationFilter
    {
        readonly string[] _claim;
        private readonly DBLearnContext _context;

        public AuthorizeFilter(DBLearnContext context, params string[] claim)
        {
            _claim = claim;
            _context = context;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            var claimsIndentity = context.HttpContext.User.Identity as ClaimsIdentity;

            if (IsAuthenticated)
            {
                var macAddress = claimsIndentity.FindFirst("MacAddress");

                var userId = Convert.ToInt64(claimsIndentity.FindFirst("UserId").Value);

                var findUser = _context.Users.FirstOrDefault(x => x.Id == userId);

                if (findUser == null)
                    throw new UnauthorizedException("خطای احراز هویت");

                if (findUser != null && findUser.MacAddress != macAddress.Value)
                {
                    throw new UnauthorizedException("خطای احراز هویت");
                }
            }
            else
            {
                throw new UnauthorizedException("خطای احراز هویت");
            }
            return;
        }
    }
}
