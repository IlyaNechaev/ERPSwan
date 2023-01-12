using ES.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ES.Web.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    string _redirectUrl { get; set; }
    public JwtAuthorizeAttribute(string RedirectUrl = null)
    {
        _redirectUrl = RedirectUrl;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        #region ATTRIBUTES_HANDLING

        // Проверка разрешения доступа без аутентификации
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        #endregion

        var user = (User?)context.HttpContext.Items["User"];

        if (user is null)
        {
            if (string.IsNullOrEmpty(_redirectUrl))
            {
                context.Result = new JsonResult("Пользователь не авторизован");
            }
            else
            {
                context.Result = new RedirectResult(_redirectUrl);
            }
        }

    }
}
