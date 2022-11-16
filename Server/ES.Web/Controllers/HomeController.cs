using ES.Web.Contracts.V1;
using ES.Web.Models;
using ES.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ES.Web.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpPost]
    [Route(ApiRoutes.Home.Register)]
    public async Task<IActionResult> Register([FromBody] RegisterEditModel model,
        [FromServices] SignInManager siManager)
    {
        var registeredUser = await siManager.Register(model);

        return Ok(registeredUser);
    }

    [HttpPost(ApiRoutes.Home.Login)]
    public async Task<IActionResult> Login([FromBody] LoginEditModel model,
                               [FromServices] IUserService userService,
                               [FromServices] SignInManager siManager)
    {
        if (!ModelState.IsValid)
        {
            return Ok(ModelState);
        }

        var token = string.Empty;
        try
        {
            var user = await userService.GetUserAsync(model.Login);
            token = siManager.SignIn().UsingJWT(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(token);
    }
}
