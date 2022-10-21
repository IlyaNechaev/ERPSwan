using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ES.Web.Extensions
{
    public static class AuthenticationExtensions
    {
        public static string GetClaim(this ClaimsPrincipal claimsPrincipal, string claimKey)
        {
            var cl = claimsPrincipal?.FindFirst(claimKey)?.Value ?? string.Empty;

            return cl;
        }

        public static string GetCookieValue(this HttpRequest request, string cookieKey)
        {
            if (request.Cookies.TryGetValue(cookieKey, out var cookieValue))
            {
                return cookieValue;
            }
            return string.Empty;
        }
    }
}
