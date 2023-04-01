using Api.Controllers.Base;
using FinalPaper.Command.CommandHandlers.LoginCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1; 

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : BaseController {
    private readonly IMediator _mediator;
    public UserController(IMediator mediator) {
        _mediator = mediator;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginCommand command) => await mediator.Send(command);
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("Users")]
    public async Task<ActionResult<string>> AllUsers([FromBody] LoginCommand command) => await _mediator.Send(command);

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("test")]
    public async Task<ActionResult<string>> Test() => "asd";

}