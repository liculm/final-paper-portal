using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Base; 

[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public abstract class BaseController : ControllerBase {
    protected IMediator mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
}