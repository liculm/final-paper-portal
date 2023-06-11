using Api.Controllers.Base;
using FinalPaper.Command.CommandHandlers.Thesis.SubmitThesis;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public sealed class ThesisController : BaseController
{
    [HttpPost("submitThesis")]
    public async Task<ActionResult<Unit>> SubmitThesis([FromBody] SubmitThesisCommand command)
    {
        return await Mediator.Send(command);
    }
}