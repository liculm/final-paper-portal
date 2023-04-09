using Api.Controllers.Base;
using FinalPaper.Command.CommandHandlers.Authentication.Login;
using FinalPaper.Command.CommandHandlers.Authentication.RefreshJwtToken;
using FinalPaper.Command.CommandHandlers.Authentication.Register;
using FinalPaper.Command.CommandHandlers.Authentication.RevokeRefreshToken;
using FinalPaper.Domain.Enums;
using FinalPaper.Domain.ViewModels;
using FinalPaper.Query.QueryHandlers.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UserController : BaseController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserViewModel>> Register([FromBody] RegisterCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserViewModel>> Login([FromBody] LoginCommand command)
    {
        var response = await Mediator.Send(command);
        if (!string.IsNullOrEmpty(response.User.RefreshToken?.Token))
            SetRefreshTokenInCookie(response.User.RefreshToken?.Token);

        return response;
    }

    [HttpPost("refresh-jwt-token")]
    public async Task<ActionResult<UserViewModel>> RefreshJwtToken([FromBody] RefreshJwtTokenCommand request)
    {
        var refreshToken = request.RefreshToken ?? Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
            return BadRequest(new { message = "Refresh Token is required!" });

        var response = await Mediator.Send(new RefreshJwtTokenCommand(refreshToken));

        if (!string.IsNullOrEmpty(response.User.RefreshToken?.Token))
            SetRefreshTokenInCookie(response.User.RefreshToken?.Token);

        return response;
    }

    [HttpPost("revoke-refresh-token")]
    public async Task<ActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenCommand request)
    {
        var refreshToken = request.RefreshToken ?? Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
            return BadRequest(new { message = "Refresh Token is required!" });

        var response = await Mediator.Send(new RevokeRefreshTokenCommand(refreshToken));
        if (!response)
            return NotFound(new { message = "Refresh Token not found!" });

        return Ok(new { message = "Refresh Token revoked!" });
    }


    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("all"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<Unit>> GatAllUsers()
    {
        return await Mediator.Send(new GetAllUsersQuery());
    }
}