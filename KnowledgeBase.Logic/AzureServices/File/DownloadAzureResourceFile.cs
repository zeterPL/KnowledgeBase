namespace KnowledgeBase.Logic.AzureServices.File;

public class DownloadAzureResourceFile : AzureResourceFile
{
    public Stream Stream { get; init; }
    public string ContentType { get; set; }

    public DownloadAzureResourceFile(Stream stream, string contentType)
    {
        Stream = stream;
        ContentType = contentType;
    }
}