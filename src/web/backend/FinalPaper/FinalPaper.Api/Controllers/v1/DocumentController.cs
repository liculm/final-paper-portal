using Api.Controllers.Base;
using FinalPaper.Command.CommandHandlers.Document.GetPDFFile;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class DocumentController: BaseController
{
    [HttpPost("getPDFFile")]
    public async Task<IActionResult> GetPdfFile([FromQuery] GetPDFFileCommand fileName)
    {
        var filePath = $"C:\\FinalPaperPDFs\\{fileName}.pdf";

        if (!System.IO.File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }
        byte[] pdfBytes = System.IO.File.ReadAllBytes(filePath);

        // return File(pdfBytes, "application/pdf", $"{filePath}.pdf");
        return Ok(await Mediator.Send(filePath));
    }
}
