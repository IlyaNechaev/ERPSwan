using ES.Web.Models;
using System;
using System.Security.Claims;

namespace ES.Web.Services;
public interface IJwtUtils
{
    public string GenerateJSONWebToken(User user);

    public ClaimsPrincipal ValidateToken(string token);

}