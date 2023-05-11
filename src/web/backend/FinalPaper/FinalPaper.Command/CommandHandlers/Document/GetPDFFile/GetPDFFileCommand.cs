using FinalPaper.Command.CommandHandlers.Document.PDFFile;
using MediatR;

namespace FinalPaper.Command.CommandHandlers.Document.GetPDFFile;

public sealed record GetPDFFileCommand(string fileName) : IRequest<PdfFileViewModel>;

public sealed record GetPDFFileCommandHandler : IRequestHandler<GetPDFFileCommand, PdfFileViewModel>
{
    public async Task<PdfFileViewModel> Handle(GetPDFFileCommand request, CancellationToken cancellationToken)
    {
        var filePath = $"C:\\FinalPaperPDFs\\{request.fileName}.pdf";

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {request.fileName}");
        }
        var pdfBytes = await File.ReadAllBytesAsync(filePath, cancellationToken);
      
        return new PdfFileViewModel(pdfBytes, "application/pdf", $"{request.fileName}.pdf");
    }
}