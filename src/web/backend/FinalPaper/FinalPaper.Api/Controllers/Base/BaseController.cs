using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Base; 

[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public abstract class BaseController : ControllerBase
{
    protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
    
    protected void SetRefreshTokenInCookie(string? refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
            return;

        var cookieOptions = new CookieOptions { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(10) };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}