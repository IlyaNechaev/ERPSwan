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
            return Ok(ResponseFactory.CreateResponse(ModelState));
        }

        var user = await userService.GetUser(model.Login, model.Password);
        if (user is null)
        {
            ModelState.AddModelError("Default", "Пользователя с такими логином и паролем не существует");
            return Ok(ResponseFactory.CreateResponse(ModelState));
        }

        var token = string.Empty;
        try
        {
            token = _signInManager.LogIn().UsingJWT(user);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("Default", ex.Message);
            return Ok(ResponseFactory.CreateResponse(ModelState));
        }

        return Ok(ResponseFactory.CreateResponse(token, user.ObjectID, user.IsDefault));
    }
}
