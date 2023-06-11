using Api.Controllers.Base;
using FinalPaper.Command.CommandHandlers.User.DeleteUser;
using FinalPaper.Command.CommandHandlers.User.Login;
using FinalPaper.Command.CommandHandlers.User.RefreshJwtToken;
using FinalPaper.Command.CommandHandlers.User.Register;
using FinalPaper.Command.CommandHandlers.User.RevokeRefreshToken;
using FinalPaper.Command.CommandHandlers.User.UpdateUser;
using FinalPaper.Domain.ViewModels;
using FinalPaper.Query.QueryHandlers.GetAllMentors;
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
    public async Task<ActionResult<UserData>> Login([FromBody] LoginCommand command)
    {
        var response = await Mediator.Send(command);
        if (!string.IsNullOrEmpty(response.RefreshToken?.Token))
            SetRefreshTokenInCookie(response.RefreshToken?.Token);

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
    [HttpGet("getAllUsers"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<UserBaseData>>> GetAllUsers()
    {
        return await Mediator.Send(new GetAllUsersQuery());
    }


    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("getAllMentors"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<MentorViewModel>>> GetAllMentors()
    {
        return await Mediator.Send(new GetAllMentorsQuery());
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("test"), Authorize(Roles = "Admin")]
    public Task<ActionResult<string>> Test()
    {
        return Task.FromResult<ActionResult<string>>("Success");
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("updateUser"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<Unit>> UpdateUser([FromBody] UpdateUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("deleteUser/{userId}"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<Unit>> DeleteUser([FromRoute] Guid userId)
    {
        return await Mediator.Send(new DeleteUserCommand(userId));
    }
}