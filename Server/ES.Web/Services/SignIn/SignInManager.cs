using ES.Web.Models;

namespace ES.Web.Services;

public class SignInManager
{
    private IServiceProvider _services;
    private IUserService _userService;
    private ISecurityService _securityService;

    public SignInManager(IServiceProvider services, IUserService userService, ISecurityService securityService)
    {
        _services = services;
        _userService = userService;
        _securityService = securityService;
    }

    public SignIn SignIn()
    {
        return new SignIn(_services);
    }

    public async Task<User> Register(RegisterEditModel model)
    {
        if (await _userService.ExistsUserAsync(model.Login))
        {
            throw new ArgumentException($"Пользователь {model.Login} уже существует");
        }

        var newUser = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Login = model.Login,
            PasswordHash = _securityService.Encrypt(model.Password),
            BirthDay = model.BirthDay,
            Age = DateTime.Now.Year - model.BirthDay.Year,
            Role = UserRole.WORKER
        };
        await _userService.AddUserAsync(newUser);

        return newUser;
    }
}

public partial class SignIn
{
    private IServiceProvider _services;

    public SignIn(IServiceProvider services)
    {
        _services = services;
    }
}