using ES.Web.Models;
using ES.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ES.Web.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpPost(Routes.Login)]
    public async Task<IActionResult> Login([FromBody] LoginEditModel model,
                               [FromServices] IUserService userService,
                               [FromServices] SignInManager siManager)
    {
        if (!ModelState.IsValid)
        {
            return Ok(ModelState);
        }

        try
        {
            var user = await userService.GetUserAsync(model.Login);

            siManager.SignIn().UsingJWT(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }
}
