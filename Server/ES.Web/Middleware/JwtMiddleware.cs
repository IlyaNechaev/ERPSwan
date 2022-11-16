using ES.Web.Extensions;
using ES.Web.Models;
using ES.Web.Services;
using Microsoft.AspNetCore.Http;

namespace ES.Web.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, 
                                 IUserService userService,
                                 IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last() ?? 
                context.Request.Cookies["token"];
            var principal = jwtUtils.ValidateToken(token);

            if (principal is not null)
            {
                try
                {
                    var userId = Guid.Parse(principal.GetClaim(ClaimKey.Id));
                    context.Items["User"] = await userService.GetUserAsync(userId);
                }
                catch { }
            }

            await _next(context);
        }
    }
}
