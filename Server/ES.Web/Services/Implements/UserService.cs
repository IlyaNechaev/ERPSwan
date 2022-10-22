using Microsoft.AspNetCore.Mvc;
using ES.Web.Models;
using ES.Web.Services;
using ES.Web.Data;

namespace ES.Web.Services;

public class UserService : IUserService
{
    ESDbContext _context;
    ILogger<UserService> _logger;

    public UserService(
        [FromServices] ESDbContext context,
        [FromServices] ILogger<UserService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<User> GetUserAsync(string login)
    {
        var user = await Task.Run(() => _context.Users.AsQueryable().FirstOrDefault(u => u.Login == login));
        if (user is null)
        {
            throw new ArgumentException($"Не удалось найти пользователя {login}");
        }

        return user;
    }

    public async Task<User> GetUserAsync(Guid userId)
    {
        var user = await Task.Run(() => _context.Users.AsQueryable().FirstOrDefault(u => u.ObjectID == userId));
        if (user is null)
        {
            throw new ArgumentException($"Не удалось найти пользователя {userId}");
        }

        return user;
    }

    public async Task AddUserAsync(User newUser)
    {
        try
        {
            await _context.Users.AddAsync(newUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    public async Task RemoveUserAsync(Guid userId)
    {
        var user = _context.Users.AsQueryable().FirstOrDefault(u => u.ObjectID == userId);

        if (user is null)
        {
            throw new ArgumentException($"Не удалось найти пользователя {userId}");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExistsUserAsync(string login)
    {
        return Task.Run(() => _context.Users.AsQueryable().Any(u => u.Login == login));
    }

    public Task<bool> ExistsUserAsync(Guid userId)
    {
        return Task.Run(() => _context.Users.AsQueryable().Any(u => u.ObjectID == userId));
    }
}