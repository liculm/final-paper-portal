using Api.Controllers.Base;
using FinalPaper.Command.CommandHandlers.Document.GetPDFFile;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class DocumentController : BaseController
{
    [HttpPost("getPDFFile")]
    public async Task<IActionResult> GetPdfFile([FromBody] GetPDFFileCommand command) {
        var result = await Mediator.Send(command);

        return File(result.FileContents, result.ContentType, result.FileDownloadName);
    }
}
