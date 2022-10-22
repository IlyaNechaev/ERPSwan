using ES.Web.Models;

namespace ES.Web.Services;

public interface IUserService
{
    public Task AddUserAsync(User newUser);
    public Task RemoveUserAsync(Guid userId);

    public Task<User> GetUserAsync(string login);
    public Task<User> GetUserAsync(Guid userId);

    public Task<bool> ExistsUserAsync(string login);
    public Task<bool> ExistsUserAsync(Guid userId);
}