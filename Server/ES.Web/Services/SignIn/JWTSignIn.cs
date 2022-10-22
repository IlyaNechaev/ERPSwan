using ES.Web.Models;

namespace ES.Web.Services;

public partial class SignIn
{
    public string UsingJWT(User user)
    {
        IJwtUtils jwtUtils = 
            (IJwtUtils?)_services.GetService(typeof(IJwtUtils)) ?? 
            throw new NullReferenceException($"Не удалось получить сервис {nameof(IJwtUtils)}");

        var token = jwtUtils.GenerateJSONWebToken(user);

        return token;
    }
}
