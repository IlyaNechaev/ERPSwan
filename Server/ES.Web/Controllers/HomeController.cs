using ES.Web.Contracts.V1;
using ES.Web.Data;
using ES.Web.Models;
using ES.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ES.Web.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    IUserService _userService;

    public HomeController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route(ApiRoutes.Home.Register)]
    public async Task<IActionResult> Register([FromBody] RegisterEditModel model,
        [FromServices] SignInManager siManager)
    {
        var registeredUser = await siManager.Register(model);
        var userResponse = new
        {
            FirstName = registeredUser.FirstName,
            LastName = registeredUser.LastName,
            ObjectID = registeredUser.ObjectID
        };

        return Ok(userResponse);
    }

    [HttpPost(ApiRoutes.Home.Login)]
    public async Task<IActionResult> Login([FromBody] LoginEditModel model,
        [FromServices] SignInManager siManager)
    {
        if (!ModelState.IsValid)
        {
            return Ok(ModelState);
        }

        var token = string.Empty;
        try
        {
            token = await siManager.SignIn().UsingJWTAsync(model.Login, model.Password);
        }
        catch (Exception ex)
        {
            return Ok(new { Error = ex.Message });
        }

        var user = await _userService.GetUserAsync(model.Login);
        return Ok(new { token = token, user = user });
    }
}
