using Api.Controllers.Base;
using FinalPaper.Command.CommandHandlers.Authentication.LoginCommand;
using FinalPaper.Command.CommandHandlers.Authentication.RegisterCommand;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UserController : BaseController
{
    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("Register")]
    public async Task<ActionResult<string>> Register([FromBody] RegisterCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet("Users")]
    public async Task<ActionResult<string>> AllUsers([FromBody] LoginCommand command)
    {
        return await Mediator.Send(command);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("test")]
    public string Test()
    {
        return "asd";
    }
}