using Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class DocumentController: BaseController
{
    [HttpGet("getPDFFile")]
    public IActionResult GetPdfFile([FromQuery] string fileName)
    {
        var filePath = $"C:\\FinalPaperPDFs\\{fileName}.pdf";

        if (!System.IO.File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }
        byte[] pdfBytes = System.IO.File.ReadAllBytes(filePath);

        return File(pdfBytes, "application/pdf", $"{filePath}.pdf");
        
    }
}
