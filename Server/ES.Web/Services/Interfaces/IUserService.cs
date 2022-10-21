using ES.Web.Models;

namespace ES.Web.Services;

public interface IUserService
{
    public Task RegisterUser(User newUser);

    public Task LoginUser(string login, string password);
}