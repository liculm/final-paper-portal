using Api.Controllers.Base;
using FinalPaper.Command.CommandHandlers.Thesis.ApproveThesis;
using FinalPaper.Command.CommandHandlers.Thesis.SubmitThesis;
using FinalPaper.Query.QueryHandlers.GetMentoringRequests;
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
    
    [HttpPut("isApproved/{thesisId}")]
    public async Task<ActionResult<Unit>> ApproveThesis([FromRoute] int thesisId, [FromBody] SetIsApprovedCommand command)
    {
        return await Mediator.Send(command with { ThesisId = thesisId });
    }
    
    [HttpGet("mentoringRequests/{mentorId}")]
    public async Task<ActionResult<List<MentoringRequestsViewModel>>> GetMentoringRequests([FromRoute] Guid mentorId)
    {
        return await Mediator.Send(new GetMentoringRequestsQuery(mentorId));
    }
}