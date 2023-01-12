using ES.Web.Contracts.V1;
using ES.Web.Data;
using ES.Web.Filters;
using ES.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations.Rules;

namespace ES.Web.Controllers;

[ApiController]
[JwtAuthorize]
public class UserController : ControllerBase
{
    ESDbContext _context;
    IUserService _userService;

    public UserController(ESDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    [HttpGet(ApiRoutes.User.GetUser)]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _userService.GetUserAsync(id);

        if (user is null)
        {
            return Ok(new { error = $"Не удалось найти пользователя {id}" });
        }

        return Ok(user);
    }
}
