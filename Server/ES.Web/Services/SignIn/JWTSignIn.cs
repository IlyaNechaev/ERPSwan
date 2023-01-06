using ES.Web.Models;

namespace ES.Web.Services;

public partial class SignIn
{
    public async Task<string> UsingJWTAsync(string login, string password)
    {
        var userService = 
            _services.GetService<IUserService>() ??
            throw new NullReferenceException($"Не удалось получить сервис {nameof(IUserService)}");

        var securityService = 
            _services.GetService<ISecurityService>() ??
            throw new NullReferenceException($"Не удалось получить сервис {nameof(ISecurityService)}");

        if (!await userService.ExistsUserAsync(login)) 
        {
            throw new ArgumentException($"Неверный логин или пароль");
        }

        var user = await userService.GetUserAsync(login);
        if (!securityService.ValidatePassword(password, user.PasswordHash))
        {
            throw new ArgumentException($"Неверный логин или пароль");
        }

        IJwtUtils jwtUtils = 
            _services.GetService<IJwtUtils>() ?? 
            throw new NullReferenceException($"Не удалось получить сервис {nameof(IJwtUtils)}");

        var token = jwtUtils.GenerateJSONWebToken(user);

        return token;
    }
}
