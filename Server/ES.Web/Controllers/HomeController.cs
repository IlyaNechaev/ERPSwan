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
                               [FromServices] IUserService userService)
    {
        if (!ModelState.IsValid)
        {
            return Ok(ModelState);
        }

        try
        {
            await userService.LoginUser(model.Login, model.Password);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }
}
