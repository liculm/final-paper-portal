namespace FinalPaper.Command.CommandHandlers.Document.PDFFile;

public sealed class PdfFileViewModel
{
    public byte[] FileContents { get; private set; }
    public string ContentType { get; private set; }
    public string? FileDownloadName { get; private set; }

    public PdfFileViewModel()
    {
        FileContents = new byte[0];
        ContentType = string.Empty;
        FileDownloadName = string.Empty;
    }

    public PdfFileViewModel(byte[] fileContents, string contentType, string? fileDownloadName)
    {
        FileContents = fileContents;
        ContentType = contentType;
        FileDownloadName = fileDownloadName;
    }
}