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

    public Task LoginUser(string login, string password)
    {
        throw new NotImplementedException();
    }

    public async Task RegisterUser(User newUser)
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
}